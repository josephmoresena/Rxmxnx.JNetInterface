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
	/// Internal throwable stack trace.
	/// </summary>
	public JStackTraceInfo[]? StackTrace { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	internal JThrowableObjectMetadata(JObjectMetadata metadata) : base(metadata)
	{
		if (metadata is not JThrowableObjectMetadata throwableMetadata)
			return;
		this.Message = throwableMetadata.Message;
		this.StackTrace = throwableMetadata.StackTrace;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="JThrowableObjectMetadata"/> instance.</param>
	protected JThrowableObjectMetadata(JThrowableObjectMetadata metadata) : base(metadata)
	{
		this.Message = metadata.Message;
		this.StackTrace = metadata.StackTrace;
	}
}