namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JByte : IPrimitiveEquatable
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => IPrimitiveNumericType.Equals(this, other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => IPrimitiveNumericType.Equals(this, other);

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

	[ExcludeFromCodeCoverage]
	static explicit IPrimitiveNumericType<JByte>.operator JByte(JByte jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JByte>.operator JDouble(JByte jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JByte>.operator JFloat(JByte jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JByte>.operator Single(JByte jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JByte>.operator JInt(JByte jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JByte>.operator Int32(JByte jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JByte>.operator JLong(JByte jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JByte>.operator Int64(JByte jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JByte>.operator JShort(JByte jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JByte>.operator Int16(JByte jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JByte>.operator SByte(JByte jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JByte>.operator Char(JByte jPrimitive) => (Char)jPrimitive._value;
}