namespace Rxmxnx.JNetInterface.Tests;

public abstract partial class VirtualMachineProxy
{
	IEnvironment? IVirtualMachine.GetEnvironment() => this.GetEnvironment();
	IThread IVirtualMachine.InitializeThread(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this.InitializeThread(threadName, threadGroup, version);
	IThread IVirtualMachine.InitializeDaemon(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this.InitializeDaemon(threadName, threadGroup, version);
}