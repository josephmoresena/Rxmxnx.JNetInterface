namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NumericHelper
{
	/// <inheritdoc cref="INumberBase{T}.MaxMagnitudeNumber(T, T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T MaxMagnitudeNumber<T>(T x, T y) where T : unmanaged, INumberBase<T> => T.MaxMagnitudeNumber(x, y);
	/// <inheritdoc cref="INumberBase{T}.MinMagnitudeNumber(T, T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T MinMagnitudeNumber<T>(T x, T y) where T : unmanaged, INumberBase<T> => T.MinMagnitudeNumber(x, y);

	/// <inheritdoc cref="INumberBase{T}.Parse(String, NumberStyles, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Parse<T>(String s, NumberStyles style, IFormatProvider? provider)
		where T : unmanaged, INumberBase<T>
		=> T.Parse(s, style, provider);
	/// <inheritdoc cref="INumberBase{T}.Parse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Parse<T>(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider)
		where T : unmanaged, INumberBase<T>
		=> T.Parse(s, style, provider);

	/// <inheritdoc cref="INumberBase{T}.TryParse(String, NumberStyles, IFormatProvider?, out T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse<T>([NotNullWhen(true)] String? s, NumberStyles style, IFormatProvider? provider,
		out T result) where T : unmanaged, INumberBase<T>
		=> T.TryParse(s, style, provider, out result);
	/// <inheritdoc cref="INumberBase{T}.TryParse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?, out T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse<T>(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider, out T result)
		where T : unmanaged, INumberBase<T>
		=> T.TryParse(s, style, provider, out result);
}