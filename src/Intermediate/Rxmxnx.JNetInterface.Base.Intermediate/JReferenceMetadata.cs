namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public abstract record JReferenceMetadata : JDataTypeMetadata
{
	/// <summary>
	/// Base type of current type metadata.
	/// </summary>
	public abstract JClassMetadata? BaseMetadata { get; }
	/// <summary>
	/// Set of interfaces metadata of current type implements.
	/// </summary>
	public abstract IImmutableSet<JInterfaceMetadata> Interfaces { get; }
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