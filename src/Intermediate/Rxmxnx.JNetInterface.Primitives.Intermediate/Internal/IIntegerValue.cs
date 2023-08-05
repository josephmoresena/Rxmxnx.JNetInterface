namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IIntegerValue<TValue> : INumericValue<TValue>
	where TValue : unmanaged, IComparable, IConvertible, ISpanFormattable, IComparable<TValue>, IEquatable<TValue>,
	IBinaryInteger<TValue>, IMinMaxValue<TValue> { }