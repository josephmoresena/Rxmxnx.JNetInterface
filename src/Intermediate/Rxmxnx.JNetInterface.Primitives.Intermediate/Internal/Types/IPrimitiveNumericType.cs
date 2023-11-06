namespace Rxmxnx.JNetInterface.Internal.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveNumericType : IPrimitiveType
{
	/// <summary>
	/// Retrieves the integer part of a <see cref="Double"/> value.
	/// </summary>
	/// <typeparam name="TInteger">Integer type.</typeparam>
	/// <param name="value">A <see cref="Double"/> value.</param>
	/// <returns>A <typeparamref name="TInteger"/> type.</returns>
	public static TInteger GetIntegerValue<TInteger>(Double value) where TInteger : unmanaged, IBinaryInteger<TInteger>
	{
		Int64 result = (Int64)value;
		return NativeUtilities.AsBytes(result).ToValue<TInteger>();
	}
}

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
internal interface IPrimitiveNumericType<TPrimitive> : IPrimitiveNumericType
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>
{
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="Double"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> to explicitly convert.</param>
	static virtual explicit operator Double(TPrimitive value) => TPrimitive.ToDouble(value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="Double"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> to explicitly convert.</param>
	static virtual explicit operator TPrimitive(Double value) => TPrimitive.FromDouble(value);

	/// <summary>
	/// Creates a <typeparamref name="TPrimitive"/> value from <see cref="Double"/>.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> value.</param>
	/// <returns>A <typeparamref name="TPrimitive"/> value.</returns>
	protected static abstract TPrimitive FromDouble(Double value);
	/// <summary>
	/// Creates a <see cref="Double"/> value from <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TPrimitive"/> value.</param>
	/// <returns>A <see cref="Double"/> value.</returns>
	protected static abstract Double ToDouble(TPrimitive value);
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