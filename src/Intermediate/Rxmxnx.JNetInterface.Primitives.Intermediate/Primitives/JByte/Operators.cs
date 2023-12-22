namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JByte : IPrimitiveEquatable
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

	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Equals(other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Equals(other);

	/// <inheritdoc cref="IEquatable{JPrimitiveObject}.Equals(JPrimitiveObject)"/>
	private Boolean Equals(JPrimitiveObject? other)
	{
		if (other is null || other.ObjectSignature[0] == UnicodePrimitiveSignatures.JBooleanSignature[0])
			return false;
		if (other.ObjectSignature[0] == UnicodePrimitiveSignatures.JByteSignature[0])
			return false;
		return false;
	}
	/// <inheritdoc cref="IEquatable{IPrimitiveType}.Equals(IPrimitiveType)"/>
	private Boolean Equals(IPrimitiveType? other)
		=> other switch
		{
			JByte jByte => this == jByte,
			IWrapper<JByte> jByteWrapper => this == jByteWrapper.Value,
			IWrapper<SByte> sByteWrapper => this._value == sByteWrapper.Value,

			JChar jChar => (JChar)this == jChar,
			IWrapper<JChar> jCharWrapper => (JChar)this == jCharWrapper.Value,
			IWrapper<Char> charWrapper => (Char)this._value == charWrapper.Value,

			JDouble jDouble => this == jDouble,
			IWrapper<JDouble> jDoubleWrapper => this == jDoubleWrapper.Value,
			IWrapper<Double> doubleWrapper => this._value == (JDouble)doubleWrapper.Value,

			JFloat jFloat => this == jFloat,
			IWrapper<JFloat> jFloatWrapper => this == jFloatWrapper.Value,
			IWrapper<Single> floatWrapper => this._value == (JFloat)floatWrapper.Value,

			JInt jInt => this == jInt,
			IWrapper<JInt> jIntWrapper => this == jIntWrapper.Value,
			IWrapper<Int32> intWrapper => this._value == (JInt)intWrapper.Value,

			JLong jLong => this == jLong,
			IWrapper<JLong> jLongWrapper => this == jLongWrapper.Value,
			IWrapper<Int64> longWrapper => this._value == (JLong)longWrapper.Value,

			JShort jShort => this == jShort,
			IWrapper<JShort> jShortWrapper => this == jShortWrapper.Value,
			IWrapper<Int16> shortWrapper => this._value == (JShort)shortWrapper.Value,
			_ => false,
		};

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