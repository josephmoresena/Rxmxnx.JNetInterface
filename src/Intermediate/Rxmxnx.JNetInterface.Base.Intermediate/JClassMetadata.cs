namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract record JClassMetadata : JReferenceMetadata
{
	/// <inheritdoc cref="JReferenceMetadata.BaseMetadata"/>
	private readonly JClassMetadata? _baseMetadata;
	/// <inheritdoc cref="JReferenceMetadata.Interfaces"/>
	private readonly IImmutableSet<JInterfaceMetadata> _interfaces;
	/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
	private readonly JTypeModifier _modifier;
	/// <inheritdoc cref="JDataTypeMetadata.Type"/>
	private readonly Type _type;

	/// <inheritdoc/>
	public override Type Type => this._type;
	/// <inheritdoc/>
	public override JClassMetadata? BaseMetadata => this._baseMetadata;
	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Class;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => this._modifier;
	/// <inheritdoc/>
	public override IImmutableSet<JInterfaceMetadata> Interfaces => this._interfaces;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type.</param>
	/// <param name="className">Class name of current type.</param>
	/// <param name="modifier">Modifier of current type.</param>
	/// <param name="interfaces">Set of interfaces metadata of current type implements.</param>
	/// <param name="baseMetadata">Base type of current type metadata.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JClassMetadata(Type type, CString className, JTypeModifier modifier,
		IImmutableSet<JInterfaceMetadata> interfaces, JClassMetadata? baseMetadata, CString? signature,
		CString? arraySignature) : base(className, signature, arraySignature)
	{
		this._type = type;
		this._modifier = modifier;
		this._interfaces = interfaces;
		this._baseMetadata = baseMetadata;
	}
}