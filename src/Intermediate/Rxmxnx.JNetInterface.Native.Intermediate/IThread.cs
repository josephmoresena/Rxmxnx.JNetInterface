namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a JNI thread.
/// </summary>
public interface IThread : IEnvironment, IDisposable
{
	/// <summary>
	/// Indicates current thread was attached to the VM with current instance.
	/// </summary>
	Boolean Attached { get; }
	/// <summary>
	/// Indicates current thread is a daemon.
	/// </summary>
	Boolean Daemon { get; }
	/// <summary>
	/// Thread name.
	/// </summary>
	CString Name { get; }
}