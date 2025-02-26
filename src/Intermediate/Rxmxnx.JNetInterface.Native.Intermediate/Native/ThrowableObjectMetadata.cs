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
	public StackTraceInfo[]? StackTrace { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="typeInformation"><see cref="ITypeInformation"/> instance.</param>
	/// <param name="message">Throwable message.</param>
	/// <param name="fromProxy">Indicates whether the current instance is a dummy object (fake java object).</param>
	[ExcludeFromCodeCoverage]
	internal ThrowableObjectMetadata(ITypeInformation typeInformation, String? message, Boolean fromProxy) :
		base(typeInformation, fromProxy)
		=> this.Message = message;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="message">Throwable message.</param>
	internal ThrowableObjectMetadata(JClassObject jClass, String? message) : base(jClass) => this.Message = message;
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
	[ExcludeFromCodeCoverage]
	protected ThrowableObjectMetadata(ThrowableObjectMetadata metadata) : base(metadata)
	{
		this.Message = metadata.Message;
		this.StackTrace = metadata.StackTrace;
	}
}