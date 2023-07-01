namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveNumeric<TPrimitive, TValue>
{
	/// <inheritdoc cref="IParsable{TSelf}.Parse(String, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Parse(String s, IFormatProvider? provider)
	{
		TValue result = TValue.Parse(s, provider);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="ISpanParsable{TSelf}.Parse(ReadOnlySpan{Char}, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Parse(ReadOnlySpan<Char> s, IFormatProvider? provider)
	{
		TValue result = TValue.Parse(s, provider);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="IParsable{TSelf}.TryParse(String, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, out TPrimitive result)
	{
		Boolean parseResult = TValue.TryParse(s, provider, out TValue valueResult);
		result = parseResult ? NativeUtilities.Transform<TValue, TPrimitive>(valueResult) : default;
		return parseResult;
	}
	/// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{Char}, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider, out TPrimitive result)
	{
		Boolean parseResult = TValue.TryParse(s, provider, out TValue valueResult);
		result = parseResult ? NativeUtilities.Transform<TValue, TPrimitive>(valueResult) : default;
		return parseResult;
	}
}