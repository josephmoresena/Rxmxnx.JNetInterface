namespace Rxmxnx.JNetInterface.Primitives;

using TypeMetadata = JPrimitiveTypeMetadata<JByte>;

/// <summary>
/// Primitive <c>byte</c>. Represents a 8-bit signed integer.
/// </summary>
public readonly partial struct JByte : INativeType, IComparable<JByte>, IEquatable<JByte>,
	IPrimitiveIntegerType<JByte, SByte>, IPrimitiveSignedType<JByte, SByte>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = IPrimitiveType<JByte, SByte>.TypeMetadataBuilder
		.Create("byte"u8, CommonNames.ByteSignatureChar).WithWrapperClassName("java/lang/Byte"u8).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JByte;

	static TypeMetadata IPrimitiveType<JByte>.Metadata => JByte.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JByte.Type;

	/// <summary>
	/// Internal 8-bit signed integer value.
	/// </summary>
	private readonly SByte _value;

	/// <summary>
	/// <see cref="SByte"/> representation of the current instance.
	/// </summary>
	public SByte Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JByte>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JByte>().Signature;

#if PACKAGE
	JLocalObject IPrimitiveType.ToObject(IEnvironment env) => this.ToObject(env);
#endif

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JByte other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JByte other) => this._value.CompareTo(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JByte, SByte>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JByte value) => new JPrimitiveObject<JByte>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JByte(JObject jObj)
		=> CommonValidationUtilities.ThrowIfInvalidCast<SByte>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JByte(SByte value) => new(value);

	static JByte IPrimitiveNumericType<JByte>.FromDouble(Double value) => new(value);
	static Double IPrimitiveNumericType<JByte>.ToDouble(JByte value) => value._value;
}