namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an enum <see cref="IDataType"/> type.
/// </summary>
public abstract record JEnumTypeMetadata : JReferenceTypeMetadata
{
	/// <summary>
	/// Name of fields representing each enum value.
	/// </summary>
	public abstract CString[]? Fields { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Enum;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JEnumTypeMetadata(CString className, CString? signature, CString? arraySignature = default) : base(
		className, signature, arraySignature) { }
}