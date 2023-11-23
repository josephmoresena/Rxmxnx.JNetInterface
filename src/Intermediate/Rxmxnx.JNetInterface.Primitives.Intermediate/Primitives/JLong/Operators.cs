namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JLong
{
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JLong"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> to explicitly convert.</param>
	public static explicit operator JByte(JLong value) => NativeUtilities.AsBytes(value).ToValue<JByte>();
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JLong"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> to explicitly convert.</param>
	public static explicit operator JShort(JLong value) => NativeUtilities.AsBytes(value).ToValue<JShort>();
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JLong"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> to explicitly convert.</param>
	public static explicit operator JInt(JLong value) => NativeUtilities.AsBytes(value).ToValue<JInt>();
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JLong"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> to explicitly convert.</param>
	public static explicit operator JChar(JLong value) => NativeUtilities.AsBytes(value).ToValue<JChar>();
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JLong"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> to implicitly convert.</param>
	public static implicit operator JFloat(JLong value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JLong"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> to implicitly convert.</param>
	public static implicit operator JDouble(JLong value) => value._value;

	static explicit IPrimitiveNumericType<JLong>.operator JLong(JLong jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JLong>.operator JFloat(JLong jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JLong>.operator JDouble(JLong jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JLong>.operator SByte(JLong jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<SByte>();
	static explicit IPrimitiveNumericType<JLong>.operator Int64(JLong jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JLong>.operator Int16(JLong jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<Int16>();
	static explicit IPrimitiveNumericType<JLong>.operator Single(JLong jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JLong>.operator Int32(JLong jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<Int32>();
	static explicit IPrimitiveNumericType<JLong>.operator Char(JLong jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<Char>();
}