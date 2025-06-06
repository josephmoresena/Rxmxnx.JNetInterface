namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JFloat : IPrimitiveEquatable
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => IPrimitiveNumericType.Equals(this, other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => IPrimitiveNumericType.Equals(this, other);

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JByte(JFloat value)
		=> IPrimitiveNumericType.GetIntegerValue<JByte, Single>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JShort(JFloat value)
		=> IPrimitiveNumericType.GetIntegerValue<JShort, Single>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JInt(JFloat value)
		=> IPrimitiveNumericType.GetIntegerValue<JInt, Single>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JLong(JFloat value)
		=> IPrimitiveNumericType.GetIntegerValue<JLong, Single>(value._value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JFloat"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to explicitly convert.</param>
	public static explicit operator JChar(JFloat value)
		=> IPrimitiveNumericType.GetIntegerValue<JChar, Single>(value._value);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JFloat"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> to implicitly convert.</param>
	public static implicit operator JDouble(JFloat value) => value.Value;

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit IPrimitiveNumericType<JFloat>.operator JFloat(JFloat jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JFloat>.operator JDouble(JFloat jPrimitive) => jPrimitive;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit IPrimitiveNumericType<JFloat>.operator Int16(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int16, Single>(jPrimitive._value);
	static explicit IPrimitiveNumericType<JFloat>.operator Single(JFloat jPrimitive) => jPrimitive._value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit IPrimitiveNumericType<JFloat>.operator Int32(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int32, Single>(jPrimitive._value);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit IPrimitiveNumericType<JFloat>.operator Char(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Char, Single>(jPrimitive._value);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit IPrimitiveNumericType<JFloat>.operator SByte(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<SByte, Single>(jPrimitive._value);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit IPrimitiveNumericType<JFloat>.operator Int64(JFloat jPrimitive)
		=> IPrimitiveNumericType.GetIntegerValue<Int64, Single>(jPrimitive._value);
}