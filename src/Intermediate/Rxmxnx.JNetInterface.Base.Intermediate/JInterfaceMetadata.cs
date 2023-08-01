namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for an interface <see cref="IDataType"/> type.
/// </summary>
public abstract record JInterfaceMetadata : JReferenceMetadata
{
	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Interface;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Abstract;
	/// <inheritdoc/>
	public override JClassMetadata? BaseMetadata => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="interfaceName">Interface name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JInterfaceMetadata(CString interfaceName, CString? signature, CString? arraySignature = default) : base(
		interfaceName, signature, arraySignature) { }
}