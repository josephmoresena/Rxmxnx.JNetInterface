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
	/// <param name="id"></param>
	/// <param name="result">Current result.</param>
	private void DeleteLocalFrame(Guid id, JLocalObject? result)
	{
		this._cache.DeleteLocalFrame(result);
		JTrace.DeleteObjectCache(id, result);
	}
	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> instance for given <paramref name="arraySignature"/>.
	/// </summary>
	/// <param name="arraySignature">JNI array signature.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	private JArrayTypeMetadata GetArrayTypeMetadata(ReadOnlySpan<Byte> arraySignature)
	{
		// Element signature is Array signature without [ prefix.
		ReadOnlySpan<Byte> elementSignature = arraySignature[1..];
		if (elementSignature[0] == UnicodeObjectSignatures.ArraySignaturePrefixChar)
		{
			// Is well-known array class? Primitive arrays are always well-known.
			if (MetadataHelper.GetArrayMetadata(elementSignature) is { } elementArrayMetadata)
				return elementArrayMetadata;

			// Iterates over array element.
			if (MetadataHelper.GetArrayMetadata(this.GetArrayTypeMetadata(elementSignature)) is { } arrayArrayMetadata)
				return arrayArrayMetadata;

			JTrace.UseTypeMetadata(arraySignature, MetadataHelper.ObjectArrayArrayMetadata);
			return MetadataHelper.ObjectArrayArrayMetadata;
		}

		// Object class name is signature without L prefix and ; suffix.
		ReadOnlySpan<Byte> elementClassName = elementSignature[1..^1];
		JReferenceTypeMetadata? elementMetadata = MetadataHelper.GetMetadata(elementClassName);
		if (elementMetadata is null) // Element is not well-known class.
		{
			JClassObject elementClass = this._cache.GetClass(elementClassName);
			elementMetadata = this.GetSuperTypeMetadata(elementClass);
		}

		JArrayTypeMetadata arrayTypeMetadata =
			MetadataHelper.GetArrayMetadata(elementMetadata) ?? MetadataHelper.ObjectArrayMetadata;
		JTrace.UseTypeMetadata(arraySignature, arrayTypeMetadata);
		return arrayTypeMetadata;
	}
	/// <summary>
	/// Retrieves the <see cref="JInterfaceTypeMetadata"/> instance from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="hashes">Set of interface validated hashes.</param>
	/// <returns>A <see cref="JInterfaceTypeMetadata"/> instance.</returns>
	private JInterfaceTypeMetadata? GetSuperInterfaceMetadata(JClassObject jClass, HashSet<String>? hashes = default)
	{
		if (hashes is null && jClass.IsAnnotation) // Annotations should use java.lang.annotation.Annotation metadata.
			return (JInterfaceTypeMetadata)MetadataHelper.GetMetadata<JAnnotationObject>();

		hashes ??= [];
		using JArrayObject<JClassObject> interfaces = jClass.GetInterfaces();
		using LocalFrame _ = new(this, IVirtualMachine.GetSuperTypeCapacity);
		foreach (JClassObject? interfaceClass in interfaces)
		{
			using (interfaceClass)
			{
				// Super interface was already checked.
				if (hashes.Contains(interfaceClass!.Hash)) continue;
				JTrace.GetSuperTypeMetadata(jClass, interfaceClass);

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
	/// Retrieves the <see cref="JClassTypeMetadata"/> instance from <paramref name="jClass"/>
	/// superclass.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
	/// <returns>A <see cref="JClassTypeMetadata"/> instance.</returns>
	private static JClassTypeMetadata GetSuperClassMetadata(JEnvironment env, JClassObject jClass)
	{
		if (jClass.IsEnum) // Enums should use java.lang.Enum metadata.
		{
			JClassTypeMetadata enumTypeMetadata = (JClassTypeMetadata)MetadataHelper.GetMetadata<JEnumObject>();
			JTrace.UseTypeMetadata(jClass, enumTypeMetadata);
			return enumTypeMetadata;
		}

		Boolean checkProxy = true;
		using LocalFrame _ = new(env, IVirtualMachine.GetSuperTypeCapacity);
		while (jClass.GetSuperClass() is { } superClass)
		{
			JTrace.GetSuperTypeMetadata(jClass, superClass);

			// Super class is java.lang.Object.
			if (UnicodeClassNames.Object.AsSpan().SequenceEqual(superClass.Name))
				break;

			// Super class is java.lang.reflect.Proxy.
			if (checkProxy && UnicodeClassNames.ProxyObject().SequenceEqual(superClass.Name))
			{
				using JArrayObject<JClassObject> interfaces = superClass.GetInterfaces();
				if (interfaces.Length > 0 &&
				    jClass.Environment.ClassFeature.GetTypeMetadata(interfaces[0]) is JInterfaceTypeMetadata
					    interfaceMetadata)
				{
					// Use interface proxy metadata.
					JTrace.UseTypeMetadata(jClass, interfaceMetadata.ProxyMetadata);
					return interfaceMetadata.ProxyMetadata;
				}

				// No interface proxy metadata, we should use java.lang.reflect.Proxy metadata.
				JClassTypeMetadata proxyTypeMetadata = (JClassTypeMetadata)MetadataHelper.GetMetadata<JProxyObject>();
				JTrace.UseTypeMetadata(jClass, proxyTypeMetadata);
				return proxyTypeMetadata;
			}
			checkProxy = false;

			// Super class is well-known
			if (MetadataHelper.GetMetadata(superClass.Name) is JClassTypeMetadata classMetadata)
			{
				JTrace.UseTypeMetadata(jClass, classMetadata);
				return classMetadata;
			}

			jClass = superClass;
		}

		JClassTypeMetadata objectTypeMetadata = (JClassTypeMetadata)MetadataHelper.GetMetadata<JLocalObject>();
		JTrace.UseTypeMetadata(jClass, objectTypeMetadata);
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