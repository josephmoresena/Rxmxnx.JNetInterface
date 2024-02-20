namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>, IPrimitiveEquatable
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Modulus(in TValue left, in TValue right)
	{
		TValue result = left % right;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
}