namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record EnvironmentCache : IClassFeature
	{
		public JClassObject AsClassObject(JClassLocalRef classRef)
		{
			JClassObject result = this.GetClass(classRef, true);
			return this.Register(result);
		}
		public JClassObject AsClassObject(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			if (jObject is JClassObject jClass) return jClass;
			ValidationUtilities.ThrowIfDefault(jObject);
			if (!jObject.IsInstanceOf<JClassObject>()) throw new ArgumentException("Object is not a class");
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
			ValidationUtilities.ThrowIfDummy(jObject);
			JClassObject jClass = this.GetClass<TDataType>();
			this.ReloadClass(jObject as JClassObject);
			ValidationUtilities.ThrowIfDefault(jObject);
			JClassObject objectClass = this.GetClass(jObject.ObjectClassName);
			Boolean result = this.IsAssignableFrom(objectClass, jClass);
			this.SetAssignableTo<TDataType>(jObject, result);
			return result;
		}
		public JClassObject GetClass(ReadOnlySpan<Byte> className)
		{
			CStringSequence classInformation = MetadataHelper.GetClassInformation(className);
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
			ValidationUtilities.ThrowIfDummy(jClass);
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
			ValidationUtilities.ThrowIfDummy(jClass);
			ValidationUtilities.ThrowIfDummy(otherClass);
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
			ValidationUtilities.ThrowIfDummy(jObject);
			ValidationUtilities.ThrowIfDummy(jClass);
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
		public JClassObject LoadClass(CString className, ReadOnlySpan<Byte> rawClassBytes,
			JClassLoaderObject? jClassLoader = default)
		{
			className = JDataTypeMetadata.JniParseClassName(className);
			return NativeUtilities.WithSafeFixed(className.AsSpan(), rawClassBytes, (this, jClassLoader),
			                                     EnvironmentCache.LoadClass);
		}
		public JClassObject LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes,
			JClassLoaderObject? jClassLoader = default) where TDataType : JLocalObject, IReferenceType<TDataType>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TDataType>();
			return NativeUtilities.WithSafeFixed(metadata.ClassName.AsSpan(), rawClassBytes, (this, jClassLoader),
			                                     EnvironmentCache.LoadClass);
		}
		public void GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
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
		public JClassObject GetClass(String classHash)
		{
			if (this._classes.TryGetValue(classHash, out JClassObject? jClass))
				return jClass;
			CStringSequence classInformation = MetadataHelper.GetClassInformation(classHash);
			return this.GetOrFindClass(new TypeInformation(classInformation));
		}
	}
}