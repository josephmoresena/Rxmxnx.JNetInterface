namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="IPrimitiveWrapperType"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
internal sealed record JPrimitiveWrapperObjectMetadata<TPrimitive> : ObjectMetadata
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Internal value.
	/// </summary>
	public TPrimitive? Value { get; init; }

	internal JPrimitiveWrapperObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is JPrimitiveWrapperObjectMetadata<TPrimitive> wrapperMetadata)
			this.Value = wrapperMetadata.Value;
	}
}