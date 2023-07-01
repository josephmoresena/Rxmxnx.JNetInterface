namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java signed number.
/// </summary>
internal interface IPrimitiveSigned : IPrimitiveNumeric { }

/// <summary>
/// This interface exposes an object that represents a java signed number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive integer.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent integer.</typeparam>
internal interface IPrimitiveSigned<TPrimitive, TValue> : IPrimitiveSigned, IPrimitiveNumeric<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveSigned<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue>, ISignedNumber<TValue>
{
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static readonly TPrimitive NegativeOne = NativeUtilities.Transform<TValue, TPrimitive>(TValue.NegativeOne);
}