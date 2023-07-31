namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveNumericType<TPrimitive, TValue>
{
	/// <inheritdoc cref="INumberBase{TSelf}.One"/>
	public static readonly TPrimitive One = NativeUtilities.Transform<TValue, TPrimitive>(TValue.One);
	/// <inheritdoc cref="INumberBase{TSelf}.Zero"/>
	public static TPrimitive Zero = NativeUtilities.Transform<TValue, TPrimitive>(TValue.Zero);

	/// <inheritdoc cref="INumberBase{TSelf}.MaxMagnitudeNumber(TSelf, TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive MaxMagnitudeNumber(in TValue x, in TValue y)
	{
		TValue result = TValue.MaxMagnitudeNumber(x, y);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="INumberBase{TSelf}.MinMagnitudeNumber(TSelf, TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive MinMagnitudeNumber(in TValue x, in TValue y)
	{
		TValue result = TValue.MinMagnitudeNumber(x, y);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}

	/// <inheritdoc cref="INumberBase{TSelf}.Parse(String, NumberStyles, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Parse(String s, NumberStyles style, IFormatProvider? provider)
	{
		TValue result = TValue.Parse(s, style, provider);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="INumberBase{TSelf}.Parse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Parse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider)
	{
		TValue result = TValue.Parse(s, style, provider);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}

	/// <inheritdoc cref="INumberBase{TSelf}.TryParse(String, NumberStyles, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse([NotNullWhen(true)] String? s, NumberStyles style, IFormatProvider? provider,
		out TPrimitive result)
	{
		Boolean parseResult = TValue.TryParse(s, style, provider, out TValue valueResult);
		result = parseResult ? NativeUtilities.Transform<TValue, TPrimitive>(valueResult) : default;
		return parseResult;
	}
	/// <inheritdoc cref="INumberBase{TSelf}.TryParse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider,
		out TPrimitive result)
	{
		Boolean parseResult = TValue.TryParse(s, style, provider, out TValue valueResult);
		result = parseResult ? NativeUtilities.Transform<TValue, TPrimitive>(valueResult) : default;
		return parseResult;
	}
	/// <inheritdoc cref="INumberBase{TSelf}.CreateChecked{TOther}(TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive CreateChecked<TOther>(TOther value) where TOther : INumberBase<TOther>
		=> NativeUtilities.Transform<TValue, TPrimitive>(TValue.CreateChecked(value));

	/// <inheritdoc cref="INumberBase{TSelf}.CreateSaturating{TOther}(TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive CreateSaturating<TOther>(TOther value) where TOther : INumberBase<TOther>
		=> NativeUtilities.Transform<TValue, TPrimitive>(TValue.CreateSaturating(value));

	/// <inheritdoc cref="INumberBase{TSelf}.CreateTruncating{TOther}(TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive CreateTruncating<TOther>(TOther value) where TOther : INumberBase<TOther>
		=> NativeUtilities.Transform<TValue, TPrimitive>(TValue.CreateTruncating(value));
}