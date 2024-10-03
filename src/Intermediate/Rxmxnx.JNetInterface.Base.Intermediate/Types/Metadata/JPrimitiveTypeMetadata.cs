namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
/// </summary>
public abstract partial class JPrimitiveTypeMetadata : JDataTypeMetadata
{
	/// <summary>
	/// <see cref="JPrimitiveTypeMetadata"/> instance for Java <c>void</c> type.
	/// </summary>
	public static readonly JPrimitiveTypeMetadata VoidMetadata = new JVoidTypeMetadata();

	/// <summary>
	/// JNI name for the current type wrapper class.
	/// </summary>
	public CString WrapperClassSignature => this.WrapperInformation.Signature;
	/// <summary>
	/// JNI signature for the current type wrapper class.
	/// </summary>
	public CString WrapperClassName => this.WrapperInformation.ClassName;
	/// <summary>
	/// Underline primitive CLR type.
	/// </summary>
	public Type UnderlineType { get; }
	/// <summary>
	/// Native primitive type.
	/// </summary>
	public abstract JNativeType NativeType { get; }
	/// <summary>
	/// Size of the current primitive type in bytes.
	/// </summary>
	public override Int32 SizeOf { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Primitive;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Information of wrapper class.
	/// </summary>
	internal TypeInfoSequence WrapperInformation { get; }

	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="sizeOf">Size of the current primitive type in bytes.</param>
	/// <param name="underlineType">Underline primitive CLR type.</param>
	/// <param name="information">Internal information.</param>
	/// <param name="wrapperInformation">Wrapper class information.</param>
	private protected JPrimitiveTypeMetadata(Int32 sizeOf, Type underlineType, TypeInfoSequence information,
		TypeInfoSequence wrapperInformation) : base(information)
	{
		this.SizeOf = sizeOf;
		this.UnderlineType = underlineType;
		this.WrapperInformation = wrapperInformation;
	}

	/// <summary>
	/// Creates a <see cref="IPrimitiveType"/> value from <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Binary read-only span.</param>
	/// <returns>A <see cref="IPrimitiveType"/> value from <paramref name="bytes"/>.</returns>
	public abstract IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes);

	/// <inheritdoc/>
	public override String? ToString()
#if !PACKAGE
		=> MetadataTextUtilities.TypeMetadataToStringEnabled ?
#else
		=> IVirtualMachine.TypeMetadataToStringEnabled ?
#endif
			MetadataTextUtilities.GetString(this) :
			base.ToString();
}

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class JPrimitiveTypeMetadata<TPrimitive> : JPrimitiveTypeMetadata
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="underlineType">Underline primitive CLR type.</param>
	/// <param name="information">Internal information.</param>
	/// <param name="wrapperInformation">Wrapper class information.</param>
	private protected unsafe JPrimitiveTypeMetadata(Type underlineType, TypeInfoSequence information,
		TypeInfoSequence wrapperInformation) :
		base(sizeof(TPrimitive), underlineType, information, wrapperInformation) { }
}