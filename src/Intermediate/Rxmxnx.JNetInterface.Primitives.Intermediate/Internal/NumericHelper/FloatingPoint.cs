namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NumericHelper
{
	/// <inheritdoc cref="IFloatingPoint{T}.GetExponentByteCount()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetExponentByteCount<T>(T value) where T : unmanaged, IFloatingPoint<T>
		=> value.GetExponentByteCount();
	/// <inheritdoc cref="IFloatingPoint{T}.GetExponentShortestBitLength()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetExponentShortestBitLength<T>(T value) where T : unmanaged, IFloatingPoint<T>
		=> value.GetExponentShortestBitLength();
	/// <inheritdoc cref="IFloatingPoint{T}.GetSignificandBitLength()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetSignificandBitLength<T>(T value) where T : unmanaged, IFloatingPoint<T>
		=> value.GetSignificandBitLength();
	/// <inheritdoc cref="IFloatingPoint{T}.GetSignificandByteCount()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetSignificandByteCount<T>(T value) where T : unmanaged, IFloatingPoint<T>
		=> value.GetSignificandByteCount();
	/// <inheritdoc cref="IFloatingPoint{T}.TryWriteExponentBigEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteExponentBigEndian<T>(T value, Span<Byte> destination, out Int32 bytesWritten)
		where T : unmanaged, IFloatingPoint<T>
		=> value.TryWriteExponentBigEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IFloatingPoint{T}.TryWriteExponentBigEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteExponentLittleEndian<T>(T value, Span<Byte> destination, out Int32 bytesWritten)
		where T : unmanaged, IFloatingPoint<T>
		=> value.TryWriteExponentLittleEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IFloatingPoint{T}.TryWriteSignificandBigEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteSignificandBigEndian<T>(T value, Span<Byte> destination, out Int32 bytesWritten)
		where T : unmanaged, IFloatingPoint<T>
		=> value.TryWriteSignificandBigEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IFloatingPoint{T}.TryWriteSignificandLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteSignificandLittleEndian<T>(T value, Span<Byte> destination, out Int32 bytesWritten)
		where T : unmanaged, IFloatingPoint<T>
		=> value.TryWriteSignificandLittleEndian(destination, out bytesWritten);
}