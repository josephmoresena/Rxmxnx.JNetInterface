namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stores the metadata of a <see cref="IPrimitiveWrapperType"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
internal sealed record PrimitiveWrapperObjectMetadata<TPrimitive> : ObjectMetadata
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Internal value.
	/// </summary>
	public TPrimitive? Value { get; init; }

	internal PrimitiveWrapperObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is PrimitiveWrapperObjectMetadata<TPrimitive> wrapperMetadata)
			this.Value = wrapperMetadata.Value;
	}
}