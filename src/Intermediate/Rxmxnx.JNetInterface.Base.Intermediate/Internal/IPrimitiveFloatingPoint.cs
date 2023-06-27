namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
internal interface IPrimitiveFloatingPoint : IPrimitiveNumeric { }

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
internal interface
	IPrimitiveFloatingPoint<TPrimitive, TValue> : IPrimitiveFloatingPoint<TValue>, IPrimitiveNumeric<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveFloatingPoint<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>,
	IBinaryFloatingPointIeee754<TValue>, IMinMaxValue<TValue> { }