namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract record JReferenceMetadata : JDataTypeMetadata
{
	/// <inheritdoc/>
	public override Int32 SizeOf => NativeUtilities.PointerSize;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JReferenceMetadata(CString className, CString? signature, CString? arraySignature = default) : base(
		className, signature ?? JDataTypeMetadata.ComputeReferenceTypeSignature(className), arraySignature) { }
}