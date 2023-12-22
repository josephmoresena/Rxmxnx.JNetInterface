namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JFloat
{
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JByte(JFloat value) => IPrimitiveNumericType.GetIntegerValue<SByte>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JShort(JFloat value) => IPrimitiveNumericType.GetIntegerValue<Int16>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JInt(JFloat value) => IPrimitiveNumericType.GetIntegerValue<Int32>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JLong(JFloat value) => IPrimitiveNumericType.GetIntegerValue<Int64>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JChar(JFloat value) => NativeUtilities.AsBytes(value).ToValue<JChar>();
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JFloat"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to implicitly convert.</param>
	public static implicit operator JDouble(JFloat value) => value._value;

	static explicit IPrimitiveNumericType<JFloat>.operator JFloat(JFloat jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JFloat>.operator JDouble(JFloat jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JFloat>.operator Int16(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int16>(jPrimitive._value);
	static explicit IPrimitiveNumericType<JFloat>.operator Single(JFloat jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JFloat>.operator Int32(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int32>(jPrimitive._value);
	static explicit IPrimitiveNumericType<JFloat>.operator Char(JFloat jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<Char>();
	static explicit IPrimitiveNumericType<JFloat>.operator SByte(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<SByte>(jPrimitive._value);
	static explicit IPrimitiveNumericType<JFloat>.operator Int64(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int64>(jPrimitive._value);
}