namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveNumericType<TPrimitive, TValue>
{
	/// <inheritdoc cref="IBinaryNumber{TSelf}.AllBitsSet"/>
	public static readonly TPrimitive AllBitsSet = NativeUtilities.Transform<TValue, TPrimitive>(~TValue.Zero);

	/// <inheritdoc cref="IBinaryNumber{TSelf}.IsPow2(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsPow2(in TPrimitive value) => TValue.IsPow2(value.Value);
	/// <inheritdoc cref="IBinaryNumber{TSelf}.Log2(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Log2(in TPrimitive value)
	{
		TValue result = TValue.Log2(value.Value);
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
}