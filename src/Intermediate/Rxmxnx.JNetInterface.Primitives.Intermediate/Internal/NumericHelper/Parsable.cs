namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NumericHelper
{
	/// <inheritdoc cref="IParsable{TSelf}.Parse(String, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Parse<T>(String s, IFormatProvider? provider) where T : unmanaged, IParsable<T>
		=> T.Parse(s, provider);
	/// <inheritdoc cref="ISpanParsable{TSelf}.Parse(ReadOnlySpan{Char}, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Parse<T>(ReadOnlySpan<Char> s, IFormatProvider? provider) where T : unmanaged, ISpanParsable<T>
		=> T.Parse(s, provider);
	/// <inheritdoc cref="IParsable{TSelf}.TryParse(String, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse<T>([NotNullWhen(true)] String? s, IFormatProvider? provider, out T result)
		where T : unmanaged, ISpanParsable<T>
		=> T.TryParse(s, provider, out result);
	/// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{Char}, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse<T>(ReadOnlySpan<Char> s, IFormatProvider? provider, out T result)
		where T : unmanaged, ISpanParsable<T>
		=> T.TryParse(s, provider, out result);
#if NET8_0_OR_GREATER
	/// <inheritdoc cref="IUtf8SpanParsable{TSelf}.Parse(ReadOnlySpan{Byte}, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Parse<T>(ReadOnlySpan<Byte> s, IFormatProvider? provider) where T : unmanaged, IUtf8SpanParsable<T>
		=> T.Parse(s, provider);
	/// <inheritdoc cref="IUtf8SpanParsable{TSelf}.TryParse(ReadOnlySpan{Byte}, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse<T>(ReadOnlySpan<Byte> utf8Text, IFormatProvider? provider, out T result)
		where T : unmanaged, IUtf8SpanParsable<T>
		=> T.TryParse(utf8Text, provider, out result);
#endif
}