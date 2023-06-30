namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveNumeric<TPrimitive, TValue>
{
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity" />
	public static readonly TPrimitive AdditiveIdentity = NativeUtilities.Transform<TValue, TPrimitive>(TValue.AdditiveIdentity);
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity" />
	public static readonly TPrimitive MultiplicativeIdentity = NativeUtilities.Transform<TValue, TPrimitive>(TValue.MultiplicativeIdentity);

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
	
	/// <inheritdoc cref="IDivisionOperators{TSelf, TOther, TResult}.op_Division(TSelf, TOther)" />
	public static TPrimitive Division(in TValue left, in TValue right)
		=> NativeUtilities.Transform<TValue, TPrimitive>(left / right);
	/// <inheritdoc cref="IDivisionOperators{TSelf, TOther, TResult}.op_Division(TSelf, TOther)" />
	public static TPrimitive CheckedDivision(in TValue left, in TValue right)
	{
		TValue result;
		checked
		{
			result = left / right;
		}
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	
	/// <inheritdoc cref="IIncrementOperators{TSelf}.op_Increment(TSelf)" />
	public static void Increment(ref TPrimitive value)
	{
		ref TValue refValue = ref value.Transform<TPrimitive, TValue>();
		refValue++;
	}
	/// <inheritdoc cref="IIncrementOperators{TSelf}.op_CheckedIncrement(TSelf)" />
	public static void CheckedIncrement(ref TPrimitive value)
	{
		ref TValue refValue = ref value.Transform<TPrimitive, TValue>();
		checked
		{
			refValue++;
		}
	}
	
	/// <inheritdoc cref="IMultiplyOperators{TSelf, TOther, TResult}.op_Multiply(TSelf, TOther)" />
	public static TPrimitive Multiply(in TValue left, in TValue right)
		=> NativeUtilities.Transform<TValue, TPrimitive>(left * right);
	/// <inheritdoc cref="IMultiplyOperators{TSelf, TOther, TResult}.op_CheckedMultiply(TSelf, TOther)" />
	public static TPrimitive CheckedMultiply(in TValue left, in TValue right)
	{
		TValue result;
		checked
		{
			result = left * right;
		}
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	
	/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_Subtraction(TSelf, TOther)" />
	public static TPrimitive Subtraction(in TValue left, in TValue right)
		=> NativeUtilities.Transform<TValue, TPrimitive>(left - right);
	/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_CheckedSubtraction(TSelf, TOther)" />
	public static TPrimitive CheckedSubtraction(in TValue left, in TValue right)
	{
		TValue result;
		checked
		{
			result = left - right;
		}
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
	
	/// <inheritdoc cref="IUnaryPlusOperators{TSelf, TResult}.op_UnaryPlus(TSelf)" />
	public static TPrimitive UnaryPlus(in TValue value)
		=> NativeUtilities.Transform<TValue, TPrimitive>(+value);
	
	/// <inheritdoc cref="IUnaryNegationOperators{TSelf, TResult}.op_UnaryNegation(TSelf)" />
	public static TPrimitive UnaryNegation(in TValue value)
		=> NativeUtilities.Transform<TValue, TPrimitive>(-value);
	/// <inheritdoc cref="IUnaryNegationOperators{TSelf, TResult}.op_CheckedUnaryNegation(TSelf)" />
	public static TPrimitive CheckedUnaryNegation(in TValue value)
	{
		TValue result;
		checked
		{
			result = -value;
		}
		return NativeUtilities.Transform<TValue, TPrimitive>(result);
	}
}