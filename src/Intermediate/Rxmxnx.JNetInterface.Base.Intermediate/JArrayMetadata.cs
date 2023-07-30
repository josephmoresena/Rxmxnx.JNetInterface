namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a array <see cref="IDataType"/> type.
/// </summary>
public sealed record JArrayMetadata : JDataTypeMetadata
{
	/// <inheritdoc cref="JArrayMetadata.ElementMetadata"/>
	private readonly JDataTypeMetadata _elementMetadata;

	/// <summary>
	/// Element type of current array metadata.
	/// </summary>
	public JDataTypeMetadata ElementMetadata => this._elementMetadata;

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Array;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;
	/// <inheritdoc/>
	public override Int32 SizeOf => NativeUtilities.PointerSize;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type of current type.</param>
	/// <param name="elementMetadata">Element type of current type metadata.</param>
	/// <param name="signature">JNI signature for current array type.</param>
	internal JArrayMetadata(Type type, JDataTypeMetadata elementMetadata, CString signature) :
		base(type, signature, signature)
		=> this._elementMetadata = elementMetadata;
}