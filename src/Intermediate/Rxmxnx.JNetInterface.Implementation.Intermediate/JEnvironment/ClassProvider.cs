namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IClassProvider
	{
		JClassObject IClassProvider.JObject => this.GetClass<JLocalObject>();
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
			=> throw new NotImplementedException();
		public JClassObject GetClass(CString className)
		{
			String hash = JMetadataHelper.GetHashSequence(ref className).ToString();
			return this.GetObjectClass(className, hash);
		}
		public JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>
		{
			JDataTypeMetadata metadata = JMetadataHelper.GetMetadata<TDataType>();
			return this.GetObjectClass(metadata.ClassName, metadata.Hash);
		}
		public JClassObject GetObjectClass(JLocalObject jLocal) => throw new NotImplementedException();
		public JClassObject? GetSuperClass(JClassObject jClass) => throw new NotImplementedException();
		public Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
			=> throw new NotImplementedException();
		public JClassObject LoadClass(CString className, ReadOnlySpan<Byte> rawClassBytes,
			JLocalObject? jClassLoader = default)
			=> throw new NotImplementedException();
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
		{
			throw new NotImplementedException();
		}
		private JClassObject GetObjectClass(CString className, String hash)
		{
			if (this._classes.TryGetValue(hash, out JClassObject? result)) return result;
			IEnvironment env = this.VirtualMachine.GetEnvironment(this.Reference);
			if (JMetadataHelper.GetMetadata(hash) is { } metadata)
			{
				result = new(env, metadata, false);
			}
			else
			{
				JClassLocalRef classRef = className.WithSafeFixed(this, JEnvironmentCache.FindClass);
				result = new(env, classRef, hash, false);
			}
			return this.Register(result);
		}

		private static JClassLocalRef FindClass(in IReadOnlyFixedMemory classNameCtx, JEnvironmentCache cache)
		{
			FindClassDelegate findClass = cache.GetDelegate<FindClassDelegate>();
			JClassLocalRef result = findClass(cache.Reference, (ReadOnlyValPtr<Byte>)classNameCtx.Pointer);
			if (result.Value == default) cache.CheckJniError();
			return result;
		}
	}
}