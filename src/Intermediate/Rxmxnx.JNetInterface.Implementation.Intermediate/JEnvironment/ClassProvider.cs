namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IClassProvider
	{
		JClassObject IClassProvider.BooleanClassObject => this.GetClass<JBooleanObject>();
		JClassObject IClassProvider.ByteClassObject => this.GetClass<JByteObject>();
		JClassObject IClassProvider.CharacterClassObject => this.GetClass<JCharacterObject>();
		JClassObject IClassProvider.DoubleClassObject => this.GetClass<JDoubleObject>();
		JClassObject IClassProvider.FloatClassObject => this.GetClass<JFloatObject>();
		JClassObject IClassProvider.IntegerClassObject => this.GetClass<JIntegerObject>();
		JClassObject IClassProvider.LongClassObject => this.GetClass<JLongObject>();
		JClassObject IClassProvider.ShortClassObject => this.GetClass<JShortObject>();
		
		public Boolean IsAssignableTo<TDataType>(JReferenceObject jObject)
			where TDataType : JReferenceObject, IDataType<TDataType>
			=> throw new NotImplementedException();
		public JClassObject GetClass(CString className) => throw new NotImplementedException();
		public JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>
			=> throw new NotImplementedException();
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
	}
}