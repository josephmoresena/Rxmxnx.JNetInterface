namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an array <see cref="IDataType"/> type.
/// </summary>
public abstract record JArrayMetadata : JReferenceMetadata
{
	/// <summary>
	/// Element type of current array metadata.
	/// </summary>
	public abstract JDataTypeMetadata ElementMetadata { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Array;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">JNI signature for current array type.</param>
	internal JArrayMetadata(CString signature) : base(signature, signature) { }
}