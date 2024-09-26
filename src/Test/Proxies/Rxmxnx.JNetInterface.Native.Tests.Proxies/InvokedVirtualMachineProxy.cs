namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.AbstractProxyJustification)]
public abstract class InvokedVirtualMachineProxy : VirtualMachineProxy, IInvokedVirtualMachine
{
	public abstract void Dispose();
}