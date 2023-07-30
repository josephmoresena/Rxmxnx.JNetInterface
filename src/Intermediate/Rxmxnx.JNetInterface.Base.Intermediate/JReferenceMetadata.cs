using System.Collections.Immutable;

namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public sealed record JReferenceMetadata : JDataTypeMetadata
{
	/// <inheritdoc cref="JReferenceMetadata.BaseMetadata"/>
	private readonly JReferenceMetadata? _baseMetadata;
	/// <inheritdoc cref="JReferenceMetadata.Interfaces"/>
	private readonly IImmutableSet<JDataTypeMetadata> _interfaces;
	/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
	private readonly JTypeModifier _modifier;

	/// <summary>
	/// Base type of current type metadata.
	/// </summary>
	public JReferenceMetadata? BaseMetadata => this._baseMetadata;
	/// <summary>
	/// Set of interfaces metadata of current type implements.
	/// </summary>
	public IImmutableSet<JDataTypeMetadata> Interfaces => this._interfaces;

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Class;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => this._modifier;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type of current type.</param>
	/// <param name="className">Class name of current type.</param>
	/// <param name="modifier">Modifier of current type.</param>
	/// <param name="interfaces">Set of interfaces metadata of current type implements.</param>
	/// <param name="baseMetadata">Base type of current type metadata.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JReferenceMetadata(Type type, CString className, JTypeModifier modifier,
		IImmutableSet<JDataTypeMetadata> interfaces, JReferenceMetadata? baseMetadata, CString? signature,
		CString? arraySignature) : base(type, className,
		                                signature ?? JDataTypeMetadata.ComputeReferenceTypeSignature(className),
		                                arraySignature)
	{
		this._modifier = modifier;
		this._interfaces = interfaces;
		this._baseMetadata = baseMetadata;
	}
}