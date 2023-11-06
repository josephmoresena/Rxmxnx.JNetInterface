namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>short</c>. Represents a primitive 16-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JShort : INativeType<JShort>, ISelfEquatableComparable<JShort>,
	IPrimitiveNumericIntegerType<JShort, Int16>, IPrimitiveNumericSignedType<JShort, Int16>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata typeMetadata = IPrimitiveType<JShort, Int16>.JTypeMetadataBuilder
		.Create(UnicodePrimitiveSignatures.JShortSignature)
		.WithWrapperClassName(UnicodeClassNames.JShortObjectClassName)
		.WithArraySignature(UnicodePrimitiveArraySignatures.JShortArraySignature)
		.WithWrapperClassSignature(UnicodeObjectSignatures.JShortObjectSignature).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JShort;

	static JDataTypeMetadata IDataType.Metadata => JShort.typeMetadata;
	static Type? IDataType.FamilyType => default;
	static JNativeType IPrimitiveType.NativeType => JShort.Type;

	/// <summary>
	/// Internal 16-bit signed integer value.
	/// </summary>
	private readonly Int16 _value;

	/// <summary>
	/// <see cref="Int16"/> representation of current instance.
	/// </summary>
	public Int16 Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JShort>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JShort>().Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JShort() => this._value = default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JShort(Int16 value) => this._value = value;

	Int64 IPrimitiveNumericType.LongValue => this._value;
	Double IPrimitiveNumericType.DoubleValue => this._value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JShort other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JShort other) => this._value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JShort, Int16>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JShort value) => new JPrimitiveObject<JShort>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JShort(JObject jObj)
		=> ValidationUtilities.ThrowIfInvalidCast<Int16>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JShort(Int16 value) => new(value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JShort Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JShort, Int16>();
	}
}