namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>short</c>. Represents a primitive 16-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JShort : INative<JShort>, IPrimitiveInteger<JShort, Int16>, IComparable<JShort>,
	IEquatable<JShort>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JShort;
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JShortObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodePrimitiveSignatures.JShortSignature;
	/// <inheritdoc/>
	public static CString ArraySignature => UnicodePrimitiveArraySignatures.JShortArraySignature;
	/// <inheritdoc cref="IPrimitive.PrimitiveMetadata"/>
	public static JPrimitiveMetadata PrimitiveMetadata => new JPrimitiveMetadata<JShort>();

	/// <summary>
	/// Internal 16-bit signed integer value.
	/// </summary>
	private readonly Int16 _value;

	/// <summary>
	/// <see cref="Int16"/> representation of current instance.
	/// </summary>
	public Int16 Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => JLong.ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => JLong.Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);

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
	public Int32 CompareTo(Object? obj) => JPrimitiveMetadata.Compare<JShort, Int16>(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JShort value) => new JPrimitiveObject<JShort>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JShort(Int16 value) => new(value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JShort Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JShort, Int16>();
	}
}