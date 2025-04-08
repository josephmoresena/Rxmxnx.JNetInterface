namespace Rxmxnx.JNetInterface.Primitives;

using TypeMetadata = JPrimitiveTypeMetadata<JBoolean>;
using IPrimitiveValueType = IPrimitiveType<JBoolean, Boolean>;

/// <summary>
/// Primitive <c>boolean</c>. Represents a Boolean (<see langword="true"/> or <see langword="false"/>) value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1210,
                 Justification = CommonConstants.NoBooleanComparisonOperatorsJustification)]
#endif
public readonly partial struct JBoolean : INativeType, IComparable<JBoolean>, IEquatable<JBoolean>, IPrimitiveValueType
{
	/// <summary>
	/// Primitive type info.
	/// </summary>
	private static readonly TypeInfoSequence primitiveInfo = new(ClassNameHelper.BooleanPrimitiveHash, 7, 1);
	/// <summary>
	/// Wrapper type info.
	/// </summary>
	private static readonly TypeInfoSequence wrapperInfo = new(ClassNameHelper.BooleanObjectHash, 17);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new IPrimitiveValueType.PrimitiveTypeMetadata(JBoolean.primitiveInfo, JBoolean.wrapperInfo);

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JBoolean;

	static TypeMetadata IPrimitiveType<JBoolean>.Metadata => JBoolean.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JBoolean.Type;

	/// <summary>
	/// Internal 8-bit unsigned integer value.
	/// </summary>
	private readonly Byte _value;

	/// <summary>
	/// <see cref="Boolean"/> representation of the current instance.
	/// </summary>
	public Boolean Value => !this._value.Equals(JBoolean.FalseValue);
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JBoolean>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JBoolean>().Signature;
	/// <summary>
	/// Internal byte value.
	/// </summary>
	public Byte ByteValue => this._value;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JBoolean() => this._value = JBoolean.FalseValue;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JBoolean(Boolean value) => this._value = value ? JBoolean.TrueValue : JBoolean.FalseValue;

#if PACKAGE
	JLocalObject IPrimitiveType.ToObject(IEnvironment env) => this.ToObject(env);
#endif

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JBoolean other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JBoolean other) => this.Value.CompareTo(other.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveValueType.Compare(this, obj);
	/// <inheritdoc cref="Boolean.TryFormat(Span{Char}, out Int32)"/>
	public Boolean TryFormat(Span<Char> destination, out Int32 charsWritten)
		=> this.Value.TryFormat(destination, out charsWritten);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JBoolean value) => new JPrimitiveObject<JBoolean>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JBoolean(JObject jObj)
		=> CommonValidationUtilities.ThrowIfInvalidCast<Boolean>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JBoolean(Boolean value) => new(value);

	/// <inheritdoc cref="Boolean.Parse(String)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JBoolean Parse(String value) => Boolean.Parse(value);
	/// <inheritdoc cref="Boolean.Parse(ReadOnlySpan{Char})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JBoolean Parse(ReadOnlySpan<Char> value) => Boolean.Parse(value);
	/// <inheritdoc cref="Boolean.TryParse(String, out Boolean)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse([NotNullWhen(true)] String? value, out JBoolean result)
	{
		Boolean parseResult = Boolean.TryParse(value, out Boolean boolResult);
		result = parseResult && boolResult;
		return parseResult;
	}
	/// <inheritdoc cref="Boolean.TryParse(ReadOnlySpan{Char}, out Boolean)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> value, out JBoolean result)
	{
		Boolean parseResult = Boolean.TryParse(value, out Boolean boolResult);
		result = parseResult && boolResult;
		return parseResult;
	}
}