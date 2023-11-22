namespace Rxmxnx.JNetInterface.Native.Dummies;

/// <summary>
/// This interface exposes a JNI dummy instance.
/// </summary>
public interface IDummyEnvironment : IEnvironment, IAccessProvider, IClassProvider, IReferenceProvider, IStringProvider,
	IEnumProvider, IArrayProvider
{
	JClassObject IClassProvider.BooleanClassObject => this.GetClass<JBooleanObject>();
	JClassObject IClassProvider.ByteClassObject => this.GetClass<JByteObject>();
	JClassObject IClassProvider.CharacterClassObject => this.GetClass<JCharacterObject>();
	JClassObject IClassProvider.DoubleClassObject => this.GetClass<JDoubleObject>();
	JClassObject IClassProvider.FloatClassObject => this.GetClass<JFloatObject>();
	JClassObject IClassProvider.IntegerClassObject => this.GetClass<JIntegerObject>();
	JClassObject IClassProvider.LongClassObject => this.GetClass<JLongObject>();
	JClassObject IClassProvider.ShortClassObject => this.GetClass<JShortObject>();
	
	TObject IAccessProvider.CallInternalConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		IObject?[] args)
		=> this.CallConstructor<TObject>(jClass, definition, args.Normalize());
	TResult IAccessProvider.CallInternalStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		IObject?[] args)
		=> this.CallStaticFunction<TResult>(jClass, definition, args.Normalize())!;
	void IAccessProvider.CallInternalStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args)
		=> this.CallStaticMethod(jClass, definition, args.Normalize());
	TResult IAccessProvider.CallInternalFunction<TResult>(JLocalObject jLocal, JFunctionDefinition definition,
		IObject?[] args)
		=> this.CallFunction<TResult>(jLocal, definition, args.Normalize())!;
	TResult IAccessProvider.CallInternalNonVirtualFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args)
		=> this.CallNonVirtualFunction<TResult>(jLocal, jClass, definition, args.Normalize())!;
	void IAccessProvider.CallInternalMethod(JLocalObject jLocal, JMethodDefinition definition, IObject?[] args)
		=> this.CallMethod(jLocal, definition, args.Normalize());
	void IAccessProvider.CallInternalNonVirtualMethod(JLocalObject jLocal, JClassObject jClass,
		JMethodDefinition definition, IObject?[] args)
		=> this.CallNonVirtualMethod(jLocal, jClass, definition, args.Normalize());

	void IAccessProvider.GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JFieldDefinition definition)
		=> definition.PrimitiveGet(bytes, jLocal);
	void IAccessProvider.SetPrimitiveField(JLocalObject jLocal, JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		=> definition.PrimitiveSet(jLocal, bytes);
	void IAccessProvider.GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		=> definition.PrimitiveStaticGet(bytes, jClass);
	void IAccessProvider.SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> definition.PrimitiveStaticSet(jClass, bytes);

	void IAccessProvider.CallPrimitiveStaticFunction(Span<Byte> bytes, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args)
		=> definition.PrimitiveStaticInvoke(bytes, jClass, args);
	void IAccessProvider.CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JFunctionDefinition definition,
		IObject?[] args)
		=> definition.PrimitiveInvoke(bytes, jLocal, args);
	void IAccessProvider.CallPrimitiveNonVirtualFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args)
		=> definition.PrimitiveInvoke(bytes, jLocal, jClass, args);
	
	IAccessProvider IEnvironment.AccessProvider => this;
	IClassProvider IEnvironment.ClassProvider => this;
	IReferenceProvider IEnvironment.ReferenceProvider => this;
	IStringProvider IEnvironment.StringProvider => this;
	IEnumProvider IEnvironment.EnumProvider => this;
	IArrayProvider IEnvironment.ArrayProvider => this;
}