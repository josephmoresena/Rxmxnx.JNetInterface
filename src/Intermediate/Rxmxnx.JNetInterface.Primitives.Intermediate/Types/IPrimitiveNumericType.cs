namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveNumericType : IPrimitiveType
{
	/// <summary>
	/// Retrieves the integer part of a <see cref="Double"/> value.
	/// </summary>
	/// <typeparam name="TInteger">Integer type.</typeparam>
	/// <param name="value">A <see cref="Double"/> value.</param>
	/// <returns>A <typeparamref name="TInteger"/> type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static TInteger GetIntegerValue<TInteger>(Double value)
		where TInteger : unmanaged, IBinaryInteger<TInteger>
	{
		try
		{
			Int64 result = value switch
			{
				SByte.MinValue => SByte.MinValue,
				SByte.MaxValue => SByte.MaxValue,
				Int16.MinValue => Int16.MinValue,
				Int16.MaxValue => Int16.MaxValue,
				Int32.MinValue => Int32.MinValue,
				Int32.MaxValue => Int32.MaxValue,
				Int64.MinValue => Int64.MinValue,
				Int64.MaxValue => Int64.MaxValue,
				Double.MinValue => 0L,
				Double.MaxValue => -1L,
				Single.MinValue => 0L,
				Single.MaxValue => -1L,
				Single.PositiveInfinity => -1L,
				_ => (Int64)value,
			};
			return NativeUtilities.AsBytes(result).ToValue<TInteger>();
		}
		catch (Exception)
		{
			return Double.IsNegative(value) ? TInteger.Zero : TInteger.AllBitsSet;
		}
	}
	/// <summary>
	/// Retrieves the integer part of a <see cref="Single"/> value.
	/// </summary>
	/// <typeparam name="TInteger">Integer type.</typeparam>
	/// <param name="value">A <see cref="Single"/> value.</param>
	/// <returns>A <typeparamref name="TInteger"/> type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static TInteger GetIntegerValue<TInteger>(Single value)
		where TInteger : unmanaged, IBinaryInteger<TInteger>
	{
		try
		{
			Int64 result = value switch
			{
				SByte.MinValue => SByte.MinValue,
				SByte.MaxValue => SByte.MaxValue,
				Int16.MinValue => Int16.MinValue,
				Int16.MaxValue => Int16.MaxValue,
				Int32.MinValue => Int32.MinValue,
				Int32.MaxValue => Int32.MaxValue,
				Int64.MinValue => Int64.MinValue,
				Int64.MaxValue => Int64.MaxValue,
				Single.NegativeInfinity => 0L,
				Single.MinValue => 0L,
				Single.MaxValue => -1L,
				Single.PositiveInfinity => -1L,
				_ => (Int64)value,
			};
			return NativeUtilities.AsBytes(result).ToValue<TInteger>();
		}
		catch (Exception)
		{
			return Double.IsNegative(value) ? TInteger.Zero : TInteger.AllBitsSet;
		}
	}
	/// <summary>
	/// Retrieves the single-precision value of a <see cref="Double"/> value.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> value.</param>
	/// <returns>A <see cref="Single"/> type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static Single GetSingleValue(Double value)
		=> value switch
		{
			_ => (Single)value,
		};
	/// <summary>
	/// Retrieves the single-precision value of a <see cref="Single"/> value.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> value.</param>
	/// <returns>A <see cref="Single"/> type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static Single GetSingleValue(Single value)
		=> value switch
		{
			_ => value,
		};

	/// <inheritdoc cref="IEquatable{JPrimitiveObject}.Equals(JPrimitiveObject)"/>
	protected static Boolean Equals<TPrimitive>(TPrimitive primitive, JPrimitiveObject? other)
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
	{
		if (other is null || other.ObjectSignature[0] == UnicodePrimitiveSignatures.BooleanSignatureChar)
			return false;
		return other.ObjectSignature[0] switch
		{
			UnicodePrimitiveSignatures.ByteSignatureChar => (JByte)primitive == other.AsPrimitive<JByte, SByte>(),
			UnicodePrimitiveSignatures.CharSignatureChar => (JChar)primitive == other.AsPrimitive<JChar, Char>(),
			UnicodePrimitiveSignatures.DoubleSignatureChar => (JDouble)primitive ==
				other.AsPrimitive<JDouble, Double>(),
			UnicodePrimitiveSignatures.FloatSignatureChar => (JFloat)primitive == other.AsPrimitive<JFloat, Single>(),
			UnicodePrimitiveSignatures.IntSignatureChar => (JInt)primitive == other.AsPrimitive<JInt, Int32>(),
			UnicodePrimitiveSignatures.LongSignatureChar => (JLong)primitive == other.AsPrimitive<JLong, Int64>(),
			UnicodePrimitiveSignatures.ShortSignatureChar => (JShort)primitive == other.AsPrimitive<JShort, Int16>(),
			_ => false,
		};
	}
	/// <inheritdoc cref="IEquatable{IPrimitiveType}.Equals(IPrimitiveType)"/>
	protected static Boolean Equals<TPrimitive>(TPrimitive primitive, IPrimitiveType? other)
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
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
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
	IEquatable<TPrimitive>, IPrimitiveEquatable
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue>
{
	/// <inheritdoc cref="INumberBase{TSelf}.One"/>
	protected static readonly TPrimitive One = IPrimitiveNumericType<TPrimitive, TValue>.GetOne();
	/// <inheritdoc cref="INumberBase{TSelf}.Zero"/>
	protected static readonly TPrimitive Zero = IPrimitiveNumericType<TPrimitive, TValue>.GetZero();
	/// <inheritdoc cref="IBinaryNumber{TSelf}.AllBitsSet"/>
	protected static readonly TPrimitive AllBitsSet = IPrimitiveNumericType<TPrimitive, TValue>.GetAllBitsSet();
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	protected static readonly TPrimitive AdditiveIdentity =
		IPrimitiveNumericType<TPrimitive, TValue>.GetAdditiveIdentity();
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	protected static readonly TPrimitive MultiplicativeIdentity =
		IPrimitiveNumericType<TPrimitive, TValue>.GetMultiplicativeIdentity();

	/// <inheritdoc cref="INumberBase{TSelf}.One"/>
	private static TPrimitive GetOne()
	{
		TValue result = TValue.One;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="INumberBase{TSelf}.Zero"/>
	private static TPrimitive GetZero()
	{
		TValue result = TValue.Zero;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	private static TPrimitive GetAdditiveIdentity()
	{
		TValue result = TValue.AdditiveIdentity;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	private static TPrimitive GetMultiplicativeIdentity()
	{
		TValue result = TValue.MultiplicativeIdentity;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IBinaryNumber{TSelf}.AllBitsSet"/>
	private static TPrimitive GetAllBitsSet()
	{
		TValue result = TValue.AllBitsSet;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
}