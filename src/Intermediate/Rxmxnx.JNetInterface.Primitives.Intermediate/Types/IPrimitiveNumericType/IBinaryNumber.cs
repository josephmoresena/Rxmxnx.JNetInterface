namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>, IPrimitiveEquatable
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue>
{
	/// <inheritdoc cref="IBinaryNumber{TSelf}.IsPow2(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsPow2(in TPrimitive value) => TValue.IsPow2(value.Value);
	/// <inheritdoc cref="IBinaryNumber{TSelf}.Log2(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive Log2(in TPrimitive value)
	{
		TValue result = TValue.Log2(value.Value);
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
}