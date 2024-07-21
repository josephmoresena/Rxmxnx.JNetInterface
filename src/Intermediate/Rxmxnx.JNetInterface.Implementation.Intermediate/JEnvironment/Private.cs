namespace Rxmxnx.JNetInterface;

[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
partial class JEnvironment
{
	/// <summary>
	/// <see cref="JEnvironment"/> cache.
	/// </summary>
	private readonly EnvironmentCache _cache;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cache">A <see cref="JEnvironment"/> reference.</param>
	private JEnvironment(EnvironmentCache cache) => this._cache = cache;

	/// <summary>
	/// Tests whether two references refer to the same object.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="otherRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if both references refer to the same object; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	private unsafe Boolean IsSame(JObjectLocalRef localRef, JObjectLocalRef otherRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.IsSameObjectInfo);
		JBoolean result = nativeInterface.ReferenceFunctions.IsSameObject(this._cache.Reference, localRef, otherRef);
		this._cache.CheckJniError();
		return result.Value;
	}
	/// <summary>
	/// Creates a new local reference frame.
	/// </summary>
	/// <param name="capacity">Frame capacity.</param>
	/// <exception cref="InvalidOperationException"/>
	/// <exception cref="JniException"/>
	private unsafe void CreateLocalFrame(Int32 capacity)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.PushLocalFrameInfo);
		JResult result = nativeInterface.ReferenceFunctions.PushLocalFrame(this.Reference, capacity);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
	}
	/// <summary>
	/// Deletes the current local reference frame.
	/// </summary>
	/// <param name="frame">A <see cref="LocalFrame"/> instance.</param>
	/// <param name="result">Current result.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void DeleteLocalFrame(LocalFrame frame, JLocalObject? result)
	{
		this._cache.DeleteLocalFrame(result);
		JTrace.DeleteObjectCache(frame.Id, result);
	}
	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> instance for given <paramref name="arraySignature"/>.
	/// </summary>
	/// <param name="arraySignature">JNI array signature.</param>
	/// <param name="arrayHash">Array class hash.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	private JArrayTypeMetadata GetArrayTypeMetadata(ReadOnlySpan<Byte> arraySignature, String arrayHash)
	{
		// Element signature is Array signature without [ prefix.
		ReadOnlySpan<Byte> elementSignature = arraySignature[1..];
		ReadOnlySpan<Byte> elementClassName = elementSignature;
		if (elementSignature[0] != CommonNames.ArraySignaturePrefixChar &&
		    elementSignature[^1] == CommonNames.ObjectSignatureSuffixChar)
			// Object class name is signature without L prefix and ; suffix.
			elementClassName = elementSignature[1..^1];
		CStringSequence elementClassInformation = MetadataHelper.GetClassInformation(elementClassName, false);
		String elementHash = elementClassInformation.ToString();
		if (elementSignature[0] == CommonNames.ArraySignaturePrefixChar)
			return this.GetArrayArrayTypeMetadata(arraySignature, arrayHash, elementSignature, elementHash);

		JReferenceTypeMetadata? elementMetadata = MetadataHelper.GetMetadata(elementClassInformation.ToString());
		if (elementMetadata is null) // Element is not well-known class.
		{
			JClassObject elementClass = this._cache.GetClass(elementClassName);
			elementMetadata = this.GetSuperTypeMetadata(elementClass);
		}

		JArrayTypeMetadata arrayTypeMetadata =
			MetadataHelper.GetExactArrayMetadata(elementMetadata) ?? MetadataHelper.ObjectArrayMetadata;
		JTrace.UseTypeMetadata(arraySignature, arrayTypeMetadata);
		if (arrayHash != arrayTypeMetadata.Hash)
			MetadataHelper.RegisterSuperView(arrayHash, arrayTypeMetadata.Hash);
		return arrayTypeMetadata;
	}
	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> instance for given <paramref name="arrayArraySignature"/> using
	/// <paramref name="arraySignature"/>.
	/// </summary>
	/// <param name="arrayArraySignature">JNI array array signature.</param>
	/// <param name="arrayArrayHash">Array array class hash.</param>
	/// <param name="arraySignature">JNI array element signature.</param>
	/// <param name="arrayHash">Array element class hash.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	private JArrayTypeMetadata GetArrayArrayTypeMetadata(ReadOnlySpan<Byte> arrayArraySignature, String arrayArrayHash,
		ReadOnlySpan<Byte> arraySignature, String arrayHash)
	{
		// Is well-known array class? Primitive arrays are always well-known.
		if (MetadataHelper.GetExactArrayMetadata(arrayHash) is { } elementArrayMetadata)
			return elementArrayMetadata;

		// Iterates over array element.
		if (MetadataHelper.GetExactArrayMetadata(this.GetArrayTypeMetadata(arraySignature, arrayHash)) is
		    { } arrayArrayMetadata)
			return arrayArrayMetadata;

		JTrace.UseTypeMetadata(arrayArraySignature, MetadataHelper.ObjectArrayArrayMetadata);
		MetadataHelper.RegisterSuperView(arrayArrayHash, MetadataHelper.ObjectArrayArrayMetadata.Hash);
		return MetadataHelper.ObjectArrayArrayMetadata;
	}
	/// <summary>
	/// Retrieves the <see cref="JInterfaceTypeMetadata"/> instance from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JInterfaceTypeMetadata"/> instance.</returns>
	private JInterfaceTypeMetadata? GetSuperInterfaceMetadata(JClassObject jClass)
	{
		if (!jClass.IsAnnotation) return this.GetSuperInterfaceMetadata(jClass, []);

		// Annotations should use java.lang.annotation.Annotation metadata.
		JInterfaceTypeMetadata annotationMetadata =
			(JInterfaceTypeMetadata)MetadataHelper.GetExactMetadata<JAnnotationObject>();
		MetadataHelper.RegisterSuperView(jClass.Hash, annotationMetadata.Hash);
		return annotationMetadata;
	}
	/// <summary>
	/// Retrieves the <see cref="JInterfaceTypeMetadata"/> instance from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="hashes">Set of interface validated hashes.</param>
	/// <returns>A <see cref="JInterfaceTypeMetadata"/> instance.</returns>
	private JInterfaceTypeMetadata? GetSuperInterfaceMetadata(JClassObject jClass, HashSet<String> hashes)
	{
		using JArrayObject<JClassObject> interfaces = jClass.GetInterfaces();
		using LocalFrame _ = new(this, IVirtualMachine.GetSuperTypeCapacity);
		JArrayLocalRef arrayRef = interfaces.Reference;
		for (Int32 i = 0; i < interfaces.Length; i++)
		{
			JClassObject interfaceClass = this._cache.GetInterfaceClass(arrayRef, i);

			// Super interface was already checked.
			if (hashes.Contains(interfaceClass.Hash)) continue;
			JTrace.GetSuperTypeMetadata(jClass, interfaceClass);

			MetadataHelper.RegisterSuperView(jClass.Hash, interfaceClass.Hash);

			// Super interface is well-known
			if (MetadataHelper.GetMetadata(interfaceClass.Hash) is JInterfaceTypeMetadata superInterfaceMetadata)
			{
				JTrace.UseTypeMetadata(jClass, superInterfaceMetadata);
				return superInterfaceMetadata;
			}

			hashes.Add(interfaceClass.Hash);
			if (this.GetSuperInterfaceMetadata(interfaceClass, hashes) is { } metadata)
				return metadata;
		}
		return default;
	}
	/// <summary>
	/// Retrieves the <see cref="ThrowableException"/> pending exception.
	/// </summary>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	private ThrowableException? GetThrown()
	{
		ThrowableException? jniException = this._cache.Thrown as ThrowableException;
		if (jniException is not null || this._cache.Thrown is null) return jniException;
		if (!this._cache.JniSecure(JniSafetyLevels.ErrorSafe))
			throw this._cache.Thrown;
		return this.ParseException(this._cache.GetPendingException());
	}
	/// <summary>
	/// Parses <paramref name="throwableRef"/> to a <see cref="ThrowableException"/> instance.
	/// </summary>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	private ThrowableException? ParseException(JThrowableLocalRef throwableRef)
	{
		if (throwableRef.IsDefault) return default;
		ThrowableException jniException = this._cache.CreateThrowableException(throwableRef);
		this._cache.ThrowJniException(jniException, false);
		return jniException;
	}
	/// <summary>
	/// Sets <paramref name="throwableException"/> as pending exception.
	/// </summary>
	/// <param name="throwableException">A <see cref="ThrowableException"/> instance.</param>
	private void SetThrown(ThrowableException? throwableException)
	{
		if (this._cache.Thrown is CriticalException)
			throw this._cache.Thrown;
		this._cache.ThrowJniException(throwableException, false);
	}
	/// <summary>
	/// Retrieves <see cref="JReferenceTypeMetadata"/> from super type of <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	private JReferenceTypeMetadata GetSuperTypeMetadata(JClassObject jClass)
	{
		if (!jClass.IsInterface)
			return JEnvironment.GetSuperClassMetadata(this, jClass);
		JReferenceTypeMetadata? result = this.GetSuperInterfaceMetadata(jClass);
		if (result is not null) return result;

		JTrace.UseTypeMetadata(jClass, MetadataHelper.ObjectMetadata);
		return MetadataHelper.ObjectMetadata;
	}
	/// <summary>
	/// Deletes <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference to remove.</param>
	private unsafe void DeleteLocalRef(JObjectLocalRef localRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.DeleteLocalRefInfo);
		nativeInterface.ReferenceFunctions.DeleteLocalRef.DeleteRef(this.Reference, localRef);
	}
	/// <summary>
	/// Retrieves object class reference.
	/// </summary>
	/// <param name="localRef">Object instance to get class.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	private unsafe JClassLocalRef GetObjectClass(JObjectLocalRef localRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetObjectClassInfo);
		JClassLocalRef classRef = nativeInterface.ObjectFunctions.GetObjectClass(this.Reference, localRef);
		if (classRef.IsDefault) this._cache.CheckJniError();
		JTrace.GetObjectClass(localRef, classRef);
		return classRef;
	}

	/// <summary>
	/// Retrieves the <see cref="JClassTypeMetadata"/> instance from <paramref name="jClass"/>
	/// superclass.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	private static JReferenceTypeMetadata GetSuperClassMetadata(JEnvironment env, JClassObject jClass)
	{
		if (jClass.IsEnum) // Enums should use java.lang.Enum metadata.
		{
			JClassTypeMetadata enumTypeMetadata = (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JEnumObject>();
			JTrace.UseTypeMetadata(jClass, enumTypeMetadata);
			MetadataHelper.RegisterSuperClass(jClass.Hash, enumTypeMetadata.Hash);
			return enumTypeMetadata;
		}

		Boolean checkProxy = true;
		using (LocalFrame _ = new(env, IVirtualMachine.GetSuperTypeCapacity))
		{
			while (jClass.GetSuperClass() is { } superClass)
			{
				JTrace.GetSuperTypeMetadata(jClass, superClass);
				MetadataHelper.RegisterSuperClass(jClass.Hash, superClass.Hash);

				// Super class is java.lang.Object.
				if (CommonNames.Object.AsSpan().SequenceEqual(superClass.Name))
					break;

				// Super class is java.lang.reflect.Proxy.
				if (checkProxy && CommonNames.ProxyObject.SequenceEqual(superClass.Name))
				{
					using JArrayObject<JClassObject> interfaces = jClass.GetInterfaces();
					JClassObject? interfaceClass = interfaces.Length > 0 ?
						env._cache.GetInterfaceClass(interfaces.Reference, 0) : // Retrieves first interface class.
						default;

					if (env._cache.GetTypeMetadata(interfaceClass) is JInterfaceTypeMetadata interfaceMetadata)
					{
						// Use interface proxy metadata.
						JTrace.UseTypeMetadata(jClass, interfaceMetadata);
						MetadataHelper.RegisterSuperClass(jClass.Hash, interfaceMetadata.Hash);
						return interfaceMetadata; // If proxy, type metadata is an interface metadata.
					}

					// No interface proxy metadata, we should use java.lang.reflect.Proxy metadata.
					JClassTypeMetadata proxyTypeMetadata =
						(JClassTypeMetadata)MetadataHelper.GetExactMetadata<JProxyObject>();
					JTrace.UseTypeMetadata(jClass, proxyTypeMetadata);
					return proxyTypeMetadata;
				}
				checkProxy = false;

				// Super class is well-known
				if (MetadataHelper.GetExactMetadata(superClass.Hash) is JClassTypeMetadata classMetadata)
				{
					JTrace.UseTypeMetadata(jClass, classMetadata);
					return classMetadata;
				}
				jClass = superClass;
			}
		}

		JClassTypeMetadata objectTypeMetadata = (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JLocalObject>();
		JTrace.UseTypeMetadata(jClass, objectTypeMetadata);
		MetadataHelper.RegisterSuperClass(jClass.Hash, objectTypeMetadata.Hash);
		return objectTypeMetadata;
	}
	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
#pragma warning disable CA1859
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
#pragma warning restore CA1859
}