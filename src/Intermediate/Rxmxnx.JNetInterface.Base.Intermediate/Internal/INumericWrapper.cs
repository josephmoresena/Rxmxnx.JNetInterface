namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
internal interface IPrimitiveWrapperNumeric<TValue> : IPrimitiveNumeric, IPrimitiveWrapper<TValue>
	where TValue : unmanaged, IComparable, IConvertible, ISpanFormattable, IComparable<TValue>, IEquatable<TValue>,
	IBinaryNumber<TValue>, IMinMaxValue<TValue> { }