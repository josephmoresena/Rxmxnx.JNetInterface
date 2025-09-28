namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NumericHelper
{
	/// <inheritdoc cref="IBitwiseOperators{TSelf, TOther, TResult}.op_BitwiseAnd(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T BitwiseAnd<T>(T left, T right) where T : unmanaged, IBitwiseOperators<T, T, T> => left & right;
	/// <inheritdoc cref="IBitwiseOperators{TSelf, TOther, TResult}.op_BitwiseOr(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T BitwiseOr<T>(T left, T right) where T : unmanaged, IBitwiseOperators<T, T, T> => left | right;
	/// <inheritdoc cref="IBitwiseOperators{TSelf, TOther, TResult}.op_ExclusiveOr(TSelf, TOther)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T ExclusiveOr<T>(T left, T right) where T : unmanaged, IBitwiseOperators<T, T, T> => left ^ right;
	/// <inheritdoc cref="IBitwiseOperators{TSelf, TOther, TResult}.op_OnesComplement(TSelf)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T OnesComplement<T>(T value) where T : unmanaged, IBitwiseOperators<T, T, T> => ~value;
}