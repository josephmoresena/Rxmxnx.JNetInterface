namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveNumeric<TPrimitive, TValue>
{
	/// <inheritdoc cref="INumberBase{TSelf}.One"/>
	public static TPrimitive One
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			TValue result = TValue.One;
			return NativeUtilities.Transform<TValue, TPrimitive>(result);
		}
	}
	/// <inheritdoc cref="INumberBase{TSelf}.Radix"/>
	public static Int32 Radix => TValue.Radix;
	/// <inheritdoc cref="INumberBase{TSelf}.Zero"/>
	public static TPrimitive Zero
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			TValue result = TValue.Zero;
			return NativeUtilities.Transform<TValue, TPrimitive>(result);
		}
	}

	/// <inheritdoc cref="INumberBase{TSelf}.MaxMagnitudeNumber(TSelf, TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive MaxMagnitudeNumber(TValue x, TValue y)
	{
		TValue result = TValue.MaxMagnitudeNumber(x, y);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="INumberBase{TSelf}.MinMagnitudeNumber(TSelf, TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive MinMagnitudeNumber(TValue x, TValue y)
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
		[MaybeNullWhen(false)] out TPrimitive result)
	{
		Boolean parseResult = TValue.TryParse(s, style, provider, out TValue valueResult);
		result = parseResult ? NativeUtilities.Transform<TValue, TPrimitive>(valueResult) : default;
		return parseResult;
	}
	/// <inheritdoc cref="INumberBase{TSelf}.TryParse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider,
		[MaybeNullWhen(false)] out TPrimitive result)
	{
		Boolean parseResult = TValue.TryParse(s, style, provider, out TValue valueResult);
		result = parseResult ? NativeUtilities.Transform<TValue, TPrimitive>(valueResult) : default;
		return parseResult;
	}
}