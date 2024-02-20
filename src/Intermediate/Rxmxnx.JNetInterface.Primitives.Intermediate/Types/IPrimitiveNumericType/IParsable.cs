namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>, IPrimitiveEquatable
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue>
{
	/// <inheritdoc cref="IParsable{TSelf}.Parse(String, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Parse(String s, IFormatProvider? provider)
	{
		TValue result = TValue.Parse(s, provider);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="ISpanParsable{TSelf}.Parse(ReadOnlySpan{Char}, IFormatProvider?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Parse(ReadOnlySpan<Char> s, IFormatProvider? provider)
	{
		TValue result = TValue.Parse(s, provider);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IParsable{TSelf}.TryParse(String, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, out TPrimitive result)
	{
		Unsafe.SkipInit(out result);
		return TValue.TryParse(s, provider, out Unsafe.As<TPrimitive, TValue>(ref result));
	}
	/// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{Char}, IFormatProvider?, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider, out TPrimitive result)
	{
		Unsafe.SkipInit(out result);
		return TValue.TryParse(s, provider, out Unsafe.As<TPrimitive, TValue>(ref result));
	}
}