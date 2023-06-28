namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveNumeric<TPrimitive, TValue>
{
	/// <inheritdoc cref="IBitwiseOperators{TSelf, TOther, TResult}.op_BitwiseAnd(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive BitwiseAnd(TValue left, TValue right)
	{
		TValue result = left & right;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="IBitwiseOperators{TSelf, TOther, TResult}.op_BitwiseOr(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive BitwiseOr(TValue left, TValue right)
	{
		TValue result = left | right;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="IBitwiseOperators{TSelf, TOther, TResult}.op_ExclusiveOr(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive ExclusiveOr(TValue left, TValue right)
	{
		TValue result = left ^ right;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="IBitwiseOperators{TSelf, TOther, TResult}.op_OnesComplement(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TPrimitive OnesComplement(TValue value)
	{
		TValue result = ~value;
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
}