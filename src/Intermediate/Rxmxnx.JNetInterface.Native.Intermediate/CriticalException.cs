namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents error that occur during JNI calls into critical a block.
/// </summary>
public sealed class CriticalException : JniException
{
	/// <summary>
	/// Singleton instance.
	/// </summary>
	internal static readonly CriticalException Instance = new();

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	private CriticalException() : base(message: CommonConstants.CriticalExceptionMessage) { }
}