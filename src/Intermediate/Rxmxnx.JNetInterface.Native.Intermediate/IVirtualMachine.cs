namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes the invocation interface of a Java Virtual Machine.
/// </summary>
public interface IVirtualMachine
{
	/// <summary>
	/// Minimum virtual machine version required for any JNI thread.
	/// </summary>
	public const Int32 MinimalVersion = 0x00010006;

	/// <summary>
	/// JNI reference to the interface.
	/// </summary>
	JVirtualMachineRef Reference { get; }

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance for current thread.
	/// </summary>
	/// <returns>The <see cref="IEnvironment"/> instance for current thread.</returns>
	IEnvironment? GetEnvironment();
	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance that <paramref name="envRef"/>
	/// references to.
	/// </summary>
	/// <param name="envRef"><see cref="JEnvironmentRef"/> reference to JNI interface.</param>
	/// <returns>
	/// The <see cref="IEnvironment"/> instance referenced by <paramref name="envRef"/>.
	/// </returns>
	IEnvironment GetEnvironment(JEnvironmentRef envRef);
	/// <summary>
	/// Attaches the current thread to the virtual machine for <paramref name="purpose"/>.
	/// </summary>
	/// <param name="purpose">The purpose of requested thread.</param>
	/// <returns>A <see cref="IThread"/> instance for given purpose.</returns>
	IThread CreateThread(ThreadPurpose purpose);
	/// <summary>
	/// Attaches the current thread to the virtual machine.
	/// </summary>
	/// <param name="threadName">A name for current thread.</param>
	/// <param name="threadGroup">A <see cref="JObject"/> instance of <c>java.lang.ThreadGroup</c>.</param>
	/// <param name="version">Thread requested version.</param>
	/// <returns>A <see cref="IThread"/> instance.</returns>
	IThread InitializeThread(CString threadName, JObject? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
}