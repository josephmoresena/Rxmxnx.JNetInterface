namespace Rxmxnx.JNetInterface.Tests;

public partial class InvokeInterfaceHelper
{
	[UnmanagedCallersOnly]
	private static JResult DestroyVirtualMachine(JVirtualMachineRef vmRef)
		=> InvokeInterfaceHelper.proxies[vmRef].DestroyVirtualMachine();
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThread(JVirtualMachineRef vmRef, ValPtr<JEnvironmentRef> envRef,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper> arg)
		=> InvokeInterfaceHelper.proxies[vmRef].AttachCurrentThread(envRef, arg);
	[UnmanagedCallersOnly]
	private static JResult DetachCurrentThread(JVirtualMachineRef vmRef)
		=> InvokeInterfaceHelper.proxies[vmRef].DetachCurrentThread();
	[UnmanagedCallersOnly]
	private static JResult GetEnv(JVirtualMachineRef vmRef, ValPtr<JEnvironmentRef> envRef, Int32 version)
		=> InvokeInterfaceHelper.proxies[vmRef].GetEnv(envRef, version);
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThreadAsDaemon(JVirtualMachineRef vmRef, ValPtr<JEnvironmentRef> envRef,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper> arg)
		=> InvokeInterfaceHelper.proxies[vmRef].AttachCurrentThreadAsDaemon(envRef, arg);
}