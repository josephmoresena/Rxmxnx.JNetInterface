namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>boolean</c>. Represents a Boolean (<see langword="true"/> or <see langword="false"/>) value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct JBoolean : IPrimitive<JBoolean, Boolean>, IComparable<JBoolean>
{
	/// <summary>
	/// Unsigned byte value for <see langword="true"/> value.
	/// </summary>
	private const Byte trueValue = 0x01;
	/// <summary>
	/// Unsigned byte value for <see langword="false"/> value.
	/// </summary>
	private const Byte falseValue = 0x00;

	/// <summary>
	/// Internal 8-bit unsigned integer value.
	/// </summary>
	private readonly Byte _value;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public JBoolean() => this._value = JBoolean.falseValue;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	private JBoolean(Boolean value) => this._value = value ? JBoolean.trueValue : JBoolean.falseValue;
	/// <inheritdoc/>
	public Int32 CompareTo(JBoolean other) => this.Value.CompareTo(other.Value);
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JBooleanObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodePrimitiveSignatures.JBooleanSignature;
	/// <inheritdoc/>
	public static CString ArraySignature => UnicodePrimitiveArraySignatures.JBooleanArraySignature;
	/// <inheritdoc cref="IPrimitive.PrimitiveMetadata"/>
	public static JPrimitiveMetadata PrimitiveMetadata => new JPrimitiveMetadata<JBoolean, Byte>();

	/// <inheritdoc/>
	public CString ObjectClassName => JBoolean.ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => JBoolean.Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this.Value == default;

	/// <summary>
	/// <see cref="Boolean"/> representation of current instance.
	/// </summary>
	public Boolean Value => this._value.Equals(JBoolean.trueValue);

	/// <inheritdoc/>
	public Int32 CompareTo(Object? obj) => this.Value.CompareTo(obj);

	/// <inheritdoc/>
	public static JBoolean Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JBoolean, Boolean>();
	}
	/// <inheritdoc/>
	public static implicit operator JBoolean(Boolean value) => new(value);
}