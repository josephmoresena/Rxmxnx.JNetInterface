namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>double</c>. Represents a double-precision floating-point number.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JDouble : INativeType<JDouble>, ISelfEquatableComparable<JDouble>,
	IPrimitiveNumericFloatingPointType<JDouble, Double>, IPrimitiveNumericSignedType<JDouble, Double>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata typeMetadata = IPrimitiveType<JDouble, Double>.JTypeMetadataBuilder
		.Create(UnicodePrimitiveSignatures.JDoubleSignature)
		.WithWrapperClassName(UnicodeClassNames.JDoubleObjectClassName)
		.WithArraySignature(UnicodePrimitiveArraySignatures.JDoubleArraySignature)
		.WithWrapperClassSignature(UnicodeObjectSignatures.JDoubleObjectSignature).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JDouble;

	static JDataTypeMetadata IDataType.Metadata => JDouble.typeMetadata;
	static Type? IDataType.FamilyType => default;
	static JNativeType IPrimitiveType.NativeType => JDouble.Type;

	/// <summary>
	/// Internal double-precision floating-point number value.
	/// </summary>
	private readonly Double _value;

	/// <summary>
	/// <see cref="Double"/> representation of current instance.
	/// </summary>
	public Double Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JDouble>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JDouble>().Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JDouble() => this._value = default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JDouble(Double value) => this._value = value;

	Int64 IPrimitiveNumericType.LongValue => (Int64)this._value;
	Double IPrimitiveNumericType.DoubleValue => this._value;

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
		=> ValidationUtilities.ThrowIfInvalidCast<Double>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JDouble(Double value) => new(value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JDouble Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JDouble, Double>();
	}
}