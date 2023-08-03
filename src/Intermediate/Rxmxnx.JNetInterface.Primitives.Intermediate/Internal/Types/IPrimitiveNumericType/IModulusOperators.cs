namespace Rxmxnx.JNetInterface.Internal.Types;

internal partial interface IPrimitiveNumericType<TPrimitive, TValue>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Modulus(in TValue left, in TValue right)
	{
		TValue result = left % right;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
}