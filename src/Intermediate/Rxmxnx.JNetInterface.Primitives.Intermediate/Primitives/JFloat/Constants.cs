namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JFloat
{
	/// <inheritdoc cref="INumberBase{JFloat}.One"/>
	public static JFloat One => IPrimitiveNumericType<JFloat, Single>.One;
	/// <inheritdoc cref="INumberBase{JFloat}.Zero"/>
	public static JFloat Zero => IPrimitiveNumericType<JFloat, Single>.Zero;
	/// <inheritdoc cref="IBinaryNumber{JFloat}.AllBitsSet"/>
	public static JFloat AllBitsSet => IPrimitiveNumericType<JFloat, Single>.AllBitsSet;
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	public static JFloat AdditiveIdentity => IPrimitiveNumericType<JFloat, Single>.AdditiveIdentity;
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	public static JFloat MultiplicativeIdentity => IPrimitiveNumericType<JFloat, Single>.MultiplicativeIdentity;
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static JFloat NegativeOne => IPrimitiveSignedType<JFloat, Single>.NegativeOne;
}