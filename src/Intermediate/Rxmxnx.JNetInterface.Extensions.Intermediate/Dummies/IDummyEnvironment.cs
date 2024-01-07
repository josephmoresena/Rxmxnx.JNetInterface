namespace Rxmxnx.JNetInterface.Native.Dummies;

/// <summary>
/// This interface exposes a JNI dummy instance.
/// </summary>
public interface IDummyEnvironment : IEnvironment, IAccessFeature, IClassFeature, IReferenceFeature, IStringFeature,
	IArrayFeature
{
	TObject IAccessFeature.CallInternalConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		IObject?[] args)
		=> this.CallConstructor<TObject>(jClass, definition, args.Normalize());
	TResult IAccessFeature.CallInternalStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		IObject?[] args)
		=> this.CallStaticFunction<TResult>(jClass, definition, args.Normalize())!;
	void IAccessFeature.CallInternalStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args)
		=> this.CallStaticMethod(jClass, definition, args.Normalize());
	TResult IAccessFeature.CallInternalFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args)
		=> this.CallFunction<TResult>(jLocal, jClass, definition, nonVirtual, args.Normalize())!;
	void IAccessFeature.CallInternalMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args)
		=> this.CallMethod(jLocal, jClass, definition, nonVirtual, args.Normalize());

	void IAccessFeature.GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFieldDefinition definition)
		=> definition.PrimitiveGet(bytes, jLocal, jClass);
	void IAccessFeature.SetPrimitiveField(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> definition.PrimitiveSet(jLocal, jClass, bytes);
	void IAccessFeature.GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		=> definition.PrimitiveStaticGet(bytes, jClass);
	void IAccessFeature.SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> definition.PrimitiveStaticSet(jClass, bytes);

	void IAccessFeature.CallPrimitiveStaticFunction(Span<Byte> bytes, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args)
		=> definition.PrimitiveStaticInvoke(bytes, jClass, args);
	void IAccessFeature.CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args)
		=> definition.PrimitiveInvoke(bytes, jLocal, jClass, nonVirtual, args);

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

	IAccessFeature IEnvironment.AccessFeature => this;
	IClassFeature IEnvironment.ClassFeature => this;
	IReferenceFeature IEnvironment.ReferenceFeature => this;
	IStringFeature IEnvironment.StringFeature => this;
	IArrayFeature IEnvironment.ArrayFeature => this;

	JLocalObject IReferenceFeature.CreateWrapper<TPrimitive>(TPrimitive primitive)
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