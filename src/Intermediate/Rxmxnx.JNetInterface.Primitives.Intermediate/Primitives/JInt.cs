namespace Rxmxnx.JNetInterface.Primitives;

using TypeMetadata = JPrimitiveTypeMetadata<JInt>;

/// <summary>
/// Primitive <c>int</c>. Represents a primitive 32-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JInt : INativeType<JInt>, IComparable<JInt>, IEquatable<JInt>,
	IPrimitiveIntegerType<JInt, Int32>, IPrimitiveSignedType<JInt, Int32>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = IPrimitiveType<JInt, Int32>.TypeMetadataBuilder
		.Create("int"u8, CommonNames.IntSignatureChar).WithWrapperClassName("java/lang/Integer"u8).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JInt;

	static TypeMetadata IPrimitiveType<JInt>.Metadata => JInt.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JInt.Type;

	/// <summary>
	/// Internal 32-bit signed integer value.
	/// </summary>
	private readonly Int32 _value;

	/// <summary>
	/// <see cref="Int32"/> representation of the current instance.
	/// </summary>
	public Int32 Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JInt>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JInt>().Signature;

#if PACKAGE
	JLocalObject IPrimitiveType.ToObject(IEnvironment env) => this.ToObject(env);
#endif

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JInt other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JInt other) => this._value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JInt, Int32>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JInt value) => new JPrimitiveObject<JInt>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JInt(JObject jObj)
		=> CommonValidationUtilities.ThrowIfInvalidCast<Int32>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JInt(Int32 value) => new(value);

	static JInt IPrimitiveNumericType<JInt>.FromDouble(Double value) => new(value);
	static Double IPrimitiveNumericType<JInt>.ToDouble(JInt value) => value._value;
}