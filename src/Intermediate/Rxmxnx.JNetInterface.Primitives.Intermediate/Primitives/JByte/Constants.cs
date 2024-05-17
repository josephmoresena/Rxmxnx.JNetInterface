namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JByte
{
	/// <inheritdoc cref="INumberBase{JByte}.One"/>
	public static JByte One => IPrimitiveNumericType<JByte, SByte>.One;
	/// <inheritdoc cref="INumberBase{JByte}.Zero"/>
	public static JByte Zero => IPrimitiveNumericType<JByte, SByte>.Zero;
	/// <inheritdoc cref="IBinaryNumber{JByte}.AllBitsSet"/>
	public static JByte AllBitsSet => IPrimitiveNumericType<JByte, SByte>.AllBitsSet;
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	public static JByte AdditiveIdentity => IPrimitiveNumericType<JByte, SByte>.AdditiveIdentity;
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	public static JByte MultiplicativeIdentity => IPrimitiveNumericType<JByte, SByte>.MultiplicativeIdentity;
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static JByte NegativeOne => IPrimitiveSignedType<JByte, SByte>.NegativeOne;
}