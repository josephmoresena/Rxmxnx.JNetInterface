namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java signed number.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveSignedType : IPrimitiveNumericType { }

/// <summary>
/// This interface exposes an object that represents a java signed number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive integer.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent integer.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface
	IPrimitiveSignedType<TPrimitive, TValue> : IPrimitiveSignedType, IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveSignedType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue>, ISignedNumber<TValue>
{
	/// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
	public static readonly TPrimitive NegativeOne = NativeUtilities.Transform<TValue, TPrimitive>(TValue.NegativeOne);
}