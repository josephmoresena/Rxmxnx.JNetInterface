namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents an action to be performed within a specific JNI environment context.
/// </summary>
public interface ILocalFrameAction : IFrameAction
{
	/// <summary>
	/// Local frame required capacity.
	/// </summary>
	Int32 RequiredCapacity { get; }
}