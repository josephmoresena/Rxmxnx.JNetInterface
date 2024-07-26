namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents an error that occur during JNI calls into a critical block.
/// </summary>
public sealed class CriticalException : JniException
{
	/// <summary>
	/// Singleton instance.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static readonly CriticalException Instance = new(CommonConstants.CriticalExceptionMessage);
	/// <summary>
	/// Singleton unknown instance.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static readonly CriticalException UnknownInstance = new(CommonConstants.UnknownExceptionMessage);

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	private CriticalException(String message) : base(message: message) { }
}