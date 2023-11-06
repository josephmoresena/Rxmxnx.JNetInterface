namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JDouble
{
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JDouble"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> to explicitly convert.</param>
	public static explicit operator JByte(JDouble value) => IPrimitiveNumericType.GetIntegerValue<SByte>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JDouble"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> to explicitly convert.</param>
	public static explicit operator JShort(JDouble value) => IPrimitiveNumericType.GetIntegerValue<Int16>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JDouble"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> to explicitly convert.</param>
	public static explicit operator JInt(JDouble value) => IPrimitiveNumericType.GetIntegerValue<Int32>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JDouble"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> to explicitly convert.</param>
	public static explicit operator JLong(JDouble value) => IPrimitiveNumericType.GetIntegerValue<Int64>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JDouble"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> to explicitly convert.</param>
	public static explicit operator JChar(JDouble value) => NativeUtilities.AsBytes(value).ToValue<JChar>();
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JDouble"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> to implicitly convert.</param>
	public static implicit operator JFloat(JDouble value) => (Single)value._value;
}