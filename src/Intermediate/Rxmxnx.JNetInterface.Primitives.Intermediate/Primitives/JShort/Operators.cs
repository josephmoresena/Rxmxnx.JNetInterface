namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JShort
{
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JShort"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to explicitly convert.</param>
	public static explicit operator JByte(JShort value) => NativeUtilities.AsBytes(value).ToValue<JByte>();
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JShort"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to explicitly convert.</param>
	public static implicit operator JInt(JShort value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JShort"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to implicitly convert.</param>
	public static implicit operator JLong(JShort value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JShort"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to explicitly convert.</param>
	public static explicit operator JChar(JShort value) => (Char)value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JShort"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to implicitly convert.</param>
	public static implicit operator JFloat(JShort value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JShort"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to implicitly convert.</param>
	public static implicit operator JDouble(JShort value) => value._value;
}