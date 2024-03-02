namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache : IClassFeature
	{
		public JClassObject AsClassObject(JClassLocalRef classRef) => this.Register(this.GetClass(classRef, true));
		public JClassObject AsClassObject(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfProxy(jObject);
			if (jObject is JClassObject jClass) return jClass;
			ValidationUtilities.ThrowIfDefault(jObject);
			if (!jObject.InstanceOf<JClassObject>()) throw new ArgumentException("Object is not a class");
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JClassLocalRef classRef = jniTransaction.Add<JClassLocalRef>(jObject);
			JClassObject result = this.AsClassObject(classRef);
			if (jObject is ILocalObject local && classRef == result.InternalReference)
				result.Lifetime.Synchronize(local.Lifetime);
			return result;
		}
		public Boolean IsAssignableTo<TDataType>(JReferenceObject jObject)
			where TDataType : JReferenceObject, IDataType<TDataType>
		{
			ValidationUtilities.ThrowIfProxy(jObject);
			JClassObject jClass = this.GetClass<TDataType>();
			this.ReloadClass(jObject as JClassObject);
			ValidationUtilities.ThrowIfDefault(jObject);
			JClassObject objectClass = this.GetClass(jObject.ObjectClassName);
			Boolean result = this.IsAssignableFrom(objectClass, jClass);
			this.SetAssignableTo<TDataType>(jObject, result);
			return result;
		}
		[return: NotNullIfNotNull(nameof(jClass))]
		public JReferenceTypeMetadata? GetTypeMetadata(JClassObject? jClass)
		{
			if (jClass is null) return default;
			if (MetadataHelper.GetMetadata(jClass.Hash) is { } result)
				return result;
			using LocalFrame _ = new(this._env, 2);
			return jClass.ClassSignature[0] switch
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
				_ => !jClass.IsInterface ?
					this._env.GetClassMetadata(jClass) :
					this._env.GetInterfaceMetadata(jClass) ??
					(JReferenceTypeMetadata)MetadataHelper.GetMetadata<JLocalObject>(),
			};
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
		public JClassObject? GetSuperClass(JClassObject jClass)
		{
			if (MetadataHelper.GetMetadata(jClass.Hash)?.BaseMetadata is { } metadata)
				return this.GetOrFindClass(metadata);
			ValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			ValidationUtilities.ThrowIfDefault(jClass);
			GetSuperclassDelegate getSuperClass = this.GetDelegate<GetSuperclassDelegate>();
			JClassLocalRef superClassRef = jniTransaction.Add(getSuperClass(this.Reference, classRef));
			if (!superClassRef.IsDefault)
			{
				JClassObject jSuperClass = this.AsClassObject(superClassRef);
				if (jSuperClass.InternalReference != superClassRef.Value) this._env.DeleteLocalRef(superClassRef.Value);
				return jSuperClass;
			}
			this.CheckJniError();
			return default;
		}
		public Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
		{
			Boolean? result = MetadataHelper.IsAssignableFrom(jClass, otherClass);
			if (result.HasValue) return result.Value;
			ValidationUtilities.ThrowIfProxy(jClass);
			ValidationUtilities.ThrowIfProxy(otherClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JClassLocalRef otherClassRef = jniTransaction.Add(this.ReloadClass(otherClass));
			IsAssignableFromDelegate isAssignableFrom = this.GetDelegate<IsAssignableFromDelegate>();
			result = isAssignableFrom(this.Reference, classRef, otherClassRef) == JBoolean.TrueValue;
			this.CheckJniError();
			return result.Value;
		}
		public Boolean IsInstanceOf(JReferenceObject jObject, JClassObject jClass)
		{
			ValidationUtilities.ThrowIfProxy(jObject);
			ValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JObjectLocalRef localRef = jniTransaction.Add(jObject);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			return this.IsInstanceOf(localRef, classRef);
		}
		public Boolean IsInstanceOf<TDataType>(JReferenceObject jObject)
			where TDataType : JReferenceObject, IDataType<TDataType>
		{
			Boolean result = this.IsInstanceOf(jObject, this.GetClass<TDataType>());
			return result;
		}
		public JClassObject LoadClass(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> rawClassBytes,
			JClassLoaderObject? jClassLoader = default)
		{
			CStringSequence classInformation = MetadataHelper.GetClassInformation(className, false);
			ITypeInformation metadata = new TypeInformation(classInformation);
			return rawClassBytes.WithSafeFixed((this, metadata, jClassLoader), EnvironmentCache.LoadClass);
		}
		public JClassObject LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes,
			JClassLoaderObject? jClassLoader = default) where TDataType : JLocalObject, IReferenceType<TDataType>
		{
			ITypeInformation metadata = MetadataHelper.GetMetadata<TDataType>();
			return rawClassBytes.WithSafeFixed((this, metadata, jClassLoader), EnvironmentCache.LoadClass);
		}
		public void GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
		{
			ValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			if (classRef.IsDefault) throw new ArgumentException("Unloaded class.");
			JClassObject loadedClass = this.AsClassObject(classRef);
			name = loadedClass.Name;
			signature = loadedClass.ClassSignature;
			hash = loadedClass.Hash;
			if (!Object.ReferenceEquals(jClass, loadedClass))
				loadedClass.Lifetime.Synchronize(jClass.Lifetime);
		}
		public void SetAssignableTo<TDataType>(JReferenceObject jObject, Boolean isAssignable)
			where TDataType : JReferenceObject, IDataType<TDataType>
			=> jObject.SetAssignableTo<TDataType>(isAssignable);
	}
}