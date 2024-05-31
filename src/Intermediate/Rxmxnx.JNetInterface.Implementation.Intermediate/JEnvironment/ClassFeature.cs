namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial record EnvironmentCache : IClassFeature
	{
		public JClassObject AsClassObject(JClassLocalRef classRef) => this.Register(this.GetClass(classRef, true));
		public JClassObject AsClassObject(JReferenceObject jObject)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			if (jObject is JClassObject jClass) return jClass;
			ImplementationValidationUtilities.ThrowIfDefault(jObject);
			if (!jObject.InstanceOf<JClassObject>()) throw new ArgumentException("Object is not a class");
			return this.AsClassObjectUnchecked(jObject);
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
			using LocalFrame _ = new(this._env, 2);
			result = jClass.ClassSignature[0] switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => (JClassTypeMetadata)MetadataHelper
					.GetMetadata<JBooleanObject>(),
				UnicodePrimitiveSignatures.ByteSignatureChar => (JClassTypeMetadata)MetadataHelper
					.GetMetadata<JByteObject>(),
				UnicodePrimitiveSignatures.CharSignatureChar => (JClassTypeMetadata)MetadataHelper
					.GetMetadata<JCharacterObject>(),
				UnicodePrimitiveSignatures.DoubleSignatureChar => (JClassTypeMetadata)MetadataHelper
					.GetMetadata<JDoubleObject>(),
				UnicodePrimitiveSignatures.FloatSignatureChar => (JClassTypeMetadata)MetadataHelper
					.GetMetadata<JFloatObject>(),
				UnicodePrimitiveSignatures.IntSignatureChar => (JClassTypeMetadata)MetadataHelper
					.GetMetadata<JIntegerObject>(),
				UnicodePrimitiveSignatures.LongSignatureChar => (JClassTypeMetadata)MetadataHelper
					.GetMetadata<JLongObject>(),
				UnicodePrimitiveSignatures.ShortSignatureChar => (JClassTypeMetadata)MetadataHelper
					.GetMetadata<JShortObject>(),
				UnicodeObjectSignatures.ArraySignaturePrefixChar => this._env.GetArrayTypeMetadata(
					jClass.ClassSignature),
				_ => this._env.GetSuperTypeMetadata(jClass),
			};

			if (jClass.ClassSignature.Length == 1) // Primitive class should use WrapperClassMetadata.
				JTrace.UseTypeMetadata(jClass, result);
			return result;
		}
		public JModuleObject? GetModule(JClassObject jClass)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			if (this.Version < NativeInterface9.RequiredVersion) return default;
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			return this.GetModule(classRef);
		}
		public void ThrowNew<TThrowable>(CString? message, Boolean throwException)
			where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			ReadOnlySpan<Byte> utf8Message = JEnvironment.GetSafeSpan(message);
			this.ThrowNew<TThrowable>(utf8Message, throwException, message?.ToString());
		}
		public void ThrowNew<TThrowable>(String? message, Boolean throwException)
			where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			CString? utf8Message = (CString?)message;
			this.ThrowNew<TThrowable>(utf8Message, throwException, message);
		}

		public JClassObject GetClass(ReadOnlySpan<Byte> className)
		{
			CStringSequence classInformation = MetadataHelper.GetClassInformation(className, false);
			return this.GetOrFindClass(new TypeInformation(classInformation));
		}
		public JClassObject GetClass(String classHash)
		{
			if (this._classes.TryGetValue(classHash, out JClassObject? jClass))
				return jClass;
			CStringSequence classInformation = MetadataHelper.GetClassInformation(classHash);
			return this.GetOrFindClass(new TypeInformation(classInformation));
		}
		public JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TDataType>();
			return this.GetOrFindClass(metadata);
		}
		public JClassObject GetObjectClass(JLocalObject jLocal) => this.GetClass(jLocal.ObjectClassName);
		public unsafe JClassObject? GetSuperClass(JClassObject jClass)
		{
			if (MetadataHelper.GetMetadata(jClass.Hash)?.BaseMetadata is { } metadata)
				return this.GetOrFindClass(metadata);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			ImplementationValidationUtilities.ThrowIfDefault(jClass);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetSuperclassInfo);
			JClassLocalRef superClassRef =
				jniTransaction.Add(nativeInterface.ClassFunctions.GetSuperclass(this.Reference, classRef));
			if (!superClassRef.IsDefault)
			{
				JClassObject jSuperClass = this.AsClassObject(superClassRef);
				if (jSuperClass.LocalReference != superClassRef.Value) this._env.DeleteLocalRef(superClassRef.Value);
				return jSuperClass;
			}
			this.CheckJniError();
			return default;
		}
		public unsafe Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
		{
			Boolean? result = MetadataHelper.IsAssignableFrom(jClass, otherClass);
			if (result.HasValue) return result.Value;
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			ImplementationValidationUtilities.ThrowIfProxy(otherClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JClassLocalRef otherClassRef = jniTransaction.Add(this.ReloadClass(otherClass));
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.IsAssignableFromInfo);
			result = nativeInterface.ClassFunctions.IsAssignableFrom(this.Reference, classRef, otherClassRef).Value;
			this.CheckJniError();
			return result.Value;
		}
		public Boolean IsInstanceOf(JReferenceObject jObject, JClassObject jClass)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
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
			CStringSequence classInformation = MetadataHelper.GetClassInformation(className, false);
			ITypeInformation metadata = new TypeInformation(classInformation);
			return this.LoadClass(metadata, rawClassBytes, jClassLoader);
		}
		public JClassObject
			LoadClass<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TDataType>(
				ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader = default)
			where TDataType : JLocalObject, IReferenceType<TDataType>
		{
			ITypeInformation metadata = MetadataHelper.GetMetadata<TDataType>();
			return this.LoadClass(metadata, rawClassBytes, jClassLoader);
		}
		public void GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			ImplementationValidationUtilities.ThrowIfDefault(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JClassLocalRef classRef = jniTransaction.Add(jClass);
			JReferenceType referenceType = this._env.GetReferenceType(classRef.Value);
			Boolean isLocalRef = referenceType == JReferenceType.LocalRefType;
			JTrace.GetClassInfo(classRef, referenceType);
			if (classRef.IsDefault) throw new ArgumentException("Unloaded class.");
			JClassObject loadedClass = this.GetClass(classRef, isLocalRef);
			name = loadedClass.Name;
			signature = loadedClass.ClassSignature;
			hash = loadedClass.Hash;
			if (!Object.ReferenceEquals(jClass, loadedClass))
				loadedClass.Lifetime.Synchronize(jClass.Lifetime);
		}
	}
}