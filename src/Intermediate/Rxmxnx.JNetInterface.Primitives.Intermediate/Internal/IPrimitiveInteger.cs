namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
internal interface IPrimitiveInteger : IPrimitiveNumeric { }

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive integer.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent integer.</typeparam>
internal interface
	IPrimitiveInteger<TPrimitive, TValue> : IPrimitiveInteger, IIntegerWrapper<TValue>,
		IPrimitiveNumeric<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveInteger<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryInteger<TValue>
	, IMinMaxValue<TValue>
{
	/// <inheritdoc cref="IBinaryInteger{TSelf}.PopCount(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive PopCount(in TValue value)
		=> NativeUtilities.Transform<TValue, TPrimitive>(TValue.PopCount(value));
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TrailingZeroCount(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive TrailingZeroCount(in TValue value)
		=> NativeUtilities.Transform<TValue, TPrimitive>(TValue.PopCount(value));
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadBigEndian(ReadOnlySpan{Byte}, Boolean, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out TPrimitive value)
	{
		Boolean readResult = TValue.TryReadBigEndian(source, isUnsigned, out TValue valueResult);
		value = readResult ? NativeUtilities.Transform<TValue, TPrimitive>(valueResult) : default;
		return readResult;
	}
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadLittleEndian(ReadOnlySpan{Byte}, Boolean, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out TPrimitive value)
	{
		Boolean readResult = TValue.TryReadLittleEndian(source, isUnsigned, out TValue valueResult);
		value = readResult ? NativeUtilities.Transform<TValue, TPrimitive>(valueResult) : default;
		return readResult;
	}
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteBigEndian(TValue value, Span<Byte> destination, out Int32 bytesWritten)
		=> value.TryWriteBigEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteLittleEndian(TValue value, Span<Byte> destination, out Int32 bytesWritten)
		=> value.TryWriteBigEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetShortestBitLength(TValue value) => value.GetShortestBitLength();
}