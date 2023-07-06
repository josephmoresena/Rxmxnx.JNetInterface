namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a JNI thread.
/// </summary>
public interface IThread : IEnvironment, IDisposable
{
	/// <summary>
	/// Retrieves the JNI interface for current thread.
	/// </summary>
	/// <returns>The <see cref="IEnvironment"/> instanced for current JNI thread.</returns>
	IEnvironment? GetEnvironment();
}