namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IClassProvider
	{
		JClassObject IClassProvider.Object => this.GetClass<JLocalObject>();
		JClassObject IClassProvider.StringClassObject => this.GetClass<JStringObject>();
		JClassObject IClassProvider.NumberClassObject => this.GetClass<JNumberObject>();
		JClassObject IClassProvider.EnumClassObject => this.GetClass<JEnumObject>();
		JClassObject IClassProvider.ThrowableClassObject => this.GetClass<JThrowableObject>();
		JClassObject IClassProvider.BooleanObject => this.GetClass<JBooleanObject>();
		JClassObject IClassProvider.ByteObject => this.GetClass<JByteObject>();
		JClassObject IClassProvider.CharacterObject => this.GetClass<JCharacterObject>();
		JClassObject IClassProvider.DoubleObject => this.GetClass<JDoubleObject>();
		JClassObject IClassProvider.FloatObject => this.GetClass<JFloatObject>();
		JClassObject IClassProvider.IntegerObject => this.GetClass<JIntegerObject>();
		JClassObject IClassProvider.LongObject => this.GetClass<JLongObject>();
		JClassObject IClassProvider.ShortObject => this.GetClass<JShortObject>();

		public JClassObject AsClassObject(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			this.ReloadClass(jObject as JClassObject);
			ValidationUtilities.ThrowIfDefault(jObject);
			JEnvironment env = this._mainClasses.Environment;
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			JObjectLocalRef localRef = jniTransaction.Add(jObject);
			JClassLocalRef classRef = jniTransaction.Add(env.GetObjectClass(localRef));
			return env.GetClass(classRef, true);
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
			jObject.SetAssignableTo<TDataType>(result);
			return result;
		}
		public JClassObject GetClass(CString className)
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
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			ValidationUtilities.ThrowIfDefault(jClass);
			GetSuperclassDelegate getSuperClass = this.GetDelegate<GetSuperclassDelegate>();
			JClassLocalRef superClassRef = jniTransaction.Add(getSuperClass(this.Reference, classRef));
			JEnvironment env = this._mainClasses.Environment;
			if (superClassRef.Value != default)
				return env.GetClass(superClassRef, true);
			this.CheckJniError();
			return default;
		}
		public Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
		{
			Boolean? result = MetadataHelper.IsAssignableFrom(jClass, otherClass);
			if (result.HasValue) return result.Value;
			ValidationUtilities.ThrowIfDummy(jClass);
			ValidationUtilities.ThrowIfDummy(otherClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JClassLocalRef otherClassRef = jniTransaction.Add(this.ReloadClass(otherClass));
			IsAssignableFromDelegate isAssignableFrom = this.GetDelegate<IsAssignableFromDelegate>();
			result = isAssignableFrom(this.Reference, classRef, otherClassRef) == JBoolean.TrueValue;
			this.CheckJniError();
			return result.Value;
		}
		public JClassObject LoadClass(CString className, ReadOnlySpan<Byte> rawClassBytes,
			JLocalObject? jClassLoader = default)
		{
			className = JDataTypeMetadata.JniParseClassName(className);
			return NativeUtilities.WithSafeFixed(className.AsSpan(), rawClassBytes, (this, jClassLoader),
			                                     JEnvironmentCache.LoadClass);
		}
		public JClassObject LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes, JLocalObject? jClassLoader = default)
			where TDataType : JLocalObject, IReferenceType<TDataType>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TDataType>();
			return NativeUtilities.WithSafeFixed(metadata.ClassName.AsSpan(), rawClassBytes, (this, jClassLoader),
			                                     JEnvironmentCache.LoadClass);
		}
		public void GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			if (classRef.Value == default) throw new ArgumentException("Unloaded class.");
			JClassObject loadedClass = this._mainClasses.Environment.GetClass(classRef, true);
			name = loadedClass.Name;
			signature = loadedClass.ClassSignature;
			hash = loadedClass.Hash;
			if (!Object.ReferenceEquals(jClass, loadedClass))
				loadedClass.Lifetime.Synchronize(jClass.Lifetime);
		}
		public void SetAssignableTo<TDataType>(JReferenceObject jObject)
			where TDataType : JReferenceObject, IDataType<TDataType>
			=> jObject.SetAssignableTo<TDataType>(true);

		/// <summary>
		/// Retrieves class from cache or loads it using JNI.
		/// </summary>
		/// <param name="classInformation">A <see cref="ITypeInformation"/> instance.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetOrFindClass(ITypeInformation classInformation)
		{
			if (this._classes.TryGetValue(classInformation.Hash, out JClassObject? result)) return result;
			if (MetadataHelper.GetMetadata(classInformation.Hash) is { } metadata)
			{
				result = new(this.ClassObject, metadata);
			}
			else
			{
				JClassLocalRef classRef = this._objects.FindClassParameter(classInformation.Hash);
				if (classRef.Value == default)
					classRef = classInformation.ClassName.WithSafeFixed(this, JEnvironmentCache.FindClass);
				result = new(this.ClassObject, classInformation, classRef);
			}
			return this.Register(result);
		}
		/// <summary>
		/// Loads a java class from its binary information into the current VM.
		/// </summary>
		/// <param name="memoryList">
		/// A fixed memory list containing both JNI class name and class binary information.
		/// </param>
		/// <param name="args">Cache and class loader.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private static JClassObject LoadClass(ReadOnlyFixedMemoryList memoryList,
			(JEnvironmentCache cache, JLocalObject? jClassLoader) args)
		{
			ValidationUtilities.ThrowIfDummy(args.jClassLoader);
			CStringSequence classInformation = MetadataHelper.GetClassInformation(memoryList[0].Bytes);
			DefineClassDelegate defineClass = args.cache.GetDelegate<DefineClassDelegate>();
			using JniTransaction jniTransaction = args.cache.VirtualMachine.CreateTransaction();
			JObjectLocalRef localRef = jniTransaction.Add(args.jClassLoader);
			JClassLocalRef classRef = defineClass(args.cache.Reference, (ReadOnlyValPtr<Byte>)memoryList[0].Pointer,
			                                      localRef, memoryList[1].Pointer, memoryList[1].Bytes.Length);
			if (classRef.Value == default) args.cache.CheckJniError();
			if (args.cache._classes.TryGetValue(classInformation.ToString(), out JClassObject? result))
				result.SetValue(classRef);
			else
				result = new(args.cache.ClassObject, new TypeInformation(classInformation), classRef);
			return args.cache.Register(result);
		}
	}
}