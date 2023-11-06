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
}