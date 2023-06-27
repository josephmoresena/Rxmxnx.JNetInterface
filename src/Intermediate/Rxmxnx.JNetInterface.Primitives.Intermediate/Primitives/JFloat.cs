namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>float</c>. Represents a single-precision floating-point number.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JFloat : INative<JFloat>, IPrimitiveFloatingPoint<JFloat, Single>, IComparable<JFloat>,
	IEquatable<JFloat>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JFloat;
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JFloatObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodePrimitiveSignatures.JFloatSignature;
	/// <inheritdoc/>
	public static CString ArraySignature => UnicodePrimitiveArraySignatures.JFloatArraySignature;
	/// <inheritdoc cref="IPrimitive.PrimitiveMetadata"/>
	public static JPrimitiveMetadata PrimitiveMetadata => new JPrimitiveMetadata<JFloat>();

	/// <summary>
	/// Internal single-precision floating-point number value.
	/// </summary>
	private readonly Single _value;

	/// <summary>
	/// <see cref="Single"/> representation of current instance.
	/// </summary>
	public Single Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => JFloat.ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => JFloat.Signature;
	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Equals(default);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JFloat() => this._value = default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JFloat(Single value) => this._value = value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JFloat other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JFloat other) => this._value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => JPrimitiveMetadata.Compare<JFloat, Single>(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JFloat value) => new JPrimitiveObject<JFloat>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JFloat(Single value) => new(value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JFloat Create(JObject? jObject)
	{
		ArgumentNullException.ThrowIfNull(jObject);
		return jObject.AsPrimitive<JFloat, Single>();
	}
}