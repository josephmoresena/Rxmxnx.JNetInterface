namespace Rxmxnx.JNetInterface.Primitives;

using TypeMetadata = JPrimitiveTypeMetadata<JByte>;
using IPrimitiveValueType = IPrimitiveType<JByte, SByte>;
using IPrimitiveNumericType = IPrimitiveNumericType<JByte>;

/// <summary>
/// Primitive <c>byte</c>. Represents an 8-bit signed integer.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = sizeof(SByte), Pack = 0)]
public readonly partial struct JByte : IPrimitiveIntegerType, IPrimitiveNumericType, IPrimitiveValueType
{
	/// <summary>
	/// Primitive type info.
	/// </summary>
	private static readonly TypeInfoSequence primitiveInfo = new(ClassNameHelper.BytePrimitiveHash, 4, 1);
	/// <summary>
	/// Wrapper type info.
	/// </summary>
	private static readonly TypeInfoSequence wrapperInfo = new(ClassNameHelper.ByteObjectHash, 14);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new IPrimitiveValueType.PrimitiveTypeMetadata(JByte.primitiveInfo, JByte.wrapperInfo);

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JByte;

	static TypeMetadata IPrimitiveType<JByte>.Metadata => JByte.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JByte.Type;

	/// <summary>
	/// Internal 8-bit signed integer value.
	/// </summary>
	[FieldOffset(0)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly SByte _value;

	/// <summary>
	/// <see cref="SByte"/> representation of the current instance.
	/// </summary>
	// ReSharper disable once ConvertToAutoPropertyWhenPossible
	public SByte Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JByte>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JByte>().Signature;

#if PACKAGE
	JLocalObject IPrimitiveType.ToObject(IEnvironment env) => this.ToObject(env);
#endif

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JByte other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JByte other) => this._value.CompareTo(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveValueType.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JByte value) => new JPrimitiveObject<JByte>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JByte(JObject jObj)
		=> CommonValidationUtilities.ThrowIfInvalidCast<SByte>(jObj as IConvertible);
	/// <inheritdoc cref="INativeDataType{TNativeType}.op_Implicit(SByte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JByte(SByte value) => new(value);

	static JByte IPrimitiveNumericType.FromDouble(Double value) => new(value);
	static Double IPrimitiveNumericType.ToDouble(JByte value) => value._value;
}