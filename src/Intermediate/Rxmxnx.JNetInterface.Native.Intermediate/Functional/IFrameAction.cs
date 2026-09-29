namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents an action to be performed within a specific JNI environment context.
/// </summary>
public interface IFrameAction
{
	/// <summary>
	/// Executes the provided action within the provided environment instance.
	/// </summary>
	/// <param name="env">The environment context in which the action is executed.</param>
	/// <param name="objects">The objects to be used within the action.</param>
	void Accept(IEnvironment env, ReadOnlySpan<JLocalObject?> objects);
}