namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents a function interface that applies a specific logic in the context of a provided JNI environment.
/// </summary>
/// <typeparam name="TOutput">The type of the output produced by the function.</typeparam>
public interface IFrameFunction<out TOutput>
{
	/// <summary>
	/// Local frame required capacity.
	/// </summary>
	Int32 RequiredCapacity { get; }

	/// <summary>
	/// Applies the specified function logic using the provided environment instance.
	/// </summary>
	/// <param name="env">The environment instance used to execute the function.</param>
	/// <returns>The result of the function execution.</returns>
	TOutput Apply(IEnvironment env);
}