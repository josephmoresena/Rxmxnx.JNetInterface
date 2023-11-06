namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JByte
{
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JByte"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> to implicitly convert.</param>
	public static implicit operator JShort(JByte value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JByte"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> to implicitly convert.</param>
	public static implicit operator JInt(JByte value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JByte"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> to implicitly convert.</param>
	public static implicit operator JLong(JByte value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JByte"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> to explicitly convert.</param>
	public static explicit operator JChar(JByte value) => (Char)value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JByte"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> to implicitly convert.</param>
	public static implicit operator JFloat(JByte value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JByte"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> to implicitly convert.</param>
	public static implicit operator JDouble(JByte value) => value._value;
}