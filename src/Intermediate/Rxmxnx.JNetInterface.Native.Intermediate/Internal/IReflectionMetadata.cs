namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a <c>java.lang.Class&lt;?&gt;</c> JNI reflection container.
/// </summary>
internal interface IReflectionMetadata
{
	/// <inheritdoc cref="JDataTypeMetadata.ArgumentMetadata"/>
	JArgumentMetadata ArgumentMetadata { get; }
	/// <summary>
	/// Creates a <see cref="JFunctionDefinition"/> instance from <paramref name="functionName"/> and
	/// <paramref name="metadata"/>.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A new <see cref="JFunctionDefinition"/> instance.</returns>
	JFunctionDefinition CreateFunctionDefinition(ReadOnlySpan<Byte> functionName, JArgumentMetadata[] metadata);
	/// <summary>
	/// Creates a <see cref="JFunctionDefinition"/> instance from <paramref name="fieldName"/>.
	/// </summary>
	/// <param name="fieldName">Field name.</param>
	/// <returns>A new <see cref="JFieldDefinition"/> instance.</returns>
	JFieldDefinition CreateFieldDefinition(ReadOnlySpan<Byte> fieldName);
}