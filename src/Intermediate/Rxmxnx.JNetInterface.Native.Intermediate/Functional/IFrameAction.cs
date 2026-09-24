namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents an action to be performed within a specific JNI environment context.
/// </summary>
public interface IFrameAction
{
	/// <summary>
	/// Local frame required capacity.
	/// </summary>
	Int32 RequiredCapacity { get; }

	/// <summary>
	/// Executes the provided action within the given environment context.
	/// </summary>
	/// <param name="env">The environment context in which the action is executed.</param>
	/// <param name="objects">The objects to be used within the action.</param>
	void Accept(IEnvironment env, ReadOnlySpan<JLocalObject?> objects);
}