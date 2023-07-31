namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
internal interface IPrimitiveNumericType : IPrimitiveType { }

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent number.</typeparam>
internal partial interface
	IPrimitiveNumericType<TPrimitive, TValue> : IPrimitiveNumericType, INumericWrapper<TValue>,
		IPrimitiveType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>, IBinaryNumber<TValue>,
	IMinMaxValue<TValue> { }