namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>int</c>. Represents a primitive 32-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JInt : INativeType<JInt>, ISelfEquatableComparable<JInt>,
	IPrimitiveNumericIntegerType<JInt, Int32>, IPrimitiveNumericSignedType<JInt, Int32>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata typeMetadata = IPrimitiveType<JInt, Int32>.JTypeMetadataBuilder
		.Create(UnicodePrimitiveSignatures.JIntSignature)
		.WithWrapperClassName(UnicodeClassNames.JIntegerObjectClassName)
		.WithArraySignature(UnicodePrimitiveArraySignatures.JIntArraySignature)
		.WithWrapperClassSignature(UnicodeObjectSignatures.JIntegerObjectSignature).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JInt;

	static JDataTypeMetadata IDataType.Metadata => JInt.typeMetadata;
	static Type? IDataType.FamilyType => default;
	static JNativeType IPrimitiveType.NativeType => JInt.Type;

	/// <summary>
	/// Internal 32-bit signed integer value.
	/// </summary>
	private readonly Int32 _value;

	/// <summary>
	/// <see cref="Int32"/> representation of current instance.
	/// </summary>
	public Int32 Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JFloat>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JFloat>().Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JInt() => this._value = default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JInt(Int32 value) => this._value = value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JInt other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JInt other) => this._value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JInt, Int32>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JInt value) => new JPrimitiveObject<JInt>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JInt(JObject jObj)
		=> ValidationUtilities.ThrowIfInvalidCast<Int32>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JInt(Int32 value) => new(value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JInt Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JInt, Int32>();
	}

	static JInt IPrimitiveNumericType<JInt>.FromDouble(Double value)
		=> IPrimitiveNumericType.GetIntegerValue<Int32>(value);
	static Double IPrimitiveNumericType<JInt>.ToDouble(JInt value) => value._value;
}