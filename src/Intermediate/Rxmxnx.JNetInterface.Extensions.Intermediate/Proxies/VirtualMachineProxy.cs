namespace Rxmxnx.JNetInterface.Proxies;

/// <summary>
/// This interface exposes a proxy for invocation interface.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
public abstract class VirtualMachineProxy : IVirtualMachine
{
	/// <inheritdoc/>
	public abstract JVirtualMachineRef Reference { get; }

	Boolean IVirtualMachine.NoProxy => false;
	IEnvironment? IVirtualMachine.GetEnvironment() => this.GetEnvironment();
	IThread IVirtualMachine.CreateThread(ThreadPurpose purpose) => this.CreateThread(purpose);
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
	/// <inheritdoc cref="IVirtualMachine.CreateThread(ThreadPurpose)"/>
	public abstract ThreadProxy CreateThread(ThreadPurpose purpose);
}