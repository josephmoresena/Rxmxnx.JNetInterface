namespace Rxmxnx.JNetInterface.Primitives;

using TypeMetadata = JPrimitiveTypeMetadata<JFloat>;
using IPrimitiveValueType = IPrimitiveType<JFloat, Single>;
using IPrimitiveFloatingPointType = IPrimitiveFloatingPointType<JFloat, Single>;
using IPrimitiveSignedType = IPrimitiveSignedType<JFloat, Single>;

/// <summary>
/// Primitive <c>float</c>. Represents a single-precision floating-point number.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JFloat : INativeType, IComparable<JFloat>, IEquatable<JFloat>,
	IPrimitiveFloatingPointType, IPrimitiveSignedType
{
	/// <summary>
	/// Primitive type info.
	/// </summary>
	private static readonly TypeInfoSequence primitiveInfo = new(ClassNameHelper.FloatPrimitiveHash, 5, 1);
	/// <summary>
	/// Wrapper type info.
	/// </summary>
	private static readonly TypeInfoSequence wrapperInfo = new(ClassNameHelper.FloatObjectHash, 15);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new IPrimitiveValueType.PrimitiveTypeMetadata(JFloat.primitiveInfo, JFloat.wrapperInfo);

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JFloat;

	static TypeMetadata IPrimitiveType<JFloat>.Metadata => JFloat.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JFloat.Type;

	/// <summary>
	/// Internal single-precision floating-point number value.
	/// </summary>
	private readonly Single _value;

	/// <summary>
	/// <see cref="Single"/> representation of the current instance.
	/// </summary>
	public Single Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JFloat>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JFloat>().Signature;

#if PACKAGE
	JLocalObject IPrimitiveType.ToObject(IEnvironment env) => this.ToObject(env);
#endif

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JFloat other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JFloat other) => this._value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveValueType.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JFloat value) => new JPrimitiveObject<JFloat>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JFloat(JObject jObj)
		=> CommonValidationUtilities.ThrowIfInvalidCast<Single>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JFloat(Single value) => new(value);

	static JFloat IPrimitiveNumericType<JFloat>.FromDouble(Double value) => IPrimitiveNumericType.GetSingleValue(value);
	static Double IPrimitiveNumericType<JFloat>.ToDouble(JFloat value) => value._value;
}