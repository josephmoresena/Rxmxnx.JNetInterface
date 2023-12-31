namespace Rxmxnx.JNetInterface.Native.Dummies;

/// <summary>
/// This interface exposes a JNI dummy instance.
/// </summary>
public interface IDummyEnvironment : IEnvironment, IAccessProvider, IClassProvider, IReferenceProvider, IStringProvider,
	IArrayProvider
{
	TObject IAccessProvider.CallInternalConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		IObject?[] args)
		=> this.CallConstructor<TObject>(jClass, definition, args.Normalize());
	TResult IAccessProvider.CallInternalStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		IObject?[] args)
		=> this.CallStaticFunction<TResult>(jClass, definition, args.Normalize())!;
	void IAccessProvider.CallInternalStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args)
		=> this.CallStaticMethod(jClass, definition, args.Normalize());
	TResult IAccessProvider.CallInternalFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args)
		=> this.CallFunction<TResult>(jLocal, jClass, definition, nonVirtual, args.Normalize())!;
	void IAccessProvider.CallInternalMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args)
		=> this.CallMethod(jLocal, jClass, definition, nonVirtual, args.Normalize());

	void IAccessProvider.GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFieldDefinition definition)
		=> definition.PrimitiveGet(bytes, jLocal, jClass);
	void IAccessProvider.SetPrimitiveField(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> definition.PrimitiveSet(jLocal, jClass, bytes);
	void IAccessProvider.GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		=> definition.PrimitiveStaticGet(bytes, jClass);
	void IAccessProvider.SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> definition.PrimitiveStaticSet(jClass, bytes);

	void IAccessProvider.CallPrimitiveStaticFunction(Span<Byte> bytes, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args)
		=> definition.PrimitiveStaticInvoke(bytes, jClass, args);
	void IAccessProvider.CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args)
		=> definition.PrimitiveInvoke(bytes, jLocal, jClass, nonVirtual, args);
	
	JClassObject IClassProvider.BooleanPrimitive => this.GetClass<JBoolean>();
	JClassObject IClassProvider.BytePrimitive => this.GetClass<JByte>();
	JClassObject IClassProvider.CharPrimitive => this.GetClass<JChar>();
	JClassObject IClassProvider.DoublePrimitive => this.GetClass<JDouble>();
	JClassObject IClassProvider.FloatPrimitive => this.GetClass<JFloat>();
	JClassObject IClassProvider.IntPrimitive => this.GetClass<JInt>();
	JClassObject IClassProvider.LongPrimitive => this.GetClass<JLong>();
	JClassObject IClassProvider.ShortPrimitive => this.GetClass<JShort>();
	
	JClassObject IClassProvider.BooleanObject => this.GetClass<JBooleanObject>();
	JClassObject IClassProvider.ByteObject => this.GetClass<JByteObject>();
	JClassObject IClassProvider.CharacterObject => this.GetClass<JCharacterObject>();
	JClassObject IClassProvider.DoubleObject => this.GetClass<JDoubleObject>();
	JClassObject IClassProvider.FloatObject => this.GetClass<JFloatObject>();
	JClassObject IClassProvider.IntegerObject => this.GetClass<JIntegerObject>();
	JClassObject IClassProvider.LongObject => this.GetClass<JLongObject>();
	JClassObject IClassProvider.ShortObject => this.GetClass<JShortObject>();

	IAccessProvider IEnvironment.AccessProvider => this;
	IClassProvider IEnvironment.ClassProvider => this;
	IReferenceProvider IEnvironment.ReferenceProvider => this;
	IStringProvider IEnvironment.StringProvider => this;
	IArrayProvider IEnvironment.ArrayProvider => this;
}