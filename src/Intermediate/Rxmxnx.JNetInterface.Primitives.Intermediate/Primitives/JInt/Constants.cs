namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JInt
{
	/// <inheritdoc cref="INumberBase{JInt}.One"/>
	public static JInt One => IPrimitiveNumericType<JInt, Int32>.One;
	/// <inheritdoc cref="INumberBase{JInt}.Zero"/>
	public static JInt Zero => IPrimitiveNumericType<JInt, Int32>.Zero;
	/// <inheritdoc cref="IBinaryNumber{JInt}.AllBitsSet"/>
	public static JInt AllBitsSet => IPrimitiveNumericType<JInt, Int32>.AllBitsSet;
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	public static JInt AdditiveIdentity => IPrimitiveNumericType<JInt, Int32>.AdditiveIdentity;
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	public static JInt MultiplicativeIdentity => IPrimitiveNumericType<JInt, Int32>.MultiplicativeIdentity;
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static JInt NegativeOne => IPrimitiveSignedType<JInt, Int32>.NegativeOne;
}