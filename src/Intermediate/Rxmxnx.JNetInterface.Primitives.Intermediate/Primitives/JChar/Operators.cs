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
}