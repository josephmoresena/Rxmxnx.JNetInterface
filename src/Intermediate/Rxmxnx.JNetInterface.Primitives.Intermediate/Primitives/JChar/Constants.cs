namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JChar
{
	/// <inheritdoc cref="INumberBase{JChar}.One"/>
	public static JChar One => IPrimitiveNumericType<JChar, Char>.One;
	/// <inheritdoc cref="INumberBase{JChar}.Zero"/>
	public static JChar Zero => IPrimitiveNumericType<JChar, Char>.Zero;
	/// <inheritdoc cref="IBinaryNumber{JChar}.AllBitsSet"/>
	public static JChar AllBitsSet => IPrimitiveNumericType<JChar, Char>.AllBitsSet;
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	public static JChar AdditiveIdentity => IPrimitiveNumericType<JChar, Char>.AdditiveIdentity;
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	public static JChar MultiplicativeIdentity => IPrimitiveNumericType<JChar, Char>.MultiplicativeIdentity;
}