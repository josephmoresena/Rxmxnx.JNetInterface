namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JShort
{
	/// <inheritdoc cref="INumberBase{JShort}.One"/>
	public static JShort One => IPrimitiveNumericType<JShort, Int16>.One;
	/// <inheritdoc cref="INumberBase{JShort}.Zero"/>
	public static JShort Zero => IPrimitiveNumericType<JShort, Int16>.Zero;
	/// <inheritdoc cref="IBinaryNumber{JShort}.AllBitsSet"/>
	public static JShort AllBitsSet => IPrimitiveNumericType<JShort, Int16>.AllBitsSet;
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	public static JShort AdditiveIdentity => IPrimitiveNumericType<JShort, Int16>.AdditiveIdentity;
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	public static JShort MultiplicativeIdentity => IPrimitiveNumericType<JShort, Int16>.MultiplicativeIdentity;
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static JShort NegativeOne => IPrimitiveSignedType<JShort, Int16>.NegativeOne;
}