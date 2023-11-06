namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JInt
{
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JByte(JInt value) => NativeUtilities.AsBytes(value).ToValue<JByte>();
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JShort(JInt value) => NativeUtilities.AsBytes(value).ToValue<JShort>();
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JInt"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to implicitly convert.</param>
	public static implicit operator JLong(JInt value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JChar(JInt value) => NativeUtilities.AsBytes(value).ToValue<JChar>();
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JInt"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to implicitly convert.</param>
	public static implicit operator JFloat(JInt value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JInt"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to implicitly convert.</param>
	public static implicit operator JDouble(JInt value) => value._value;
}