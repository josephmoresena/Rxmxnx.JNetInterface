namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>byte</c>. Represents a 8-bit signed integer.
/// </summary>
public readonly partial struct JByte : INativeType<JByte>, ISelfEquatableComparable<JByte>,
	IPrimitiveNumericIntegerType<JByte, SByte>, IPrimitiveNumericSignedType<JByte, SByte>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata typeMetadata = IPrimitiveType<JByte, SByte>.JTypeMetadataBuilder
		.Create(UnicodePrimitiveSignatures.JByteSignature).WithWrapperClassName(UnicodeClassNames.JByteObjectClassName)
		.WithArraySignature(UnicodePrimitiveArraySignatures.JByteArraySignature)
		.WithWrapperClassSignature(UnicodeObjectSignatures.JByteObjectSignature).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JByte;

	static JDataTypeMetadata IDataType.Metadata => JByte.typeMetadata;
	static Type? IDataType.FamilyType => default;

	/// <summary>
	/// Internal 8-bit signed integer value.
	/// </summary>
	private readonly SByte _value;

	/// <summary>
	/// <see cref="SByte"/> representation of current instance.
	/// </summary>
	public SByte Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JByte>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JByte>().Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JByte() => this._value = default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JByte(SByte value) => this._value = value;

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
		=> ValidationUtilities.ThrowIfInvalidCast<SByte>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JByte(SByte value) => new(value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JByte Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JByte, SByte>();
	}
}