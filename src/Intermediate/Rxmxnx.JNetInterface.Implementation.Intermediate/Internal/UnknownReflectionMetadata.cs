namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class stores a reflection metadata for an unknown <see cref="IReferenceType"/> type.
/// </summary>
internal readonly struct UnknownReflectionMetadata
{
	/// <inheritdoc cref="JDataTypeMetadata.ArgumentMetadata"/>
	public JArgumentMetadata ArgumentMetadata { get; }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">The signature of type.</param>
	public UnknownReflectionMetadata(ReadOnlySpan<Byte> signature)
		=> this.ArgumentMetadata = JArgumentMetadata.Create(signature);

	/// <inheritdoc cref="JReferenceTypeMetadata.CreateFunctionDefinition(ReadOnlySpan{Byte},JArgumentMetadata[])"/>
	public JFunctionDefinition CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] paramsMetadata)
		=> new JNonTypedFunctionDefinition(functionName, this.ArgumentMetadata.Signature, paramsMetadata);
	/// <inheritdoc cref="JReferenceTypeMetadata.CreateFieldDefinition(ReadOnlySpan{Byte})"/>
	public JFieldDefinition CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
		=> new JNonTypedFieldDefinition(fieldName, this.ArgumentMetadata.Signature);
}