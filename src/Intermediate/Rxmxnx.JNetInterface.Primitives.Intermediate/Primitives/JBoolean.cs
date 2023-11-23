namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>boolean</c>. Represents a Boolean (<see langword="true"/> or <see langword="false"/>) value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JBoolean : INativeType<JBoolean>, ISelfEquatableComparable<JBoolean>,
	IPrimitiveType<JBoolean, Boolean>
{
	/// <summary>
	/// Unsigned byte value for <see langword="true"/> value.
	/// </summary>
	internal const Byte TrueValue = 0x01;
	/// <summary>
	/// Unsigned byte value for <see langword="false"/> value.
	/// </summary>
	internal const Byte FalseValue = 0x00;

	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata typeMetadata = IPrimitiveType<JBoolean, Boolean>.JTypeMetadataBuilder
		.Create(UnicodePrimitiveSignatures.JBooleanSignature)
		.WithWrapperClassName(UnicodeClassNames.JBooleanObjectClassName)
		.WithArraySignature(UnicodePrimitiveArraySignatures.JBooleanArraySignature)
		.WithWrapperClassSignature(UnicodeObjectSignatures.JBooleanObjectSignature).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JBoolean;

	static JDataTypeMetadata IDataType.Metadata => JBoolean.typeMetadata;
	static Type? IDataType.FamilyType => default;
	static JNativeType IPrimitiveType.JniType => JBoolean.Type;

	/// <summary>
	/// Internal 8-bit unsigned integer value.
	/// </summary>
	private readonly Byte _value;

	/// <summary>
	/// <see cref="Boolean"/> representation of current instance.
	/// </summary>
	public Boolean Value => this._value.Equals(JBoolean.TrueValue);
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JBoolean>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JBoolean>().Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);
	/// <summary>
	/// Internal byte value.
	/// </summary>
	internal Byte ByteValue => this._value;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JBoolean() => this._value = JBoolean.FalseValue;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value"><see cref="Byte"/> value.</param>
	internal JBoolean(Byte value) => this._value = value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JBoolean(Boolean value) => this._value = value ? JBoolean.TrueValue : JBoolean.FalseValue;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JBoolean other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JBoolean other) => this.Value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JBoolean, Boolean>.Compare(this, obj);
	/// <inheritdoc cref="Boolean.TryFormat(Span{Char}, out Int32)"/>
	public Boolean TryFormat(Span<Char> destination, out Int32 charsWritten)
		=> this.Value.TryFormat(destination, out charsWritten);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JBoolean value) => new JPrimitiveObject<JBoolean>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JBoolean(JObject jObj)
		=> ValidationUtilities.ThrowIfInvalidCast<Boolean>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JBoolean(Boolean value) => new(value);

	/// <inheritdoc cref="Boolean.Parse(String)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JBoolean Parse(String value) => Boolean.Parse(value);
	/// <inheritdoc cref="Boolean.Parse(ReadOnlySpan{Char})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JBoolean Parse(ReadOnlySpan<Char> value) => Boolean.Parse(value);
	/// <inheritdoc cref="Boolean.TryParse(String, out Boolean)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse([NotNullWhen(true)] String? value, out JBoolean result)
	{
		Boolean parseResult = Boolean.TryParse(value, out Boolean boolResult);
		result = parseResult && boolResult;
		return parseResult;
	}
	/// <inheritdoc cref="Boolean.TryParse(ReadOnlySpan{Char}, out Boolean)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> value, out JBoolean result)
	{
		Boolean parseResult = Boolean.TryParse(value, out Boolean boolResult);
		result = parseResult && boolResult;
		return parseResult;
	}
}