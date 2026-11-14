namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents an action to be performed within a specific Android JNI environment context.
/// </summary>
public interface IAndroidNativeAction
{
	/// <summary>
	/// Executes the provided action within the provided context instance.
	/// </summary>
	/// <param name="context">
	/// An instance of <see cref="AndroidJniContext"/> that provides the JNI context for the action execution.
	/// </param>
	void Accept(AndroidJniContext context);
}