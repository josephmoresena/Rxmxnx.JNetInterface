namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NumericHelper
{
	/// <inheritdoc cref="IFormattable.ToString(String, IFormatProvider)"/>
	public static String ToString<T>(T value, String? format, IFormatProvider? formatProvider)
		where T : unmanaged, IFormattable
		=> value.ToString(format, formatProvider);
	/// <inheritdoc cref="ISpanFormattable.TryFormat(Span{Char}, out Int32, ReadOnlySpan{Char}, IFormatProvider)"/>
	public static Boolean TryFormat<T>(T value, Span<Char> destination, out Int32 charsWritten,
		ReadOnlySpan<Char> format, IFormatProvider? provider) where T : unmanaged, ISpanFormattable
		=> value.TryFormat(destination, out charsWritten, format, provider);
#if NET8_0_OR_GREATER
	/// <inheritdoc cref="IUtf8SpanFormattable.TryFormat(Span{Byte}, out Int32, ReadOnlySpan{Char}, IFormatProvider)"/>
	public static Boolean TryFormat<T>(T value, Span<Byte> destination, out Int32 bytesWritten,
		ReadOnlySpan<Char> format, IFormatProvider? provider) where T : unmanaged, IUtf8SpanFormattable
		=> value.TryFormat(destination, out bytesWritten, format, provider);
#endif
}