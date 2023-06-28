namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveNumeric<TPrimitive, TValue>
{
	/// <inheritdoc cref="IBinaryNumber{TSelf}.AllBitsSet"/>
	public static virtual TPrimitive AllBitsSet
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			TValue result = ~TValue.Zero;
			return NativeUtilities.Transform<TValue, TPrimitive>(result);
		}
	}

	/// <inheritdoc cref="IBinaryNumber{TSelf}.IsPow2(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsPow2(TPrimitive value) => TValue.IsPow2(value.Value);
	/// <inheritdoc cref="IBinaryNumber{TSelf}.Log2(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Log2(TPrimitive value)
	{
		TValue result = TValue.Log2(value.Value);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
}