namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>float</c>. Represents a single-precision floating-point number.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JFloat : INativeType<JFloat>, ISelfEquatableComparable<JFloat>,
	IPrimitiveFloatingPointType<JFloat, Single>, IPrimitiveSignedType<JFloat, Single>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata<JFloat> typeMetadata = IPrimitiveType<JFloat, Single>
	                                                                      .JTypeMetadataBuilder
	                                                                      .Create(UnicodeClassNames.FloatPrimitive(),
		                                                                      UnicodePrimitiveSignatures
			                                                                      .FloatSignatureChar)
	                                                                      .WithWrapperClassName(
		                                                                      UnicodeClassNames.FloatObject()).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JFloat;

	static JPrimitiveTypeMetadata<JFloat> IPrimitiveType<JFloat>.Metadata => JFloat.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JFloat.Type;

	/// <summary>
	/// Internal single-precision floating-point number value.
	/// </summary>
	private readonly Single _value;

	/// <summary>
	/// <see cref="Single"/> representation of current instance.
	/// </summary>
	public Single Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JFloat>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JFloat>().Signature;

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
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JFloat, Single>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JFloat value) => new JPrimitiveObject<JFloat>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JFloat(JObject jObj)
		=> ValidationUtilities.ThrowIfInvalidCast<Single>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JFloat(Single value) => new(value);

	static JFloat IPrimitiveNumericType<JFloat>.FromDouble(Double value) => (Single)value;
	static Double IPrimitiveNumericType<JFloat>.ToDouble(JFloat value) => value._value;
}