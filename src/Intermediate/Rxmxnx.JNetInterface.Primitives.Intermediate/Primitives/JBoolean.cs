namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>boolean</c>. Represents a Boolean (<see langword="true"/> or <see langword="false"/>) value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JBoolean : INative<JBoolean>, IPrimitive<JBoolean, Boolean>, IComparable<JBoolean>,
	IEquatable<JBoolean>
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
	public Int32 CompareTo(Object? obj) => JPrimitiveMetadata.CompareTo<JBoolean, Boolean>(this, obj);

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
}