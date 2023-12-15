namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveNumericType<TPrimitive, TValue>
{
	/// <inheritdoc cref="INumberBase{TSelf}.One"/>
	public static readonly TPrimitive One = IPrimitiveNumericType<TPrimitive, TValue>.GetOne();
	/// <inheritdoc cref="INumberBase{TSelf}.Zero"/>
	public static TPrimitive Zero = IPrimitiveNumericType<TPrimitive, TValue>.GetZero();

	/// <inheritdoc cref="INumberBase{TSelf}.MaxMagnitudeNumber(TSelf, TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive MaxMagnitudeNumber(in TValue x, in TValue y)
	{
		TValue result = TValue.MaxMagnitudeNumber(x, y);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="INumberBase{TSelf}.MinMagnitudeNumber(TSelf, TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive MinMagnitudeNumber(in TValue x, in TValue y)
	{
		TValue result = TValue.MinMagnitudeNumber(x, y);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}

	/// <inheritdoc cref="INumberBase{TSelf}.Parse(String, NumberStyles, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Parse(String s, NumberStyles style, IFormatProvider? provider)
	{
		TValue result = TValue.Parse(s, style, provider);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="INumberBase{TSelf}.Parse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Parse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider)
	{
		TValue result = TValue.Parse(s, style, provider);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}

	/// <inheritdoc cref="INumberBase{TSelf}.TryParse(String, NumberStyles, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse([NotNullWhen(true)] String? s, NumberStyles style, IFormatProvider? provider,
		out TPrimitive result)
	{
		Unsafe.SkipInit(out result);
		return TValue.TryParse(s, style, provider, out Unsafe.As<TPrimitive, TValue>(ref result));
	}
	/// <inheritdoc cref="INumberBase{TSelf}.TryParse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider,
		out TPrimitive result)
	{
		Unsafe.SkipInit(out result);
		return TValue.TryParse(s, style, provider, out Unsafe.As<TPrimitive, TValue>(ref result));
	}
	/// <inheritdoc cref="INumberBase{TSelf}.CreateChecked{TOther}(TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive CreateChecked<TOther>(TOther value) where TOther : INumberBase<TOther>
	{
		TValue result = TValue.CreateChecked(value);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}

	/// <inheritdoc cref="INumberBase{TSelf}.CreateSaturating{TOther}(TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive CreateSaturating<TOther>(TOther value) where TOther : INumberBase<TOther>
	{
		TValue result = TValue.CreateSaturating(value);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}

	/// <inheritdoc cref="INumberBase{TSelf}.CreateTruncating{TOther}(TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive CreateTruncating<TOther>(TOther value) where TOther : INumberBase<TOther>
	{
		TValue result = TValue.CreateTruncating(value);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}

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
}