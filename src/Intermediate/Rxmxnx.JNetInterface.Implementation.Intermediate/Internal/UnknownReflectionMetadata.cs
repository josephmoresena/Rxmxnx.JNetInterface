namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stores a reflection metadata for an unknown <see cref="IReferenceType"/> type.
/// </summary>
internal sealed class UnknownReflectionMetadata : IReflectionMetadata
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">The signature of type.</param>
	public UnknownReflectionMetadata(ReadOnlySpan<Byte> signature)
		=> this.ArgumentMetadata = JArgumentMetadata.Create(signature);
	/// <inheritdoc/>
	public JArgumentMetadata ArgumentMetadata { get; }

	/// <inheritdoc/>
	public JFunctionDefinition CreateFunctionDefinition(ReadOnlySpan<Byte> functionName, JArgumentMetadata[] metadata)
		=> new JNonTypedFunctionDefinition(functionName, this.ArgumentMetadata.Signature, metadata);
	/// <inheritdoc/>
	public JFieldDefinition CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
		=> new JNonTypedFieldDefinition(fieldName, this.ArgumentMetadata.Signature);
}