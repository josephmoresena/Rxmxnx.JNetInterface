namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JFieldObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public sealed record FieldObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// Field definition.
	/// </summary>
	public JFieldDefinition? Definition { get; internal init; }
	/// <summary>
	/// Declaring class information.
	/// </summary>
	public ITypeInformation? ClassInformation { get; internal init; }
	/// <summary>
	/// JNI field id.
	/// </summary>
	public JFieldId? FieldId { get; internal init; }

	/// <inheritdoc/>
	public FieldObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is not FieldObjectMetadata fieldMetadata) return;
		this.Definition = fieldMetadata.Definition;
		this.ClassInformation = fieldMetadata.ClassInformation;
		this.FieldId = fieldMetadata.FieldId;
	}

	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private FieldObjectMetadata(FieldObjectMetadata fieldMetadata) : base(fieldMetadata)
	{
		this.Definition = fieldMetadata.Definition;
		this.ClassInformation = fieldMetadata.ClassInformation;
		this.FieldId = fieldMetadata.FieldId;
	}
}