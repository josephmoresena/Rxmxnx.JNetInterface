namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive floating point.
/// </summary>
internal interface IPrimitiveFloatingPoint : IPrimitiveNumeric { }

/// <summary>
/// This interface exposes an object that represents a java primitive floating point.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive floating point.</typeparam>
internal interface IPrimitiveFloatingPoint<TPrimitive> : IPrimitiveFloatingPoint, IPrimitiveNumeric<TPrimitive>
	where TPrimitive : IPrimitiveFloatingPoint<TPrimitive> { }

/// <summary>
/// This interface exposes an object that represents a java primitive floating point.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive floating point.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent floating point.</typeparam>
internal interface
	IPrimitiveFloatingPoint<TPrimitive, TValue> : IPrimitiveFloatingPoint<TPrimitive>,
		IPrimitiveNumeric<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveFloatingPoint<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>,
	IBinaryFloatingPointIeee754<TValue>, IMinMaxValue<TValue> { }