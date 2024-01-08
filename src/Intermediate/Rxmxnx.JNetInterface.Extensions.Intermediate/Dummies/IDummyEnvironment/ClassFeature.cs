namespace Rxmxnx.JNetInterface.Native.Dummies;

public partial interface IDummyEnvironment
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
}