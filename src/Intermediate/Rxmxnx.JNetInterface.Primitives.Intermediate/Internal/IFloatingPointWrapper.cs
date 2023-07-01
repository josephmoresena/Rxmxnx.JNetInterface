namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
internal interface IFloatingPointWrapper<TValue> : INumericWrapper<TValue>
	where TValue : unmanaged, IComparable, IConvertible, ISpanFormattable, IComparable<TValue>, IEquatable<TValue>,
	IBinaryFloatingPointIeee754<TValue>, IMinMaxValue<TValue> { }