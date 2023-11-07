namespace Rxmxnx.JNetInterface.Internal.Native;

/// <summary>
/// This record stores the metadata of a <see cref="IPrimitiveWrapperType"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
internal sealed record JPrimitiveWrapperObjectMetadata<TPrimitive> : JObjectMetadata
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Internal value.
	/// </summary>
	public TPrimitive? Value { get; init; }

	internal JPrimitiveWrapperObjectMetadata(JObjectMetadata metadata) : base(metadata)
	{
		if (metadata is JPrimitiveWrapperObjectMetadata<TPrimitive> wrapperMetadata)
			this.Value = wrapperMetadata.Value;
	}
}