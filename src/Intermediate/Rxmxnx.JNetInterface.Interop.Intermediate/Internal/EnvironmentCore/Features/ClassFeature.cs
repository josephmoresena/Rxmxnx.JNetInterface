namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed partial class EnvironmentCore : IClassFeature
{
	public JClassObject AsClassObject(JClassLocalRef classRef) => this.AsClassObject(classRef, default);
	public JClassObject AsClassObject(JReferenceObject jObject)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jObject);
		if (jObject is JClassObject jClass) return jClass;
		ImplementationValidationUtilities.ThrowIfDefault(jObject);
		if (jObject.InstanceOf<JClassObject>()) return this.AsClassObjectUnchecked(jObject);

		IMessageResource resource = IMessageResource.GetInstance();
		throw new ArgumentException(resource.NotClassObject);
	}
	[return: NotNullIfNotNull(nameof(jClass))]
	public JReferenceTypeMetadata? GetTypeMetadata(JClassObject? jClass)
	{
		if (jClass is null) return default;
		JTrace.GetMetadataOrFindClass(jClass);
		if (MetadataHelper.GetMetadata(jClass.Hash) is { } result) // Is well-known class?
		{
			JTrace.UseTypeMetadata(jClass, result);
			return result;
		}
		result = jClass.ClassSignature[0] switch
		{
			CommonNames.BooleanSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JBooleanObject>(),
			CommonNames.ByteSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JByteObject>(),
			CommonNames.CharSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JCharacterObject>(),
			CommonNames.DoubleSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JDoubleObject>(),
			CommonNames.FloatSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JFloatObject>(),
			CommonNames.IntSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JIntegerObject>(),
			CommonNames.LongSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JLongObject>(),
			CommonNames.ShortSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JShortObject>(),
			CommonNames.VoidSignatureChar => (JClassTypeMetadata)MetadataHelper.GetExactMetadata<JVoidObject>(),
			CommonNames.ArraySignaturePrefixChar => this.GetArrayTypeMetadata(jClass.ClassSignature, jClass.Hash),
			_ => this.GetSuperTypeMetadata(jClass),
		};

		if (jClass.ClassSignature.Length == 1) // Primitive class should use WrapperClassMetadata.
			JTrace.UseTypeMetadata(jClass, result);
		return result;
	}
	public JModuleObject? GetModule(JClassObject jClass)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
#if !ANDROID
		if (this.Version >= NativeInterface9.RequiredVersion)
		{
			using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(1);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			return this.GetModule(classRef);
		}
		if (AndroidFeature.ApiLevel is > 0 || this.Host.Value.Version < JRuntimeVersion.J9) return default;
		return EnvironmentCore.GetModule(this, jClass);
#else
		return default;
#endif
	}
	public void ThrowNew(JClassObject jClass, String? message, Boolean throwException)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		this.CheckClassCompatibility<JThrowableObject>(jClass, out _);

		JReferenceTypeMetadata throwableMetadata = this.GetTypeMetadata(jClass);
		CString? utf8Message = (CString?)message;
		this.ThrowNew(jClass, throwableMetadata, utf8Message, throwException, message);
	}
	public void ThrowNew(JClassObject jClass, CString? message, Boolean throwException)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		this.CheckClassCompatibility<JThrowableObject>(jClass, out _);

		JReferenceTypeMetadata throwableMetadata = this.GetTypeMetadata(jClass);
		ReadOnlySpan<Byte> utf8Message = EnvironmentCore.GetSafeSpan(message);
		this.ThrowNew(jClass, throwableMetadata, utf8Message, throwException, message?.ToString());
	}
#if !NET8_0_OR_GREATER
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public void ThrowNew<TThrowable>(CString? message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		ReadOnlySpan<Byte> utf8Message = EnvironmentCore.GetSafeSpan(message);
		this.ThrowNew<TThrowable>(utf8Message, throwException, message?.ToString());
	}
