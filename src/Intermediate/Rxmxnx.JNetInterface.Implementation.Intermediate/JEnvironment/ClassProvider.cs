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
		JClassObject IClassProvider.BooleanClassObject => this.GetClass<JBooleanObject>();
		JClassObject IClassProvider.ByteClassObject => this.GetClass<JByteObject>();
		JClassObject IClassProvider.CharacterClassObject => this.GetClass<JCharacterObject>();
		JClassObject IClassProvider.DoubleClassObject => this.GetClass<JDoubleObject>();
		JClassObject IClassProvider.FloatClassObject => this.GetClass<JFloatObject>();
		JClassObject IClassProvider.IntegerClassObject => this.GetClass<JIntegerObject>();
		JClassObject IClassProvider.LongClassObject => this.GetClass<JLongObject>();
		JClassObject IClassProvider.ShortClassObject => this.GetClass<JShortObject>();

		public JClassObject AsClassObject(JReferenceObject jObject) => throw new NotImplementedException();
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
			String hash = MetadataHelper.GetClassInformation(ref className).ToString();
			return this.GetClass(className, hash);
		}
		public JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TDataType>();
			return this.GetClass(metadata.ClassName, metadata.Hash);
		}
		public JClassObject GetObjectClass(JLocalObject jLocal) => this.GetClass(jLocal.ObjectClassName);
		public JClassObject? GetSuperClass(JClassObject jClass)
		{
			if (MetadataHelper.GetMetadata(jClass.Hash)?.BaseMetadata is { } metadata)
				return this.GetClass(metadata.ClassName, metadata.Hash);
			ValidationUtilities.ThrowIfDummy(jClass);
			JClassLocalRef classRef = this.ReloadClass(jClass);
			ValidationUtilities.ThrowIfDefault(jClass);
			GetSuperclassDelegate getSuperClass = this.GetDelegate<GetSuperclassDelegate>();
			JClassLocalRef superClassRef = getSuperClass(this.Reference, classRef);
			if (superClassRef.Value != default)
				return this.VirtualMachine.GetEnvironment(this.Reference).GetClass(superClassRef, true);
			this.CheckJniError();
			return default;
		}
		public Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
		{
			Boolean? result = MetadataHelper.IsAssignableFrom(jClass, otherClass);
			if (result.HasValue) return result.Value;
			ValidationUtilities.ThrowIfDummy(jClass);
			ValidationUtilities.ThrowIfDummy(otherClass);
			JClassLocalRef classRef = this.ReloadClass(jClass);
			JClassLocalRef otherClassRef = this.ReloadClass(otherClass);
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
		public JClassObject LoadClass(CString className, Stream rawClassBytes, JLocalObject? jClassLoader = default)
			=> throw new NotImplementedException();
		public JClassObject LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes, JLocalObject? jClassLoader = default)
			=> throw new NotImplementedException();
		public JClassObject LoadClass<TDataType>(Stream rawClassBytes, JLocalObject? jClassLoader = default)
			=> throw new NotImplementedException();
		public void GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
		{
			throw new NotImplementedException();
		}
		public void SetAssignableTo<TDataType>(JReferenceObject jObject)
			where TDataType : JReferenceObject, IDataType<TDataType>
			=> jObject.SetAssignableTo<TDataType>(true);

		private JClassObject GetClass(CString className, String hash)
		{
			if (this._classes.TryGetValue(hash, out JClassObject? result)) return result;
			JEnvironment env = this.VirtualMachine.GetEnvironment(this.Reference);
			if (MetadataHelper.GetMetadata(hash) is { } metadata)
			{
				result = new(env._cache.ClassObject, metadata);
			}
			else
			{
				JClassLocalRef classRef = className.WithSafeFixed(this, JEnvironmentCache.FindClass);
				result = new(env, classRef, hash, false);
			}
			return this.Register(result);
		}
		private static JClassObject LoadClass(ReadOnlyFixedMemoryList memoryList,
			(JEnvironmentCache cache, JLocalObject? classLoader) args)
		{
			ValidationUtilities.ThrowIfDummy(args.classLoader);
			CStringSequence classInformation = MetadataHelper.GetClassInformation(memoryList[0].Bytes);
			DefineClassDelegate defineClass = args.cache.GetDelegate<DefineClassDelegate>();
			JObjectLocalRef localRef = args.classLoader?.To<JObjectLocalRef>() ?? default;
			JClassLocalRef classRef = defineClass(args.cache.Reference, (ReadOnlyValPtr<Byte>)memoryList[0].Pointer,
			                                      localRef, memoryList[1].Pointer, memoryList[1].Bytes.Length);
			if (classRef.Value == default) args.cache.CheckJniError();
			if (args.cache._classes.TryGetValue(classInformation.ToString(), out JClassObject? result))
				result.SetValue(classRef);
			else
				result = new(args.cache.ClassObject, new TypeInformation(classInformation));
			return args.cache.Register(result);
		}
	}
}