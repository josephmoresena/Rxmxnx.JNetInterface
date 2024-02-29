namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JClassObject"/> in order to create a
/// <see cref="JBufferObject"/> instance.
/// </summary>
public record BufferObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// Indicates whether current buffer is direct.
	/// </summary>
	public Boolean IsDirect { get; init; }
	/// <summary>
	/// Buffer capacity.
	/// </summary>
	public Int64 Capacity { get; init; }
	/// <summary>
	/// Direct buffer address.
	/// </summary>
	public IntPtr? Address { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	internal BufferObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is not BufferObjectMetadata bufferMetadata)
			return;
		this.IsDirect = bufferMetadata.IsDirect;
		this.Capacity = bufferMetadata.Capacity;
		this.Address = bufferMetadata.Address;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="BufferObjectMetadata"/> instance.</param>
	protected BufferObjectMetadata(BufferObjectMetadata metadata) : base(metadata)
	{
		this.IsDirect = metadata.IsDirect;
		this.Capacity = metadata.Capacity;
		this.Address = metadata.Address;
	}
}