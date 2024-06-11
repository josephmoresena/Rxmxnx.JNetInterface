namespace Rxmxnx.JNetInterface.Primitives;

using TypeMetadata = JPrimitiveTypeMetadata<JFloat>;

/// <summary>
/// Primitive <c>float</c>. Represents a single-precision floating-point number.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JFloat : INativeType, IComparable<JFloat>, IEquatable<JFloat>,
	IPrimitiveFloatingPointType<JFloat, Single>, IPrimitiveSignedType<JFloat, Single>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = IPrimitiveType<JFloat, Single>.TypeMetadataBuilder
		.Create("float"u8, CommonNames.FloatSignatureChar).WithWrapperClassName("java/lang/Float"u8).Build();

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
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JFloat, Single>.Compare(this, obj);

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