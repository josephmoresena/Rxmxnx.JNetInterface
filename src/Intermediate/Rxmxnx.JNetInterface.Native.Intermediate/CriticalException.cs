namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents error that occur during JNI calls into critical a block.
/// </summary>
public sealed class CriticalException : JniException
{
	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	internal CriticalException() { }
}