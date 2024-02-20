namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>, IPrimitiveEquatable
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue>
{
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
}