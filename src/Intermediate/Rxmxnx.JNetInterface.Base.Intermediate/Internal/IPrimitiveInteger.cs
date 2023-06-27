namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
internal interface IPrimitiveInteger : IPrimitiveNumeric { }

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
internal interface
	IPrimitiveInteger<TPrimitive, TValue> : IPrimitiveInteger<TValue>, IPrimitiveNumeric<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveInteger<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryInteger<TValue>
	, IMinMaxValue<TValue> { }