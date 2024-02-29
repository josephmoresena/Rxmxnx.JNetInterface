namespace Rxmxnx.JNetInterface.Types.Metadata;

public abstract partial record JReferenceTypeMetadata
{
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="dispose">
	/// Indicates whether current instance should be disposed after casting.
	/// </param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jLocal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jLocal))]
	internal abstract JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false);

	/// <inheritdoc cref="IReflectionMetadata.CreateFunctionDefinition(ReadOnlySpan{Byte}, JArgumentMetadata[])"/>
	internal abstract JFunctionDefinition CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] metadata);
	/// <inheritdoc cref="IReflectionMetadata.CreateFieldDefinition(ReadOnlySpan{Byte})"/>
	internal abstract JFieldDefinition CreateFieldDefinition(ReadOnlySpan<Byte> fieldName);

	/// <summary>
	/// Creates a <see cref="JArrayTypeMetadata"/> from current instance.
	/// </summary>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	internal abstract JArrayTypeMetadata? GetArrayMetadata();
}