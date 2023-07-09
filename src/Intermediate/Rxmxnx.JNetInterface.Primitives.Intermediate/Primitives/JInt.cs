namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>int</c>. Represents a primitive 32-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JInt : INative<JInt>, ISelfEquatableComparable<JInt>, IPrimitiveInteger<JInt, Int32>,
	IPrimitiveSigned<JInt, Int32>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JInt;
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JIntegerObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodePrimitiveSignatures.JIntSignature;
	/// <inheritdoc/>
	public static CString ArraySignature => UnicodePrimitiveArraySignatures.JIntArraySignature;
	/// <inheritdoc/>
	public static CString ClassSignature => UnicodeObjectSignatures.JIntegerObjectSignature;
	/// <inheritdoc cref="IPrimitive.PrimitiveMetadata"/>
	public static JPrimitiveMetadata PrimitiveMetadata => new JPrimitiveMetadata<JInt>();

	/// <summary>
	/// Internal 32-bit signed integer value.
	/// </summary>
	private readonly Int32 _value;

	/// <summary>
	/// <see cref="Int32"/> representation of current instance.
	/// </summary>
	public Int32 Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => JInt.ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => JInt.Signature;
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
	public Int32 CompareTo(Object? obj) => JPrimitiveMetadata.Compare<JInt, Int32>(this, obj);

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
}