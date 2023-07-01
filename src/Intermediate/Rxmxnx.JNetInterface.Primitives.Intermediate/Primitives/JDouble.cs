namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>double</c>. Represents a double-precision floating-point number.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JDouble : INative<JDouble>, ISelfEquatableComparable<JDouble>,
	IPrimitiveFloatingPoint<JDouble, Double>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JDouble;
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JDoubleObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodePrimitiveSignatures.JDoubleSignature;
	/// <inheritdoc/>
	public static CString ArraySignature => UnicodePrimitiveArraySignatures.JDoubleArraySignature;
	/// <inheritdoc cref="IPrimitive.PrimitiveMetadata"/>
	public static JPrimitiveMetadata PrimitiveMetadata => new JPrimitiveMetadata<JDouble>();

	/// <summary>
	/// Internal double-precision floating-point number value.
	/// </summary>
	private readonly Double _value;

	/// <summary>
	/// <see cref="Double"/> representation of current instance.
	/// </summary>
	public Double Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => JDouble.ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => JDouble.Signature;
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

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JDouble other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JDouble other) => this._value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => JPrimitiveMetadata.Compare<JDouble, Double>(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JDouble value) => new JPrimitiveObject<JDouble>(value);
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