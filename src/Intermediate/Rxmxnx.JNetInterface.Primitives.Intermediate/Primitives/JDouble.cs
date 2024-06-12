namespace Rxmxnx.JNetInterface.Primitives;

using TypeMetadata = JPrimitiveTypeMetadata<JDouble>;

/// <summary>
/// Primitive <c>double</c>. Represents a double-precision floating-point number.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JDouble : INativeType, IComparable<JDouble>, IEquatable<JDouble>,
	IPrimitiveFloatingPointType<JDouble, Double>, IPrimitiveSignedType<JDouble, Double>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = IPrimitiveType<JDouble, Double>.TypeMetadataBuilder
		.Create("double"u8, CommonNames.DoubleSignatureChar).WithWrapperClassName("java/lang/Double"u8).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JDouble;

	static TypeMetadata IPrimitiveType<JDouble>.Metadata => JDouble.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JDouble.Type;

	/// <summary>
	/// Internal double-precision floating-point number value.
	/// </summary>
	private readonly Double _value;

	/// <summary>
	/// <see cref="Double"/> representation of the current instance.
	/// </summary>
	public Double Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JDouble>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JDouble>().Signature;

#if PACKAGE
	JLocalObject IPrimitiveType.ToObject(IEnvironment env) => this.ToObject(env);
#endif

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JDouble other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JDouble other) => this._value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JDouble, Double>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JDouble value) => new JPrimitiveObject<JDouble>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JDouble(JObject jObj)
		=> CommonValidationUtilities.ThrowIfInvalidCast<Double>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JDouble(Double value) => new(value);

	static JDouble IPrimitiveNumericType<JDouble>.FromDouble(Double value) => new(value);
	static Double IPrimitiveNumericType<JDouble>.ToDouble(JDouble value) => value._value;
}