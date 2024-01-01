namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>long</c>. Represents a primitive 64-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JLong : INativeType<JLong>, ISelfEquatableComparable<JLong>,
	IPrimitiveIntegerType<JLong, Int64>, IPrimitiveSignedType<JLong, Int64>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata typeMetadata = IPrimitiveType<JLong, Int64>.JTypeMetadataBuilder
		.Create("long"u8, "J"u8).WithWrapperClassName("java/lang/Long"u8).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JLong;

	static JDataTypeMetadata IDataType.Metadata => JLong.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JLong.Type;

	/// <summary>
	/// Internal 64-bit signed integer value.
	/// </summary>
	private readonly Int64 _value;

	/// <summary>
	/// <see cref="Int64"/> representation of current instance.
	/// </summary>
	public Int64 Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JLong>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JLong>().Signature;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JLong() => this._value = default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JLong(Int64 value) => this._value = value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JLong other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JLong other) => this._value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JLong, Int64>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JLong value) => new JPrimitiveObject<JLong>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JLong(JObject jObj)
		=> ValidationUtilities.ThrowIfInvalidCast<Int64>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JLong(Int64 value) => new(value);

	static JLong IPrimitiveNumericType<JLong>.FromDouble(Double value)
		=> IPrimitiveNumericType.GetIntegerValue<Int64>(value);
	static Double IPrimitiveNumericType<JLong>.ToDouble(JLong value) => value._value;
}