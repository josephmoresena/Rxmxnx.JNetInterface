namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NumericHelper
{
	/// <inheritdoc cref="IShiftOperators{TSelf,TOther,TResult}.op_LeftShift(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T LeftShift<T>(T value, Int32 shiftAmount) where T : unmanaged, IShiftOperators<T, Int32, T>
		=> value << shiftAmount;
	/// <inheritdoc cref="IShiftOperators{TSelf,TOther,TResult}.op_RightShift(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T RightShift<T>(T value, Int32 shiftAmount) where T : unmanaged, IShiftOperators<T, Int32, T>
		=> value >> shiftAmount;
	/// <inheritdoc cref="IShiftOperators{TSelf,TOther,TResult}.op_UnsignedRightShift(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T UnsignedRightShift<T>(T value, Int32 shiftAmount) where T : unmanaged, IShiftOperators<T, Int32, T>
		=> value >>> shiftAmount;

	/// <inheritdoc cref="IBinaryInteger{TSelf}.PopCount(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T PopCount<T>(T value) where T : unmanaged, IBinaryInteger<T> => T.PopCount(value);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TrailingZeroCount(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T TrailingZeroCount<T>(T value) where T : unmanaged, IBinaryInteger<T> => T.TrailingZeroCount(value);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadBigEndian(ReadOnlySpan{Byte}, Boolean, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryReadBigEndian<T>(ReadOnlySpan<Byte> source, Boolean isUnsigned, out T value)
		where T : unmanaged, IBinaryInteger<T>
		=> T.TryReadBigEndian(source, isUnsigned, out value);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadLittleEndian(ReadOnlySpan{Byte}, Boolean, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryReadLittleEndian<T>(ReadOnlySpan<Byte> source, Boolean isUnsigned, out T value)
		where T : unmanaged, IBinaryInteger<T>
		=> T.TryReadLittleEndian(source, isUnsigned, out value);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteBigEndian<T>(T value, Span<Byte> destination, out Int32 bytesWritten)
		where T : unmanaged, IBinaryInteger<T>
		=> value.TryWriteBigEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteLittleEndian<T>(T value, Span<Byte> destination, out Int32 bytesWritten)
		where T : unmanaged, IBinaryInteger<T>
		=> value.TryWriteLittleEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetShortestBitLength<T>(T value) where T : unmanaged, IBinaryInteger<T>
		=> value.GetShortestBitLength();
}