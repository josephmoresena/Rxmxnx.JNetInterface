namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents a function interface that applies a specific logic in the context of a provided JNI environment.
/// </summary>
/// <typeparam name="TOutput">The type of the output produced by the function.</typeparam>
public interface ILocalFrameFunction<out TOutput> : IFrameFunction<TOutput>
{
	/// <summary>
	/// Local frame required capacity.
	/// </summary>
	Int32 RequiredCapacity { get; }
}