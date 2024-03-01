namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JThrowableObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public record ThrowableObjectMetadata : ObjectMetadata
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
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="metadata"><see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="message">Throwable message.</param>
	internal ThrowableObjectMetadata(JClassObject jClass, JReferenceTypeMetadata metadata, String? message) :
		base(jClass, metadata)
		=> this.Message = message;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	internal ThrowableObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is not ThrowableObjectMetadata throwableMetadata)
			return;
		this.Message = throwableMetadata.Message;
		this.StackTrace = throwableMetadata.StackTrace;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ThrowableObjectMetadata"/> instance.</param>
	protected ThrowableObjectMetadata(ThrowableObjectMetadata metadata) : base(metadata)
	{
		this.Message = metadata.Message;
		this.StackTrace = metadata.StackTrace;
	}
}