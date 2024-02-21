namespace Rxmxnx.JNetInterface.Native.Proxies;

public abstract partial class EnvironmentProxy
{
	JClassObject IClassFeature.VoidPrimitive => new(this.ClassObject, JPrimitiveTypeMetadata.VoidMetadata);
	JClassObject IClassFeature.BooleanPrimitive => this.GetClass<JBoolean>();
	JClassObject IClassFeature.BytePrimitive => this.GetClass<JByte>();
	JClassObject IClassFeature.CharPrimitive => this.GetClass<JChar>();
	JClassObject IClassFeature.DoublePrimitive => this.GetClass<JDouble>();
	JClassObject IClassFeature.FloatPrimitive => this.GetClass<JFloat>();
	JClassObject IClassFeature.IntPrimitive => this.GetClass<JInt>();
	JClassObject IClassFeature.LongPrimitive => this.GetClass<JLong>();
	JClassObject IClassFeature.ShortPrimitive => this.GetClass<JShort>();

	JClassObject IClassFeature.VoidObject => this.GetClass<JVoidObject>();
	JClassObject IClassFeature.BooleanObject => this.GetClass<JBooleanObject>();
	JClassObject IClassFeature.ByteObject => this.GetClass<JByteObject>();
	JClassObject IClassFeature.CharacterObject => this.GetClass<JCharacterObject>();
	JClassObject IClassFeature.DoubleObject => this.GetClass<JDoubleObject>();
	JClassObject IClassFeature.FloatObject => this.GetClass<JFloatObject>();
	JClassObject IClassFeature.IntegerObject => this.GetClass<JIntegerObject>();
	JClassObject IClassFeature.LongObject => this.GetClass<JLongObject>();
	JClassObject IClassFeature.ShortObject => this.GetClass<JShortObject>();

	JClassObject IClassFeature.GetClass(ReadOnlySpan<Byte> className) => this.GetClass(new(className));
	JClassObject IClassFeature.GetClass(String classHash)
	{
		ReadOnlySpan<Byte> classInformation = classHash.AsSpan().AsBytes();
		Int32 classNameLength = ITypeInformation.GetSegmentLength(classInformation, 0);
		return this.GetClass(new(classInformation[..classNameLength]));
	}
	JClassObject IClassFeature.LoadClass(CString className, ReadOnlySpan<Byte> rawClassBytes,
		JClassLoaderObject? jClassLoader)
		=> this.LoadClass(className, rawClassBytes.ToArray(), jClassLoader);
	JClassObject IClassFeature.LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader)
		=> this.LoadClass<TDataType>(rawClassBytes.ToArray(), jClassLoader);
	void IClassFeature.GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
	{
		ITypeInformation information = this.GetClassInfo(jClass);
		name = information.ClassName;
		signature = information.Signature;
		hash = information.Hash;
	}
}