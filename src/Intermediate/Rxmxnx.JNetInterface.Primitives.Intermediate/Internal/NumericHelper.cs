namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Numeric helper class.
/// </summary>
internal static partial class NumericHelper
{
	/// <inheritdoc cref="IAdditiveIdentity{T,T}.AdditiveIdentity"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T AdditiveIdentity<T>() where T : IAdditiveIdentity<T, T> => T.AdditiveIdentity;
	/// <inheritdoc cref="IMultiplicativeIdentity{T,T}.MultiplicativeIdentity"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T MultiplicativeIdentity<T>() where T : IMultiplicativeIdentity<T, T> => T.MultiplicativeIdentity;
	/// <inheritdoc cref="INumberBase{T}.One"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T One<T>() where T : INumberBase<T> => T.One;
	/// <inheritdoc cref="INumberBase{T}.Zero"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Zero<T>() where T : INumberBase<T> => T.Zero;
	/// <inheritdoc cref="IMinMaxValue{T}.MinValue"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T MinValue<T>() where T : IMinMaxValue<T> => T.MinValue;
	/// <inheritdoc cref="IMinMaxValue{T}.MaxValue"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T MaxValue<T>() where T : IMinMaxValue<T> => T.MaxValue;
	/// <inheritdoc cref="IBinaryNumber{T}.AllBitsSet"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T AllBitsSet<T>() where T : IBinaryNumber<T> => T.AllBitsSet;
	/// <inheritdoc cref="ISignedNumber{T}.NegativeOne"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T NegativeOne<T>() where T : ISignedNumber<T> => T.NegativeOne;

	/// <inheritdoc cref="INumberBase{T}.Abs(T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Abs<T>(T value) where T : INumberBase<T> => T.Abs(value);
}