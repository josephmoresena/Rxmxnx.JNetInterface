namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an interface <see cref="IDataType"/> type.
/// </summary>
public abstract record JInterfaceTypeMetadata : JReferenceTypeMetadata
{
	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Interface;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Abstract;
	/// <inheritdoc/>
	public override JClassTypeMetadata? BaseMetadata => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="interfaceName">Interface name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JInterfaceTypeMetadata(CString interfaceName, CString? signature, CString? arraySignature = default) :
		base(interfaceName, signature, arraySignature) { }

	/// <summary>
	/// Retrieves the CLR type of implementation of <typeparamref name="TReference"/> of current interface.
	/// </summary>
	/// <typeparam name="TReference">Type of <see cref="IReferenceType{TReference}"/>.</typeparam>
	/// <returns>The CLR type of implementation of <typeparamref name="TReference"/> of current interface.</returns>
	[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
	internal abstract Type GetImplementingType<TReference>()
		where TReference : JReferenceObject, IReferenceType<TReference>;
}