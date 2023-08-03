namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract record JClassMetadata : JReferenceMetadata
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JClassMetadata(CString className, CString? signature, CString? arraySignature) : base(
		className, signature, arraySignature) { }
}