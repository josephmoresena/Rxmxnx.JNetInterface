namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveNumeric<TPrimitive, TValue>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Modulus(TValue left, TValue right)
	{
		TValue result = left % right;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
}