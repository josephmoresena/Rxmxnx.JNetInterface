namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveNumericType : IPrimitiveType
{
	/// <summary>
	/// Retrieves the integer part of a <see cref="Double"/> value.
	/// </summary>
	/// <typeparam name="TInteger">Integer type.</typeparam>
	/// <param name="value">A <see cref="Double"/> value.</param>
	/// <returns>A <typeparamref name="TInteger"/> type.</returns>
	public static TInteger GetIntegerValue<TInteger>(Double value) where TInteger : unmanaged, IBinaryInteger<TInteger>
	{
		Int64 result = (Int64)value;
		return NativeUtilities.AsBytes(result).ToValue<TInteger>();
	}

	/// <inheritdoc cref="IEquatable{JPrimitiveObject}.Equals(JPrimitiveObject)"/>
	public static Boolean Equals<TPrimitive>(TPrimitive primitive, JPrimitiveObject? other)
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
	{
		if (other is null || other.ObjectSignature[0] == 0x90 /*Z*/)
			return false;
		return other.ObjectSignature[0] switch
		{
			0x66 => //B
				(JByte)primitive == other.AsPrimitive<JByte, SByte>(),
			0x67 => //C
				(JChar)primitive == other.AsPrimitive<JChar, Char>(),
			0x68 => //D
				(JDouble)primitive == other.AsPrimitive<JDouble, Double>(),
			0x70 => //F
				(JFloat)primitive == other.AsPrimitive<JFloat, Single>(),
			0x73 => //I
				(JInt)primitive == other.AsPrimitive<JInt, Int32>(),
			0x74 => //J
				(JLong)primitive == other.AsPrimitive<JLong, Int64>(),
			0x83 => //S
				(JShort)primitive == other.AsPrimitive<JShort, Int16>(),
			_ => false,
		};
	}
	/// <inheritdoc cref="IEquatable{IPrimitiveType}.Equals(IPrimitiveType)"/>
	public static Boolean Equals<TPrimitive>(TPrimitive primitive, IPrimitiveType? other)
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
		=> other switch
		{
			JByte jByte => (JByte)primitive == jByte,
			IWrapper<JByte> jByteWrapper => (JByte)primitive == jByteWrapper.Value,
			IWrapper<SByte> sByteWrapper => (SByte)primitive == sByteWrapper.Value,

			JChar jChar => (JChar)primitive == jChar,
			IWrapper<JChar> jCharWrapper => (JChar)primitive == jCharWrapper.Value,
			IWrapper<Char> charWrapper => (Char)primitive == charWrapper.Value,

			JDouble jDouble => (JDouble)primitive == jDouble,
			IWrapper<JDouble> jDoubleWrapper => (JDouble)primitive == jDoubleWrapper.Value,
			IWrapper<Double> doubleWrapper => (Double)primitive == (JDouble)doubleWrapper.Value,

			JFloat jFloat => (JFloat)primitive == jFloat,
			IWrapper<JFloat> jFloatWrapper => (JFloat)primitive == jFloatWrapper.Value,
			IWrapper<Single> floatWrapper => (Single)primitive == (JFloat)floatWrapper.Value,

			JInt jInt => (JInt)primitive == jInt,
			IWrapper<JInt> jIntWrapper => (JInt)primitive == jIntWrapper.Value,
			IWrapper<Int32> intWrapper => (Int32)primitive == (JInt)intWrapper.Value,

			JLong jLong => (JLong)primitive == jLong,
			IWrapper<JLong> jLongWrapper => (JLong)primitive == jLongWrapper.Value,
			IWrapper<Int64> longWrapper => (Int64)primitive == (JLong)longWrapper.Value,

			JShort jShort => (JShort)primitive == jShort,
			IWrapper<JShort> jShortWrapper => (JShort)primitive == jShortWrapper.Value,
			IWrapper<Int16> shortWrapper => (Int16)primitive == (JShort)shortWrapper.Value,
			_ => false,
		};
}

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
internal interface IPrimitiveNumericType<TPrimitive> : IPrimitiveNumericType
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
{
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JByte(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="SByte"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator SByte(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JChar"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JChar(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="Char"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator Char(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JDouble(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JFloat(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="Single"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator Single(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JInt(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="Int32"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator Int32(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JLong(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="Int64"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator Int64(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JShort(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="Int16"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator Int16(TPrimitive jPrimitive);

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="Double"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> to explicitly convert.</param>
	static virtual explicit operator Double(TPrimitive value) => TPrimitive.ToDouble(value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="Double"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> to explicitly convert.</param>
	static virtual explicit operator TPrimitive(Double value) => TPrimitive.FromDouble(value);

	/// <summary>
	/// Creates a <typeparamref name="TPrimitive"/> value from <see cref="Double"/>.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> value.</param>
	/// <returns>A <typeparamref name="TPrimitive"/> value.</returns>
	protected static abstract TPrimitive FromDouble(Double value);
	/// <summary>
	/// Creates a <see cref="Double"/> value from <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TPrimitive"/> value.</param>
	/// <returns>A <see cref="Double"/> value.</returns>
	protected static abstract Double ToDouble(TPrimitive value);
}

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent number.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
internal partial interface
	IPrimitiveNumericType<TPrimitive, TValue> : IPrimitiveNumericType<TPrimitive>, INumericValue<TValue>,
	IPrimitiveType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue> { }