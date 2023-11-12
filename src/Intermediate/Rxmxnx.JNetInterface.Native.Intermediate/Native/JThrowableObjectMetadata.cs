namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JThrowableObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public record JThrowableObjectMetadata : JObjectMetadata
{
	/// <summary>
	/// Internal throwable message.
	/// </summary>
	public String? Message { get; init; }
	
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	internal JThrowableObjectMetadata(JObjectMetadata metadata) : base(metadata)
	{
		if (metadata is not JThrowableObjectMetadata enumMetadata)
			return;
		this.Message = enumMetadata.Message;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="JThrowableObjectMetadata"/> instance.</param>
	protected JThrowableObjectMetadata(JThrowableObjectMetadata metadata) : base(metadata) => this.Message = metadata.Message;
}