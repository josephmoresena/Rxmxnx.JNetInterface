namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
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
		if (!this._cache.JniSecure(JniSafetyLevels.ErrorSafe) && this._cache.HasPendingException())
			// Do not throw if not pending JNI exception.
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
		if (throwableRef == default) return default;
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
		if (throwableException is not null && Object.ReferenceEquals(CriticalException.Instance, this._cache.Thrown) &&
		    this._cache.HasPendingException())
			// Do not throw if there is no pending JNI exception or exception in the process of being cleared.
			throw this._cache.Thrown;
		this._cache.ThrowJniException(throwableException, false);
	}

	/// <summary>
	/// Retrieves field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	private unsafe JFieldId GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetFieldIdInfo);
		JFieldId fieldId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			fieldId = nativeInterface.InstanceFieldFunctions.GetFieldId.GetId(
				this.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, fieldId);
		if (fieldId == default) this._cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private unsafe JMethodId GetMethodId(JCallDefinition definition, JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetMethodIdInfo);
		JMethodId methodId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			methodId = nativeInterface.InstanceMethodFunctions.MethodFunctions.GetMethodId.GetId(
				this.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, methodId);
		if (methodId == default) this._cache.CheckJniError();
		return methodId;
	}
	/// <summary>
	/// Retrieves static method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private unsafe JMethodId GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticMethodIdInfo);
		JMethodId methodId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			methodId = nativeInterface.StaticMethodFunctions.GetMethodId.GetId(
				this.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, methodId);
		if (methodId == default) this._cache.CheckJniError();
		return methodId;
	}

	/// <summary>
	/// Indicates whether validation of <paramref name="jGlobal"/> can be avoided.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jGlobal"/> validation can be avoided;
	/// otherwise, <see langword="false"/>;
	/// </returns>
	private static Boolean IsValidationAvoidable(EnvironmentCache? cache, JGlobalBase jGlobal)
	{
		if (cache is null || !cache.Host.MemoryManager.SecureRemove(jGlobal.As<JObjectLocalRef>())) return true;
		Boolean isWeak = jGlobal is JWeak;
		if (!isWeak && LocalMainClasses.IsMainGlobal(jGlobal as JGlobal))
			return true;
		return Random.Shared.Next(0, 10) > (!isWeak ? 5 : 2);
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
				if (CommonNames.Object.SequenceEqual(superClass.Name))
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
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
#pragma warning restore CA1859
}