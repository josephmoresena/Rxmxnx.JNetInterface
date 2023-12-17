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
			=> throw new NotImplementedException();
		public JClassObject GetClass(CString className)
		{
			String hash = MetadataHelper.GetHashSequence(ref className).ToString();
			return this.GetObjectClass(className, hash);
		}
		public JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TDataType>();
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

		public CStringSequence GetClassInfo(JClassLocalRef classRef)
		{
			JClassObject jClassObject = this.ClassObject;
			//this._classes[jClassObject.As<>()]
			return default!;
		}
		private void ReloadClass(JClassObject jClass)
		{
			if (!jClass.IsDefault) return;
			JClassLocalRef classRef = jClass.Reference;
			//this._classes[]
		}

		private JClassObject GetObjectClass(CString className, String hash)
		{
			if (this._classes.TryGetValue(hash, out JClassObject? result)) return result;
			JEnvironment env = this.VirtualMachine.GetEnvironment(this.Reference);
			if (MetadataHelper.GetMetadata(hash) is { } metadata)
				result = new(env._cache.ClassObject, metadata);
			else
			{
				JClassLocalRef classRef = className.WithSafeFixed(this, JEnvironmentCache.FindClass);
				result = new(env, classRef, hash, false);
			}
			return this.Register(result);
		}
	}
}