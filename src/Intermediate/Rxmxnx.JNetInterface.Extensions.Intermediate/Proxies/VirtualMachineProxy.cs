namespace Rxmxnx.JNetInterface.Native.Proxies;

/// <summary>
/// This interface exposes a proxy for invocation interface.
/// </summary>
public abstract class VirtualMachineProxy : IVirtualMachine
{
	/// <inheritdoc/>
	public abstract JVirtualMachineRef Reference { get; }

	IEnvironment? IVirtualMachine.GetEnvironment() => this.GetEnvironment();
	IThread IVirtualMachine.InitializeThread(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this.InitializeThread(threadName, threadGroup, version);
	IThread IVirtualMachine.InitializeDaemon(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this.InitializeDaemon(threadName, threadGroup, version);
	/// <inheritdoc/>
	public abstract void FatalError(CString? message);
	/// <inheritdoc/>
	public abstract void FatalError(String? message);

	/// <inheritdoc cref="IVirtualMachine.GetEnvironment()"/>
	public abstract EnvironmentProxy? GetEnvironment();
	/// <inheritdoc cref="IVirtualMachine.InitializeThread(CString, JGlobalBase, Int32)"/>
	public abstract ThreadProxy InitializeThread(CString? threadName = default, JGlobalBase? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
	/// <inheritdoc cref="IVirtualMachine.InitializeDaemon(CString, JGlobalBase, Int32)"/>
	public abstract ThreadProxy InitializeDaemon(CString? threadName = default, JGlobalBase? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
}