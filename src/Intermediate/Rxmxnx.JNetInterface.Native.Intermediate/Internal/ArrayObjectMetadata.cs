namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stores the metadata of a <see cref="JArrayObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public sealed record ArrayObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// Internal array length.
	/// </summary>
	public Int32 Length { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	public ArrayObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is ArrayObjectMetadata arrayMetadata)
			this.Length = arrayMetadata.Length;
	}
}