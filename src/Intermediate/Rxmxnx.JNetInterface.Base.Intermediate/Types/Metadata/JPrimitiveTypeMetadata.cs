namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
/// </summary>
public abstract partial record JPrimitiveTypeMetadata : JDataTypeMetadata
{
	/// <summary>
	/// Java <c>void</c> type information.
	/// </summary>
	private static readonly CStringSequence voidInformation = new(UnicodeClassNames.VoidPrimitive(),
	                                                              stackalloc Byte[1]
	                                                              {
		                                                              UnicodePrimitiveSignatures.VoidSignatureChar,
	                                                              }, ReadOnlySpan<Byte>.Empty);

	/// <summary>
	/// <see cref="JPrimitiveTypeMetadata"/> instance for Java <c>void</c> type.
	/// </summary>
	public static readonly JPrimitiveTypeMetadata VoidMetadata = new JVoidTypeMetadata();

	/// <summary>
	/// Fake class hash for primitive void.
	/// </summary>
	internal static String FakeVoidHash => JVoidTypeMetadata.FakeHash;

	/// <inheritdoc cref="SizeOf"/>
	private readonly Int32 _sizeOf;
	/// <summary>
	/// CLR underline type.
	/// </summary>
	private readonly Type _underlineType;

	/// <summary>
	/// JNI name for the current type wrapper class.
	/// </summary>
	public CString WrapperClassSignature => this.WrapperInformation[1];
	/// <summary>
	/// JNI signature for the current type wrapper class.
	/// </summary>
	public CString WrapperClassName => this.WrapperInformation[0];
	/// <summary>
	/// Underline primitive CLR type.
	/// </summary>
	public Type UnderlineType => this._underlineType;
	/// <summary>
	/// Native primitive type.
	/// </summary>
	public abstract JNativeType NativeType { get; }
	/// <summary>
	/// Size of the current primitive type in bytes.
	/// </summary>
	public override Int32 SizeOf => this._sizeOf;

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Primitive;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Information of wrapper class.
	/// </summary>
	internal CStringSequence WrapperInformation { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	private JPrimitiveTypeMetadata() : base(JPrimitiveTypeMetadata.voidInformation)
	{
		this._sizeOf = default;
		this._underlineType = typeof(void);
		this.WrapperInformation = JDataTypeMetadata.CreateInformationSequence(UnicodeClassNames.VoidObject());
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="sizeOf">Size of the current primitive type in bytes.</param>
	/// <param name="underlineType">Underline primitive CLR type.</param>
	/// <param name="signature">JNI signature for the current primitive type.</param>
	/// <param name="className">Wrapper class name of the current primitive type.</param>
	/// <param name="wrapperClassName">Wrapper class JNI name of the current primitive type.</param>
	private protected JPrimitiveTypeMetadata(Int32 sizeOf, Type underlineType, ReadOnlySpan<Byte> signature,
		ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> wrapperClassName) : base(className, signature)
	{
		this._sizeOf = sizeOf;
		this._underlineType = underlineType;
		this.WrapperInformation = JDataTypeMetadata.CreateInformationSequence(wrapperClassName);
	}

	/// <summary>
	/// Creates a <see cref="IPrimitiveType"/> value from <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Binary read-only span.</param>
	/// <returns>A <see cref="IPrimitiveType"/> value from <paramref name="bytes"/>.</returns>
	public abstract IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes);

	/// <inheritdoc/>
	public override String ToString()
		=> base.ToString() + $"{nameof(JPrimitiveTypeMetadata.UnderlineType)} = {this.UnderlineType}, " +
			$"{nameof(JPrimitiveTypeMetadata.NativeType)} = {this.NativeType}, " +
			$"{nameof(JPrimitiveTypeMetadata.WrapperClassName)} = {this.WrapperClassName}, ";
}

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
public abstract record JPrimitiveTypeMetadata<TPrimitive> : JPrimitiveTypeMetadata
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <inheritdoc/>
	private protected JPrimitiveTypeMetadata(Type underlineType, ReadOnlySpan<Byte> signature,
		ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> wrapperClassName) : base(
		NativeUtilities.SizeOf<TPrimitive>(), underlineType, signature, className, wrapperClassName) { }

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
}