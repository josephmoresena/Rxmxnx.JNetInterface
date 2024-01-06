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

	JClassObject IClassProvider.VoidPrimitive => new(this.ClassObject, JPrimitiveTypeMetadata.VoidMetadata);
	JClassObject IClassProvider.BooleanPrimitive => this.GetClass<JBoolean>();
	JClassObject IClassProvider.BytePrimitive => this.GetClass<JByte>();
	JClassObject IClassProvider.CharPrimitive => this.GetClass<JChar>();
	JClassObject IClassProvider.DoublePrimitive => this.GetClass<JDouble>();
	JClassObject IClassProvider.FloatPrimitive => this.GetClass<JFloat>();
	JClassObject IClassProvider.IntPrimitive => this.GetClass<JInt>();
	JClassObject IClassProvider.LongPrimitive => this.GetClass<JLong>();
	JClassObject IClassProvider.ShortPrimitive => this.GetClass<JShort>();

	JClassObject IClassProvider.VoidObject => this.GetClass<JVoidObject>();
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

	JLocalObject IReferenceProvider.CreateWrapper<TPrimitive>(TPrimitive primitive)
	{
		Type typeofT = typeof(TPrimitive);
		if (typeofT == typeof(JBoolean))
			return this.CreateWrapper((JBoolean)(Object)primitive);
		if (typeofT == typeof(JByte))
			return this.CreateWrapper((JByte)(Object)primitive);
		if (typeofT == typeof(JChar))
			return this.CreateWrapper((JChar)(Object)primitive);
		if (typeofT == typeof(JDouble))
			return this.CreateWrapper((JDouble)(Object)primitive);
		if (typeofT == typeof(JFloat))
			return this.CreateWrapper((JFloat)(Object)primitive);
		if (typeofT == typeof(JInt))
			return this.CreateWrapper((JInt)(Object)primitive);
		if (typeofT == typeof(JLong))
			return this.CreateWrapper((JLong)(Object)primitive);
		return this.CreateWrapper((JShort)(Object)primitive);
	}

	/// <summary>
	/// Creates a <see cref="JBooleanObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JBoolean"/> value.</param>
	/// <returns>A <see cref="JBooleanObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JBooleanObject CreateWrapper(JBoolean value);
	/// <summary>
	/// Creates a <see cref="JByteObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> value.</param>
	/// <returns>A <see cref="JByteObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JByteObject CreateWrapper(JByte value);
	/// <summary>
	/// Creates a <see cref="JCharacterObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> value.</param>
	/// <returns>A <see cref="JCharacterObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JCharacterObject CreateWrapper(JChar value);
	/// <summary>
	/// Creates a <see cref="JDoubleObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> value.</param>
	/// <returns>A <see cref="JDoubleObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JDoubleObject CreateWrapper(JDouble value);
	/// <summary>
	/// Creates a <see cref="JFloatObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> value.</param>
	/// <returns>A <see cref="JFloatObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JFloatObject CreateWrapper(JFloat value);
	/// <summary>
	/// Creates a <see cref="JIntegerObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> value.</param>
	/// <returns>A <see cref="JIntegerObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JIntegerObject CreateWrapper(JInt value);
	/// <summary>
	/// Creates a <see cref="JLongObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> value.</param>
	/// <returns>A <see cref="JLongObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JLongObject CreateWrapper(JLong value);
	/// <summary>
	/// Creates a <see cref="JShortObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> value.</param>
	/// <returns>A <see cref="JShortObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JShortObject CreateWrapper(JShort value);
}