namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NumericHelper
{
	/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_Addition(TSelf, TOther)"/>
	public static T Addition<T>(T left, T right) where T : unmanaged, IAdditionOperators<T, T, T>
	{
		unchecked
		{
			return left + right;
		}
	}
	/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_CheckedAddition(TSelf, TOther)"/>
	public static T CheckedAddition<T>(T left, T right) where T : unmanaged, IAdditionOperators<T, T, T>
	{
		checked
		{
			return left + right;
		}
	}

	/// <inheritdoc cref="IDecrementOperators{TSelf}.op_Decrement(TSelf)"/>
	public static T Decrement<T>(T value) where T : unmanaged, IDecrementOperators<T>
	{
		unchecked
		{
			value--;
		}
		return value;
	}
	/// <inheritdoc cref="IDecrementOperators{TSelf}.op_CheckedDecrement(TSelf)"/>
	public static T CheckedDecrement<T>(T value) where T : unmanaged, IDecrementOperators<T>
	{
		checked
		{
			value--;
		}
		return value;
	}

	/// <inheritdoc cref="IDivisionOperators{TSelf, TOther, TResult}.op_Division(TSelf, TOther)"/>
	public static T Division<T>(T left, T right) where T : unmanaged, IDivisionOperators<T, T, T>
	{
		unchecked
		{
			return left / right;
		}
	}
	/// <inheritdoc cref="IDivisionOperators{TSelf, TOther, TResult}.op_Division(TSelf, TOther)"/>
	public static T CheckedDivision<T>(T left, T right) where T : unmanaged, IDivisionOperators<T, T, T>
	{
		checked
		{
			return left / right;
		}
	}

	/// <inheritdoc cref="IIncrementOperators{TSelf}.op_Increment(TSelf)"/>
	public static T Increment<T>(T value) where T : unmanaged, IIncrementOperators<T>
	{
		unchecked
		{
			value++;
		}
		return value;
	}
	/// <inheritdoc cref="IIncrementOperators{TSelf}.op_CheckedIncrement(TSelf)"/>
	public static T CheckedIncrement<T>(T value) where T : unmanaged, IIncrementOperators<T>
	{
		unchecked
		{
			value++;
		}
		return value;
	}

	/// <inheritdoc cref="IMultiplyOperators{TSelf, TOther, TResult}.op_Multiply(TSelf, TOther)"/>
	public static T Multiply<T>(T left, T right) where T : unmanaged, IMultiplyOperators<T, T, T>
	{
		unchecked
		{
			return left * right;
		}
	}
	/// <inheritdoc cref="IMultiplyOperators{TSelf, TOther, TResult}.op_CheckedMultiply(TSelf, TOther)"/>
	public static T CheckedMultiply<T>(T left, T right) where T : unmanaged, IMultiplyOperators<T, T, T>
	{
		checked
		{
			return left * right;
		}
	}

	/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_Subtraction(TSelf, TOther)"/>
	public static T Subtraction<T>(T left, T right) where T : unmanaged, ISubtractionOperators<T, T, T>
	{
		unchecked
		{
			return left - right;
		}
	}
	/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_CheckedSubtraction(TSelf, TOther)"/>
	public static T CheckedSubtraction<T>(T left, T right) where T : unmanaged, ISubtractionOperators<T, T, T>
	{
		checked
		{
			return left - right;
		}
	}

	/// <inheritdoc cref="IUnaryPlusOperators{TSelf, TResult}.op_UnaryPlus(TSelf)"/>
	public static T UnaryPlus<T>(T value) where T : unmanaged, IUnaryPlusOperators<T, T> => +value;

	/// <inheritdoc cref="IUnaryNegationOperators{TSelf, TResult}.op_UnaryNegation(TSelf)"/>
	public static T UnaryNegation<T>(T value) where T : unmanaged, IUnaryNegationOperators<T, T>
	{
		unchecked
		{
			return -value;
		}
	}
	/// <inheritdoc cref="IUnaryNegationOperators{TSelf, TResult}.op_CheckedUnaryNegation(TSelf)"/>
	public static T CheckedUnaryNegation<T>(T value) where T : unmanaged, IUnaryNegationOperators<T, T>
	{
		checked
		{
			return -value;
		}
	}
	/// <inheritdoc cref="IModulusOperators{TSelf, TOther, TResult}.op_Modulus(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Modulus<T>(T left, T right) where T : unmanaged, IModulusOperators<T, T, T> => left % right;
}