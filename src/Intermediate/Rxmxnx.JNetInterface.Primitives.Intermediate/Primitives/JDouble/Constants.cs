namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JDouble
{
	/// <inheritdoc cref="INumberBase{JDouble}.One"/>
	public static JDouble One => IPrimitiveNumericType<JDouble, Double>.One;
	/// <inheritdoc cref="INumberBase{JDouble}.Zero"/>
	public static JDouble Zero => IPrimitiveNumericType<JDouble, Double>.Zero;
	/// <inheritdoc cref="IBinaryNumber{JDouble}.AllBitsSet"/>
	public static JDouble AllBitsSet => IPrimitiveNumericType<JDouble, Double>.AllBitsSet;
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	public static JDouble AdditiveIdentity => IPrimitiveNumericType<JDouble, Double>.AdditiveIdentity;
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	public static JDouble MultiplicativeIdentity => IPrimitiveNumericType<JDouble, Double>.MultiplicativeIdentity;
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static JDouble NegativeOne => IPrimitiveSignedType<JDouble, Double>.NegativeOne;
}