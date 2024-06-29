namespace Rxmxnx.JNetInterface.Tests;

public static partial class InvokeInterfaceHelper
{
	public static JVirtualMachineRef InitializeProxy(InvokeInterfaceProxy proxy)
	{
		IntPtr ptr = InvokeInterfaceHelper.helper.Get();
		JVirtualMachineRef result = NativeUtilities.Transform<IntPtr, JVirtualMachineRef>(ptr);
		InvokeInterfaceHelper.proxies[result] = proxy;
		return result;
	}
	public static void FinalizeProxy(JVirtualMachineRef vmRef) => InvokeInterfaceHelper.proxies.TryRemove(vmRef, out _);
}