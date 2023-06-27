namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>byte</c>. Represents a 8-bit signed integer.
/// </summary>
public readonly partial struct JByte : INative<JByte>, IPrimitive<JByte, SByte>, IComparable<JByte>, IEquatable<JByte>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JByte;
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JByteObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodePrimitiveSignatures.JByteSignature;
	/// <inheritdoc/>
	public static CString ArraySignature => UnicodePrimitiveArraySignatures.JByteArraySignature;
	/// <inheritdoc cref="IPrimitive.PrimitiveMetadata"/>
	public static JPrimitiveMetadata PrimitiveMetadata => new JPrimitiveMetadata<JByte>();

	/// <summary>
	/// Internal 8-bit signed integer value.
	/// </summary>
	private readonly SByte _value;

	/// <summary>
	/// <see cref="SByte"/> representation of current instance.
	/// </summary>
	public SByte Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => JByte.ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => JByte.Signature;
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
	public Int32 CompareTo(Object? obj) => JPrimitiveMetadata.Compare<JByte, SByte>(this, obj);

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