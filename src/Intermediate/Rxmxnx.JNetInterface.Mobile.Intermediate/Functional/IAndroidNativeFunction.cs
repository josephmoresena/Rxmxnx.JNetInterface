namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents a function interface that applies a specific logic in the context of an Android JNI environment.
/// </summary>
/// <typeparam name="TOutput">The type of the output produced by the function.</typeparam>
public interface IAndroidNativeFunction<out TOutput>
{
	/// <summary>
	/// Applies the specified function logic using the provided context instance.
	/// </summary>
	/// <param name="context">
	/// An instance of <see cref="AndroidJniContext"/> that provides the JNI context for the function execution.
	/// </param>
	/// <returns>The result of the function execution.</returns>
	TOutput Apply(AndroidJniContext context);
}