#if !NET8_0_OR_GREATER
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public void ThrowNew<TThrowable>(String? message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		CString? utf8Message = (CString?)message;
		this.ThrowNew<TThrowable>(utf8Message, throwException, message);
	}
	public JClassObject GetClass(ReadOnlySpan<Byte> className)
	{
		TypeInfoSequence classInformation = MetadataHelper.GetClassInformation(className, false);
		ITypeInformation typeInformation = this.Host.TypeManager.GetTypeInformation(classInformation.ToString()) ??
			new TypeInformation(classInformation);
		return this.GetOrFindClass(typeInformation);
	}
	public JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata typeInformation = MetadataHelper.GetExactMetadata<TDataType>();
		return this.GetOrFindClass(typeInformation);
	}
	public JClassObject GetClass(ITypeInformation typeInformation)
	{
		ImplementationValidationUtilities.ThrowIfProxy(typeInformation as ObjectMetadata);
		return this.GetOrFindClass(typeInformation);
	}
	public JClassObject GetObjectClass(ObjectMetadata objectMetadata)
	{
		ImplementationValidationUtilities.ThrowIfProxy(objectMetadata);
		ITypeInformation typeInformation = this.Host.TypeManager.GetTypeInformation(objectMetadata.ObjectClassHash) ??
			new TypeInformation(new(objectMetadata.ObjectClassHash, objectMetadata.ObjectClassName.Length,
			                        objectMetadata.ObjectSignature.Length));
		return this.GetOrFindClass(typeInformation);
	}
	public JClassObject GetObjectClass(JLocalObject jLocal)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jLocal);
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(1);
		JObjectLocalRef localRef = jniTransaction.Add(jLocal);
		JClassLocalRef classRef = EnvironmentCore.GetObjectClass(this, localRef);
		JTypeKind kind = jLocal is JArrayObject ? JTypeKind.Array : JTypeKind.Class;
		JClassObject jClass = this.GetClass(classRef, true, kind, true);
		return this.Register(jClass);
	}
	public JClassObject? GetSuperClass(JClassObject jClass)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		if (jClass.IsPrimitive || jClass.IsInterface || jClass.Name.AsSpan().SequenceEqual(CommonNames.Object))
			return default; // Primitive classes, Interfaces classes or Object class has no super-class.
		if (jClass.ClassSignature[0] == CommonNames.ArraySignaturePrefixChar)
			return this.GetClass<JLocalObject>(); // Super-class of Array classes is Object class.
		if (MetadataHelper.GetExactMetadata(jClass.Hash)?.BaseMetadata is { } metadata &&
		    metadata.IsCompatibleWith(this._env))
			// Only if super class is compatible with current JRE or Android API.
			return this.GetOrFindClass(metadata); // Well-known class.
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(2);
		JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
		ImplementationValidationUtilities.ThrowIfDefault(jClass);
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetSuperclassInfo);
		JClassLocalRef superClassRef =
			jniTransaction.Add(nativeInterface.ClassFunctions.GetSuperclass(this.Reference, classRef));
		if (superClassRef != default)
		{
			JClassObject jSuperClass =
				this.AsClassObject(superClassRef, new() { Kind = JTypeKind.Class, IsFinal = false, });
			if (jSuperClass.LocalReference != superClassRef.Value) this.DeleteLocalRef(superClassRef.Value);
			MetadataHelper.RegisterSuperClass(jClass.Hash, jSuperClass.Hash);
			return jSuperClass;
		}
		this.CheckJniError();
		return default;
	}
	public Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
		=> this.IsAssignableFrom(jClass, otherClass, default);
	public Boolean IsInstanceOf(JReferenceObject jObject, JClassObject jClass)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jObject);
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(2);
		JObjectLocalRef localRef = jniTransaction.Add(jObject);
		JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
		return this.IsInstanceOf(localRef, classRef);
	}
	public Boolean IsInstanceOf<TDataType>(JReferenceObject jObject)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		Boolean result = this.IsInstanceOf(jObject, this.GetClass<TDataType>());
		jObject.SetAssignableTo<TDataType>(result);
		return result;
	}
	public JClassObject LoadClass(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> rawClassBytes,
		JClassLoaderObject? jClassLoader = default)
	{
		TypeInfoSequence classInformation = MetadataHelper.GetClassInformation(className, false);
		ITypeInformation typeInformation = new TypeInformation(classInformation);
		return this.LoadClass(typeInformation, rawClassBytes, jClassLoader);
	}
	public JClassObject LoadClass<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TDataType>(
		ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader = default)
		where TDataType : JReferenceObject, IReferenceType<TDataType>
	{
		ITypeInformation metadata = MetadataHelper.GetExactMetadata<TDataType>();
		return this.LoadClass(metadata, rawClassBytes, jClassLoader);
	}
	public void GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		ImplementationValidationUtilities.ThrowIfDefault(jClass);
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(1);
		JClassLocalRef classRef = jniTransaction.Add(jClass);
		Boolean isLocalRef = this.IsLocalObject(jClass, out JReferenceType referenceType);
		JTrace.GetClassInfo(classRef, referenceType);

		if (classRef == default)
		{
			IMessageResource resource = IMessageResource.GetInstance();
			throw new ArgumentException(resource.UnloadedClass);
		}

		JClassObject loadedClass = this.GetClass(classRef, isLocalRef);
		name = loadedClass.Name;
		signature = loadedClass.ClassSignature;
		hash = loadedClass.Hash;
		if (!Object.ReferenceEquals(jClass, loadedClass))
			loadedClass.Lifetime.Synchronize(jClass.Lifetime);
	}
}