namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java signed number.
/// </summary>
internal interface IPrimitiveNumericSignedType : IPrimitiveNumericType { }

/// <summary>
/// This interface exposes an object that represents a java signed number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive integer.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent integer.</typeparam>
internal interface
	IPrimitiveNumericSignedType<TPrimitive, TValue> : IPrimitiveNumericSignedType,
		IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumericSignedType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue>, ISignedNumber<TValue>
{
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static readonly TPrimitive NegativeOne = NativeUtilities.Transform<TValue, TPrimitive>(TValue.NegativeOne);
}