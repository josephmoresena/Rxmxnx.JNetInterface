namespace Rxmxnx.JNetInterface.Internal.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveNumericType : IPrimitiveType
{
	/// <summary>
	/// Indicates current type is an integer type.
	/// </summary>
	static virtual Boolean IsInteger
		=> ValidationUtilities.ThrowInvalidInterface<Boolean>(nameof(IPrimitiveNumericType));

	/// <summary>
	/// Retrieves the integer value of current primitive.
	/// </summary>
	internal Int64 LongValue { get; }
	/// <summary>
	/// Retrieves the double value of current primitive.
	/// </summary>
	internal Double DoubleValue { get; }
}

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
internal interface IPrimitiveNumericType<TPrimitive> : IPrimitiveNumericType
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
{
}

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent number.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
internal partial interface
	IPrimitiveNumericType<TPrimitive, TValue> : IPrimitiveNumericType<TPrimitive>, INumericValue<TValue>,
		IPrimitiveType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue> { }