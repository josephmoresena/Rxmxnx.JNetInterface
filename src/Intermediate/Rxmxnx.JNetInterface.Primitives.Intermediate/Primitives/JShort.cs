namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>short</c>. Represents a primitive 16-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JShort : INativeType<JShort>, ISelfEquatableComparable<JShort>,
	IPrimitiveIntegerType<JShort, Int16>, IPrimitiveSignedType<JShort, Int16>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata typeMetadata = IPrimitiveType<JShort, Int16>.JTypeMetadataBuilder
		.Create(UnicodePrimitiveSignatures.JShortSignature)
		.WithWrapperClassName(UnicodeClassNames.JShortObjectClassName)
		.WithWrapperClassSignature(UnicodeObjectSignatures.JShortObjectSignature).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JShort;

	static JDataTypeMetadata IDataType.Metadata => JShort.typeMetadata;
	static Type? IDataType.FamilyType => default;
	static JNativeType IPrimitiveType.JniType => JShort.Type;

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
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="Char"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="Int16"/> to explicitly convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JShort(Char value) => new((Int16)value);

	static JShort IPrimitiveNumericType<JShort>.FromDouble(Double value)
		=> IPrimitiveNumericType.GetIntegerValue<Int16>(value);
	static Double IPrimitiveNumericType<JShort>.ToDouble(JShort value) => value._value;
}