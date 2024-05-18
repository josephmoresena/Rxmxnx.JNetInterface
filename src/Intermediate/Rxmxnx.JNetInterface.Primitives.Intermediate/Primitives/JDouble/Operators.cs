namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JDouble : IPrimitiveEquatable
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => IPrimitiveNumericType.Equals(this, other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => IPrimitiveNumericType.Equals(this, other);

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
	public static explicit operator JChar(JDouble value)
		=> (JChar)IPrimitiveNumericType.GetIntegerValue<Int16>(value._value);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JDouble"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> to implicitly convert.</param>
	public static implicit operator JFloat(JDouble value) => IPrimitiveNumericType.GetSingleValue(value.Value);

	static explicit IPrimitiveNumericType<JDouble>.operator JFloat(JDouble jPrimitive) => jPrimitive;
	[ExcludeFromCodeCoverage]
	static explicit IPrimitiveNumericType<JDouble>.operator JDouble(JDouble jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JDouble>.operator SByte(JDouble jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<SByte>(jPrimitive._value);
	static explicit IPrimitiveNumericType<JDouble>.operator Int64(JDouble jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int64>(jPrimitive._value);
	static explicit IPrimitiveNumericType<JDouble>.operator Int16(JDouble jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int16>(jPrimitive._value);
	static explicit IPrimitiveNumericType<JDouble>.operator Single(JDouble jPrimitive)
		=> IPrimitiveNumericType.GetSingleValue(jPrimitive.Value);
	static explicit IPrimitiveNumericType<JDouble>.operator Int32(JDouble jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int32>(jPrimitive._value);
	static explicit IPrimitiveNumericType<JDouble>.operator Char(JDouble jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<Char>();
}