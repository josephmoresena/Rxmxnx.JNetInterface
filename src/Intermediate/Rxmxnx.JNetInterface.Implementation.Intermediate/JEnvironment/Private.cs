namespace Rxmxnx.JNetInterface;

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
	/// Creates a new global reference to <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JGlobalRef CreateGlobalRef(JReferenceObject jLocal)
		=> this._cache.CreateGlobalRef(jLocal.As<JObjectLocalRef>());
	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> instance for given <paramref name="arraySignature"/>.
	/// </summary>
	/// <param name="arraySignature">JNI array signature.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	private JArrayTypeMetadata GetArrayTypeMetadata(ReadOnlySpan<Byte> arraySignature)
	{
		ReadOnlySpan<Byte> elementSignature = arraySignature[1..];
		if (elementSignature[0] == UnicodeObjectSignatures.ArraySignaturePrefixChar)
		{
			if (MetadataHelper.GetArrayMetadata(elementSignature) is { } arrayTypeMetadata)
				return arrayTypeMetadata;
			return MetadataHelper.GetArrayMetadata(this.GetArrayTypeMetadata(elementSignature)) ??
				MetadataHelper.ObjectArrayArrayMetadata;
		}
		ReadOnlySpan<Byte> elementClassName = elementSignature[1] == UnicodeObjectSignatures.ObjectSignaturePrefixChar ?
			elementSignature[1..^1] :
			elementSignature;
		JReferenceTypeMetadata? elementMetadata = MetadataHelper.GetMetadata(elementClassName);
		if (elementMetadata is null)
		{
			JClassObject elementClass = this._cache.GetClass(elementClassName);
			elementMetadata = this._cache.GetTypeMetadata(elementClass);
		}
		return elementMetadata.GetArrayMetadata() ?? MetadataHelper.ObjectArrayMetadata;
	}
	/// <summary>
	/// Retrieves the <see cref="JClassTypeMetadata"/> instance from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JClassTypeMetadata"/> instance.</returns>
	private static JClassTypeMetadata GetClassMetadata(JClassObject jClass)
	{
		switch (jClass.IsEnum)
		{
			case true:
				return (JClassTypeMetadata)MetadataHelper.GetMetadata<JEnumObject>();
			default:
				while (jClass.GetSuperClass() is { } superClass)
				{
					if (UnicodeClassNames.ProxyObject().SequenceEqual(superClass.Name))
					{
						using JArrayObject<JClassObject> interfaces = superClass.GetInterfaces();
						if (jClass.Environment.ClassFeature.GetTypeMetadata(interfaces.FirstOrDefault()) is
						    JInterfaceTypeMetadata interfaceMetadata)
							return interfaceMetadata.ProxyMetadata;
					}
					else if (MetadataHelper.GetMetadata(superClass.Name) is JClassTypeMetadata classMetadata)
					{
						return classMetadata;
					}
				}
				break;
		}
		return (JClassTypeMetadata)MetadataHelper.GetMetadata<JLocalObject>();
	}
	/// <summary>
	/// Retrieves the <see cref="JInterfaceTypeMetadata"/> instance from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="hashes">Set of interface validated hashes.</param>
	/// <returns>A <see cref="JInterfaceTypeMetadata"/> instance.</returns>
	private JInterfaceTypeMetadata? GetInterfaceMetadata(JClassObject jClass, HashSet<String>? hashes = default)
	{
		if (jClass.IsAnnotation)
			return (JInterfaceTypeMetadata)MetadataHelper.GetMetadata<JAnnotationObject>();
		hashes ??= [];
		using JArrayObject<JClassObject> interfaces = jClass.GetInterfaces();
		using LocalFrame _ = new(this, 2);
		foreach (JClassObject? interfaceClass in interfaces)
		{
			if (hashes.Contains(interfaceClass!.Hash)) continue;
			if (this.GetInterfaceMetadata(interfaceClass) is { } metadata)
				return metadata;
			hashes.Add(interfaceClass.Hash);
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
		if (jniException is null && this._cache.Thrown is not null)
			if (!this._cache.JniSecure(JniSafetyLevels.ErrorSafe))
			{
				throw this._cache.Thrown;
			}
			else
			{
				JThrowableLocalRef throwableRef = this._cache.GetPendingException();
				if (!throwableRef.IsDefault)
				{
					jniException = this._cache.CreateThrowableException(throwableRef);
					this._cache.ThrowJniException(jniException, false);
				}
			}
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

	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
#pragma warning disable CA1859
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
#pragma warning restore CA1859
	/// <summary>
	/// Retrieves field identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	private static unsafe JFieldId GetFieldId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		ref readonly NativeInterface nativeInterface =
			ref args.env._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetFieldIdInfo);
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return nativeInterface.InstanceFieldFunctions.GetFieldId.GetId(args.env.Reference, args.classRef, namePtr,
		                                                               signaturePtr);
	}
	/// <summary>
	/// Retrieves static field identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	private static unsafe JFieldId GetStaticFieldId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		ref readonly NativeInterface nativeInterface =
			ref args.env._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticFieldIdInfo);
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return nativeInterface.StaticFieldFunctions.GetFieldId.GetId(args.env.Reference, args.classRef, namePtr,
		                                                             signaturePtr);
	}
	/// <summary>
	/// Retrieves method identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private static unsafe JMethodId GetMethodId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		ref readonly NativeInterface nativeInterface =
			ref args.env._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetMethodIdInfo);
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return nativeInterface.InstanceMethodFunctions.MethodFunctions.GetMethodId.GetId(
			args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
	/// <summary>
	/// Retrieves static method identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private static unsafe JMethodId GetStaticMethodId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		ref readonly NativeInterface nativeInterface =
			ref args.env._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticMethodIdInfo);
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return nativeInterface.StaticMethodFunctions.GetMethodId.GetId(args.env.Reference, args.classRef, namePtr,
		                                                               signaturePtr);
	}
}