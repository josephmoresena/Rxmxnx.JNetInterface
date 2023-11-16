namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract record JClassTypeMetadata : JReferenceTypeMetadata
{
	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Class;

	/// <inheritdoc/>
	internal JClassTypeMetadata(CString className, CString? signature, CString? arraySignature) : base(
		className, signature, arraySignature) { }
}