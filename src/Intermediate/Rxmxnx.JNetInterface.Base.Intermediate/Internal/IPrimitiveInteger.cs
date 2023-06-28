namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
internal interface IPrimitiveInteger : IPrimitiveNumeric { }

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive integer.</typeparam>
internal interface IPrimitiveInteger<TPrimitive> : IPrimitiveInteger, IPrimitiveNumeric<TPrimitive>
	where TPrimitive : IPrimitiveInteger<TPrimitive> { }

/// <summary>
/// This interface exposes an object that represents a java primitive integer.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive integer.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent integer.</typeparam>
internal interface
	IPrimitiveInteger<TPrimitive, TValue> : IPrimitiveInteger<TPrimitive>, IPrimitiveNumeric<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveInteger<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryInteger<TValue>
	, IMinMaxValue<TValue> { }