namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveIntegerType : IPrimitiveNumericType;

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive integer.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent integer.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface
	IPrimitiveIntegerType<TPrimitive, TValue> : IPrimitiveIntegerType, IIntegerValue<TValue>,
	IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveIntegerType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>, IPrimitiveEquatable
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryInteger<TValue>
	, IMinMaxValue<TValue>
{
	/// <inheritdoc cref="IShiftOperators{TSelf,TOther,TResult}.op_LeftShift(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static TPrimitive LeftShift(in TValue value, Int32 shiftAmount)
	{
		TValue result = value << shiftAmount;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="IShiftOperators{TSelf,TOther,TResult}.op_RightShift(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static TPrimitive RightShift(in TValue value, Int32 shiftAmount)
	{
		TValue result = value >> shiftAmount;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="IShiftOperators{TSelf,TOther,TResult}.op_UnsignedRightShift(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static TPrimitive UnsignedRightShift(in TValue value, Int32 shiftAmount)
	{
		TValue result = value >>> shiftAmount;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}

	/// <inheritdoc cref="IBinaryInteger{TSelf}.PopCount(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static TPrimitive PopCount(in TValue value)
	{
		TValue result = TValue.PopCount(value);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TrailingZeroCount(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static TPrimitive TrailingZeroCount(in TValue value)
	{
		TValue result = TValue.TrailingZeroCount(value);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadBigEndian(ReadOnlySpan{Byte}, Boolean, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static Boolean TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out TPrimitive value)
	{
		Unsafe.SkipInit(out value);
		return TValue.TryReadBigEndian(source, isUnsigned, out Unsafe.As<TPrimitive, TValue>(ref value));
	}
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadLittleEndian(ReadOnlySpan{Byte}, Boolean, out TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static Boolean TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out TPrimitive value)
	{
		Unsafe.SkipInit(out value);
		return TValue.TryReadLittleEndian(source, isUnsigned, out Unsafe.As<TPrimitive, TValue>(ref value));
	}
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static Boolean TryWriteBigEndian(TValue value, Span<Byte> destination, out Int32 bytesWritten)
		=> value.TryWriteBigEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static Boolean TryWriteLittleEndian(TValue value, Span<Byte> destination, out Int32 bytesWritten)
		=> value.TryWriteLittleEndian(destination, out bytesWritten);
	/// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian(Span{Byte}, out Int32)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected static Int32 GetShortestBitLength(TValue value) => value.GetShortestBitLength();
}