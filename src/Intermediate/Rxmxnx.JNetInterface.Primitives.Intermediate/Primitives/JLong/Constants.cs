namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JLong
{
	/// <inheritdoc cref="INumberBase{JLong}.One"/>
	public static JLong One => IPrimitiveNumericType<JLong, Int64>.One;
	/// <inheritdoc cref="INumberBase{JLong}.Zero"/>
	public static JLong Zero => IPrimitiveNumericType<JLong, Int64>.Zero;
	/// <inheritdoc cref="IBinaryNumber{JLong}.AllBitsSet"/>
	public static JLong AllBitsSet => IPrimitiveNumericType<JLong, Int64>.AllBitsSet;
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	public static JLong AdditiveIdentity => IPrimitiveNumericType<JLong, Int64>.AdditiveIdentity;
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	public static JLong MultiplicativeIdentity => IPrimitiveNumericType<JLong, Int64>.MultiplicativeIdentity;
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static JLong NegativeOne => IPrimitiveSignedType<JLong, Int64>.NegativeOne;
}