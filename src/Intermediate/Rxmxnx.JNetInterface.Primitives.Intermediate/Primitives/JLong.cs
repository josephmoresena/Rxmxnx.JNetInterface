namespace Rxmxnx.JNetInterface.Primitives;

using TypeMetadata = JPrimitiveTypeMetadata<JLong>;
using IPrimitiveValueType = IPrimitiveType<JLong, Int64>;
using IPrimitiveIntegerType = IPrimitiveIntegerType<JLong, Int64>;
using IPrimitiveSignedType = IPrimitiveSignedType<JLong, Int64>;

/// <summary>
/// Primitive <c>long</c>. Represents a primitive 64-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JLong : INativeType, IComparable<JLong>, IEquatable<JLong>, IPrimitiveIntegerType,
	IPrimitiveSignedType
{
	/// <summary>
	/// Primitive type info.
	/// </summary>
	private static readonly TypeInfoSequence primitiveInfo = new(ClassNameHelper.LongPrimitiveHash, 4, 1);
	/// <summary>
	/// Wrapper type info.
	/// </summary>
	private static readonly TypeInfoSequence wrapperInfo = new(ClassNameHelper.LongObjectHash, 14, 16);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new IPrimitiveValueType.PrimitiveTypeMetadata(JLong.primitiveInfo, JLong.wrapperInfo);

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JLong;

	static TypeMetadata IPrimitiveType<JLong>.Metadata => JLong.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JLong.Type;

	/// <summary>
	/// Internal 64-bit signed integer value.
	/// </summary>
	private readonly Int64 _value;

	/// <summary>
	/// <see cref="Int64"/> representation of the current instance.
	/// </summary>
	public Int64 Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JLong>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JLong>().Signature;

#if PACKAGE
	JLocalObject IPrimitiveType.ToObject(IEnvironment env) => this.ToObject(env);
#endif

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
		=> CommonValidationUtilities.ThrowIfInvalidCast<Int64>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JLong(Int64 value) => new(value);

	static JLong IPrimitiveNumericType<JLong>.FromDouble(Double value) => new(value);
	static Double IPrimitiveNumericType<JLong>.ToDouble(JLong value) => value._value;
}