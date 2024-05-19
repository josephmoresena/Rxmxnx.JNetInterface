namespace Rxmxnx.JNetInterface.Primitives;

/// <summary>
/// Primitive <c>char</c>. Represents a character as a UTF-16 code unit.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JChar : INativeType<JChar>, ISelfEquatableComparable<JChar>,
	IPrimitiveIntegerType<JChar, Char>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JPrimitiveTypeMetadata<JChar> typeMetadata = IPrimitiveType<JChar, Char>.TypeMetadataBuilder
		.Create(UnicodeClassNames.CharPrimitive(), UnicodePrimitiveSignatures.CharSignatureChar)
		.WithWrapperClassName(UnicodeClassNames.CharacterObject()).Build();

	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JChar;

	static JPrimitiveTypeMetadata<JChar> IPrimitiveType<JChar>.Metadata => JChar.typeMetadata;
	static JNativeType IPrimitiveType.JniType => JChar.Type;

	/// <summary>
	/// Internal UTF-16 code unit character.
	/// </summary>
	private readonly Char _value;

	/// <summary>
	/// <see cref="Char"/> representation of the current instance.
	/// </summary>
	public Char Value => this._value;
	/// <inheritdoc/>
	public CString ObjectClassName => IPrimitiveType.GetMetadata<JChar>().ClassName;
	/// <inheritdoc/>
	public CString ObjectSignature => IPrimitiveType.GetMetadata<JChar>().Signature;

#if PACKAGE
	JLocalObject IPrimitiveType.ToObject(IEnvironment env) => this.ToObject(env);
#endif

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean Equals(JChar other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(JChar other) => this._value.CompareTo(other._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 CompareTo(Object? obj) => IPrimitiveType<JChar, Char>.Compare(this, obj);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JObject(JChar value) => new JPrimitiveObject<JChar>(value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JChar(JObject jObj)
		=> CommonValidationUtilities.ThrowIfInvalidCast<Char>(jObj as IConvertible);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator JChar(Char value) => new(value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="Int16"/> to <see cref="JChar"/>.
	/// </summary>
	/// <param name="value">A <see cref="Int16"/> to explicitly convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator JChar(Int16 value) => new((Char)value);

	static JChar IPrimitiveNumericType<JChar>.FromDouble(Double value) => new(value);
	static Double IPrimitiveNumericType<JChar>.ToDouble(JChar value) => value._value;
}