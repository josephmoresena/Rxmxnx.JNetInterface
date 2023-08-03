namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>char</c>. Represents a character as a UTF-16 code unit.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JChar : INativeType<JChar>, ISelfEquatableComparable<JChar>,
	IPrimitiveNumericIntegerType<JChar, Char>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveMetadata metadata = IPrimitiveType<JChar, Char>.JPrimitiveMetadataBuilder
		.Create(UnicodePrimitiveSignatures.JCharSignature)
		.WithWrapperClassName(UnicodeClassNames.JCharacterObjectClassName)
		.WithArraySignature(UnicodePrimitiveArraySignatures.JCharArraySignature)
		.WithWrapperClassSignature(UnicodeObjectSignatures.JCharacterObjectSignature).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JChar;

	static JDataTypeMetadata IDataType.Metadata => JChar.metadata;

	/// <summary>
	/// Internal UTF-16 code unit character.
	/// </summary>
	private readonly Char _value;

	/// <summary>
	/// <see cref="Char"/> representation of current instance.
	/// </summary>
	public Char Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JChar>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JChar>().Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JChar() => this._value = default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JChar(Char value) => this._value = value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JChar other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JChar other) => this._value.CompareTo(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JChar, Char>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JChar value) => new JPrimitiveObject<JChar>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JChar(JObject jObj)
		=> ValidationUtilities.ThrowIfInvalidCast<Char>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JChar(Char value) => new(value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JChar Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JChar, Char>();
	}
}