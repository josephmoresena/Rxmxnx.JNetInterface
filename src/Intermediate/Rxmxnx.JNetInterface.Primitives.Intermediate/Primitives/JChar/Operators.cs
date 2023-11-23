namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JChar
{
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JByte(JChar value) => (SByte)value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JShort(JChar value) => (Int16)value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JInt(JChar value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JLong(JChar value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JFloat(JChar value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JDouble(JChar value) => value._value;

	static explicit IPrimitiveNumericType<JChar>.operator JChar(JChar jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JChar>.operator SByte(JChar jPrimitive) => (SByte)jPrimitive._value;
	static explicit IPrimitiveNumericType<JChar>.operator Int64(JChar jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JChar>.operator Char(JChar jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JChar>.operator Int32(JChar jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JChar>.operator Int16(JChar jPrimitive) => (Int16)jPrimitive._value;
	static explicit IPrimitiveNumericType<JChar>.operator Single(JChar jPrimitive) => jPrimitive._value;
}