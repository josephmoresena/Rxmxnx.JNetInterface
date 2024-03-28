namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.AbstractProxyJustification)]
public abstract partial class VirtualMachineProxy : IVirtualMachine
{
	public abstract JVirtualMachineRef Reference { get; }
	public abstract void FatalError(CString? message);
	public abstract void FatalError(String? message);

	public abstract EnvironmentProxy? GetEnvironment();
	public abstract ThreadProxy InitializeThread(CString? threadName = default, JGlobalBase? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
	public abstract ThreadProxy InitializeDaemon(CString? threadName = default, JGlobalBase? threadGroup = default,
		Int32 version = IVirtualMachine.MinimalVersion);
}