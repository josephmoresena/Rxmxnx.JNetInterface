namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
public abstract record JPrimitiveWrapperTypeMetadata : JClassTypeMetadata
{
	/// <summary>
	/// Primitive metadata.
	/// </summary>
	public abstract JPrimitiveTypeMetadata PrimitiveMetadata { get; }

	/// <inheritdoc/>
	internal JPrimitiveWrapperTypeMetadata(CString className, CString? signature, CString? arraySignature) : base(
		className, signature, arraySignature) { }
}