// ReSharper disable RedundantCast

namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal unsafe interface IPrimitiveNumericType : INativeType, IPrimitiveType
{
	/// <summary>
	/// Retrieves the integer part of a <see cref="Single"/> value.
	/// </summary>
	/// <typeparam name="TInteger">Integer type.</typeparam>
	/// <typeparam name="TFloatingPoint">Floating point type.</typeparam>
	/// <param name="value">A <typeparamref name="TFloatingPoint"/> value.</param>
	/// <returns>A <typeparamref name="TInteger"/> type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static TInteger GetIntegerValue<TInteger, TFloatingPoint>(TFloatingPoint value)
		where TInteger : unmanaged, IBinaryInteger<TInteger>
		where TFloatingPoint : unmanaged, IFloatingPoint<TFloatingPoint>
		=> sizeof(TInteger) switch
		{
			<= sizeof(Int32) => TFloatingPoint.IsPositive(value) ?
				IPrimitiveNumericType.CreateTruncating<TInteger, Int32, TFloatingPoint>(value) :
				IPrimitiveNumericType.CreateTruncating<TInteger, Int64, TFloatingPoint>(value),
			_ => TFloatingPoint.IsPositive(value) ?
				IPrimitiveNumericType.CreateTruncating<TInteger, Int64, TFloatingPoint>(value) :
				IPrimitiveNumericType.CreateTruncating<TInteger, Int128, TFloatingPoint>(value),
		};
	/// <summary>
	/// Retrieves the single-precision value of a <see cref="Double"/> value.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> value.</param>
	/// <returns>A <see cref="Single"/> type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static Single GetSingleValue(Double value) => Single.CreateTruncating(value);
	/// <inheritdoc cref="IEquatable{JPrimitiveObject}.Equals(JPrimitiveObject)"/>
	protected static Boolean Equals<TPrimitive>(TPrimitive primitive, JPrimitiveObject? other)
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
	{
		if (other is null || other.ObjectSignature[0] == CommonNames.BooleanSignatureChar)
			return false;
		return other.ObjectSignature[0] switch
		{
			CommonNames.ByteSignatureChar => (JByte)primitive == other.AsPrimitive<JByte, SByte>(),
			CommonNames.CharSignatureChar => (JChar)primitive == other.AsPrimitive<JChar, Char>(),
			CommonNames.DoubleSignatureChar => (JDouble)primitive == other.AsPrimitive<JDouble, Double>(),
			CommonNames.FloatSignatureChar => (JFloat)primitive == other.AsPrimitive<JFloat, Single>(),
			CommonNames.IntSignatureChar => (JInt)primitive == other.AsPrimitive<JInt, Int32>(),
			CommonNames.LongSignatureChar => (JLong)primitive == other.AsPrimitive<JLong, Int64>(),
			CommonNames.ShortSignatureChar => (JShort)primitive == other.AsPrimitive<JShort, Int16>(),
			_ => false,
		};
	}
	/// <inheritdoc cref="IEquatable{IPrimitiveType}.Equals(IPrimitiveType)"/>
	protected static Boolean Equals<TPrimitive>(TPrimitive primitive, IPrimitiveType? other)
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
		=> other switch
		{
			TPrimitive p => primitive.Equals(p),
			JByte jByte => (JByte)primitive == jByte,
			JChar jChar => (JChar)primitive == jChar,
			JDouble jDouble => (JDouble)primitive == jDouble,
			JFloat jFloat => (JFloat)primitive == jFloat,
			JInt jInt => (JInt)primitive == jInt,
			JLong jLong => (JLong)primitive == jLong,
			IWrapper<JByte> jByteWrapper => (JByte)primitive == jByteWrapper.Value,
			IWrapper<SByte> sByteWrapper => (SByte)primitive == sByteWrapper.Value,
			IWrapper<JChar> jCharWrapper => (JChar)primitive == jCharWrapper.Value,
			IWrapper<Char> charWrapper => (Char)primitive == charWrapper.Value,
			IWrapper<JDouble> jDoubleWrapper => (JDouble)primitive == jDoubleWrapper.Value,
			IWrapper<Double> doubleWrapper => (Double)primitive == (JDouble)doubleWrapper.Value,
			IWrapper<JFloat> jFloatWrapper => (JFloat)primitive == jFloatWrapper.Value,
			IWrapper<Single> floatWrapper => (Single)primitive == (JFloat)floatWrapper.Value,
			IWrapper<JInt> jIntWrapper => (JInt)primitive == jIntWrapper.Value,
			IWrapper<Int32> intWrapper => (Int32)primitive == (JInt)intWrapper.Value,
			IWrapper<JLong> jLongWrapper => (JLong)primitive == jLongWrapper.Value,
			IWrapper<Int64> longWrapper => (Int64)primitive == (JLong)longWrapper.Value,
			JShort jShort => (JShort)primitive == jShort,
			IWrapper<JShort> jShortWrapper => (JShort)primitive == jShortWrapper.Value,
			IWrapper<Int16> shortWrapper => (Int16)primitive == (JShort)shortWrapper.Value,
			_ => false,
		};

	/// <summary>
	/// Performs <see cref="INumberBase{TSelf}.CreateTruncating{TOther}(TOther)"/> in a
	/// <typeparamref name="TInteger"/> binary space and transform the result to <typeparamref name="TResult"/>.
	/// </summary>
	/// <typeparam name="TResult">Integer type</typeparam>
	/// <typeparam name="TInteger">Integer type.</typeparam>
	/// <typeparam name="TFloatingPoint">Floating point type.</typeparam>
	/// <param name="value">A <typeparamref name="TFloatingPoint"/> value.</param>
	/// <returns>A <typeparamref name="TInteger"/> type.</returns>
	private static TResult CreateTruncating<TResult, TInteger, TFloatingPoint>(TFloatingPoint value)
		where TResult : unmanaged, IBinaryInteger<TResult>
		where TInteger : unmanaged, IBinaryInteger<TInteger>
		where TFloatingPoint : unmanaged, IFloatingPoint<TFloatingPoint>
	{
		TInteger result = TInteger.CreateTruncating(value);
		return Unsafe.As<TInteger, TResult>(ref result);
	}
}

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveNumericType<TPrimitive> : IPrimitiveNumericType, IComparable<TPrimitive>,
	IEquatable<TPrimitive>, INativeDataType<TPrimitive> where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
{
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JByte(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JChar"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JChar(TPrimitive jPrimitive);
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
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JInt(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JLong(TPrimitive jPrimitive);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="jPrimitive">A <typeparamref name="TPrimitive"/> to explicitly convert.</param>
	static abstract explicit operator JShort(TPrimitive jPrimitive);

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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static explicit INativeDataType<TPrimitive>.operator SByte(TPrimitive value) => ((JByte)value).Value;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static explicit INativeDataType<TPrimitive>.operator Char(TPrimitive value) => ((JChar)value).Value;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static explicit INativeDataType<TPrimitive>.operator UInt16(TPrimitive value) => ((JChar)value).Value;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static explicit INativeDataType<TPrimitive>.operator Double(TPrimitive value) => ((JDouble)value).Value;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static explicit INativeDataType<TPrimitive>.operator Single(TPrimitive value) => ((JFloat)value).Value;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static explicit INativeDataType<TPrimitive>.operator Int32(TPrimitive value) => ((JInt)value).Value;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static explicit INativeDataType<TPrimitive>.operator Int64(TPrimitive value) => ((JLong)value).Value;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static explicit INativeDataType<TPrimitive>.operator Int16(TPrimitive value) => ((JShort)value).Value;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static implicit INativeDataType<TPrimitive>.operator TPrimitive(Double value) => TPrimitive.FromDouble(value);
}