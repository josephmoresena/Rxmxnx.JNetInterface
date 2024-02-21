namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public abstract class InvokedVirtualMachineProxy : VirtualMachineProxy, IInvokedVirtualMachine
{
	public abstract void Dispose();
}