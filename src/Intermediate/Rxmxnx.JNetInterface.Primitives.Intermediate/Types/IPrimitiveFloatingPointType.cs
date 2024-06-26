namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive floating point.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveFloatingPointType : IPrimitiveNumericType;

/// <summary>
/// This interface exposes an object that represents a java primitive floating point.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive floating point.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent floating point.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface
	IPrimitiveFloatingPointType<TPrimitive, TValue> : IPrimitiveFloatingPointType,
	IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveFloatingPointType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>, IPrimitiveEquatable
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>,
	IBinaryFloatingPointIeee754<TValue>, IMinMaxValue<TValue>
{
	/// <inheritdoc cref="IFloatingPoint{TSelf}.GetExponentByteCount()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetExponentByteCount(TValue value) => value.GetExponentByteCount();
	/// <inheritdoc cref="IFloatingPoint{TSelf}.GetExponentShortestBitLength()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetExponentShortestBitLength(TValue value) => value.GetExponentShortestBitLength();
	/// <inheritdoc cref="IFloatingPoint{TSelf}.GetSignificandBitLength()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetSignificandBitLength(TValue value) => value.GetSignificandBitLength();
	/// <inheritdoc cref="IFloatingPoint{TSelf}.GetSignificandByteCount()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Int32 GetSignificandByteCount(TValue value) => value.GetSignificandByteCount();
	/// <inheritdoc cref="IFloatingPoint{TSelf}.TryWriteExponentBigEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteExponentBigEndian(TValue value, Span<Byte> destination, out Int32 bytesWritten)
		=> value.TryWriteExponentBigEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IFloatingPoint{TSelf}.TryWriteExponentBigEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteExponentLittleEndian(TValue value, Span<Byte> destination, out Int32 bytesWritten)
		=> value.TryWriteExponentLittleEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IFloatingPoint{TSelf}.TryWriteSignificandBigEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteSignificandBigEndian(TValue value, Span<Byte> destination, out Int32 bytesWritten)
		=> value.TryWriteSignificandBigEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IFloatingPoint{TSelf}.TryWriteSignificandLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryWriteSignificandLittleEndian(TValue value, Span<Byte> destination, out Int32 bytesWritten)
		=> value.TryWriteSignificandLittleEndian(destination, out bytesWritten);
}