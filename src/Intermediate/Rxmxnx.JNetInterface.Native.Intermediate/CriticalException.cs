namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents an error that occur during JNI calls into a critical block.
/// </summary>
public sealed class CriticalException : JniException
{
	/// <summary>
	/// Singleton instance.
	/// </summary>
	private static CriticalException? instance;
	/// <summary>
	/// Singleton unknown instance.
	/// </summary>
	private static CriticalException? unknownInstance;

	/// <summary>
	/// Singleton instance.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static CriticalException Instance
		=> CriticalException.Update(IMessageResource.GetInstance().CriticalExceptionMessage,
		                            ref CriticalException.instance);
	/// <summary>
	/// Singleton unknown instance.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static CriticalException UnknownInstance
		=> CriticalException.Update(IMessageResource.GetInstance().UnknownExceptionMessage,
		                            ref CriticalException.unknownInstance);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	private CriticalException(String message) : base(message: message) { }

	/// <summary>
	/// Refresh current instance.
	/// </summary>
	/// <param name="message">Current UI message.</param>
	/// <param name="exception">A <see cref="CriticalException"/> reference.</param>
	/// <returns>A <see cref="CriticalException"/> instance updated.</returns>
	private static CriticalException Update(String message, ref CriticalException? exception)
	{
		if (exception is null || !message.AsSpan().SequenceEqual(exception.Message.AsSpan()))
			exception = new(message);
		return exception;
	}
}