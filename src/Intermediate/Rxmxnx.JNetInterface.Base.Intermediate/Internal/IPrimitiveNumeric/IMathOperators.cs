namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveNumeric<TPrimitive, TValue>
{
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity" />
	public static readonly TPrimitive AdditiveIdentity = NativeUtilities.Transform<TValue, TPrimitive>(TValue.AdditiveIdentity);

	/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_Addition(TSelf, TOther)" />
	public static TPrimitive Addition(in TValue left, in TValue right)
		=> NativeUtilities.Transform<TValue, TPrimitive>(left + right);
	/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_CheckedAddition(TSelf, TOther)" />
	public static TPrimitive CheckedAddition(in TValue left, in TValue right)
	{
		TValue result;
		checked
		{
			result = left + right;
		}
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	/// <inheritdoc cref="IDecrementOperators{TSelf}.op_Decrement(TSelf)" />
	public static void Decrement(ref TPrimitive value)
	{
		ref TValue refValue = ref value.Transform<TPrimitive, TValue>();
		refValue--;
	}
	/// <inheritdoc cref="IDecrementOperators{TSelf}.op_CheckedDecrement(TSelf)" />
	public static void CheckedDecrement(ref TPrimitive value)
	{
		ref TValue refValue = ref value.Transform<TPrimitive, TValue>();
		checked
		{
			refValue--;
		}
	}
}