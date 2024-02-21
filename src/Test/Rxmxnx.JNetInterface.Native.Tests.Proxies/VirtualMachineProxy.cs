namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.AbstractProxyJustification)]
public abstract class VirtualMachineProxy : IVirtualMachine
{
	public abstract JVirtualMachineRef Reference { get; }

	public abstract IEnvironment? GetEnvironment();
	public abstract IThread InitializeThread(CString? threadName = default, JGlobalBase? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
	public abstract IThread InitializeDaemon(CString? threadName = default, JGlobalBase? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
}