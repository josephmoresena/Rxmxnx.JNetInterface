namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
internal interface IPrimitiveNumeric : IPrimitive { }

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent number.</typeparam>
internal partial interface
	IPrimitiveNumeric<TPrimitive, TValue> : IPrimitiveNumeric, INumericWrapper<TValue>, IPrimitive<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumeric<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue> { }