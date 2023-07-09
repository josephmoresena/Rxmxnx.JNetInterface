namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>boolean</c>. Represents a Boolean (<see langword="true"/> or <see langword="false"/>) value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JBoolean : INative<JBoolean>, ISelfEquatableComparable<JBoolean>,
	IPrimitive<JBoolean, Boolean>
{
	/// <summary>
	/// Unsigned byte value for <see langword="true"/> value.
	/// </summary>
	private const Byte trueValue = 0x01;
	/// <summary>
	/// Unsigned byte value for <see langword="false"/> value.
	/// </summary>
	private const Byte falseValue = 0x00;

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JBoolean;
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JBooleanObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodePrimitiveSignatures.JBooleanSignature;
	/// <inheritdoc/>
	public static CString ArraySignature => UnicodePrimitiveArraySignatures.JBooleanArraySignature;
	/// <inheritdoc/>
	public static CString ClassSignature => UnicodeObjectSignatures.JBooleanObjectSignature;
	/// <inheritdoc cref="IPrimitive.PrimitiveMetadata"/>
	public static JPrimitiveMetadata PrimitiveMetadata => new JPrimitiveMetadata<JBoolean>();

	/// <summary>
	/// Internal 8-bit unsigned integer value.
	/// </summary>
	private readonly Byte _value;

	/// <summary>
	/// <see cref="Boolean"/> representation of current instance.
	/// </summary>
	public Boolean Value => this._value.Equals(JBoolean.trueValue);
	/// <inheritdoc/>
	public CString ObjectClassName => JBoolean.ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => JBoolean.Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JBoolean() => this._value = JBoolean.falseValue;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JBoolean(Boolean value) => this._value = value ? JBoolean.trueValue : JBoolean.falseValue;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JBoolean other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JBoolean other) => this.Value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => JPrimitiveMetadata.Compare<JBoolean, Boolean>(this, obj);
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

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JBoolean Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JBoolean, Boolean>();
	}
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