namespace Rxmxnx.JNetInterface.Internal.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive floating point.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveFloatingPointType : IPrimitiveNumericType { }

/// <summary>
/// This interface exposes an object that represents a java primitive floating point.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive floating point.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent floating point.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
internal partial interface IPrimitiveFloatingPointType<TPrimitive, TValue> : IPrimitiveFloatingPointType,
	IFloatingPointValue<TValue>, IPrimitiveNumericType<TPrimitive, TValue>
	where TPrimitive : unmanaged, IPrimitiveFloatingPointType<TPrimitive, TValue>, IComparable<TPrimitive>,
	IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>,
	IBinaryFloatingPointIeee754<TValue>, IMinMaxValue<TValue> { }