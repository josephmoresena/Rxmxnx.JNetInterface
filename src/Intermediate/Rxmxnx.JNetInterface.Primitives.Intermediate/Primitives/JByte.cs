namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>byte</c>. Represents a 8-bit signed integer.
/// </summary>
public readonly record struct JByte : INative<JByte>, IPrimitive<JByte, SByte>, IComparable<JByte>
{
	/// <summary>
	/// Internal 8-bit signed integer value.
	/// </summary>
	private readonly SByte _value;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public JByte() => this._value = default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	private JByte(SByte value) => this._value = value;
	/// <inheritdoc/>
	public Int32 CompareTo(JByte other) => this.Value.CompareTo(other.Value);
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JByte;
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JByteObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodePrimitiveSignatures.JByteSignature;
	/// <inheritdoc/>
	public static CString ArraySignature => UnicodePrimitiveArraySignatures.JByteArraySignature;
	/// <inheritdoc cref="IPrimitive.PrimitiveMetadata"/>
	public static JPrimitiveMetadata PrimitiveMetadata => new JPrimitiveMetadata<JByte, SByte>();

	/// <inheritdoc/>
	public CString ObjectClassName => JByte.ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => JByte.Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this.Value == default;

	/// <summary>
	/// <see cref="SByte"/> representation of current instance.
	/// </summary>
	public SByte Value => this._value;

	/// <inheritdoc/>
	public Int32 CompareTo(Object? obj) => this.Value.CompareTo(obj);

	/// <inheritdoc/>
	public static JByte Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JByte, SByte>();
	}
	/// <inheritdoc/>
	public static implicit operator JByte(SByte value) => new(value);
}