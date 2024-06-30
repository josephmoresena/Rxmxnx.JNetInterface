namespace Rxmxnx.JNetInterface.Tests;

public unsafe partial class InvokeInterfaceHelper
{
	[UnmanagedCallersOnly]
	private static JResult DestroyVirtualMachine(JVirtualMachineRef vmRef)
		=> InvokeInterfaceHelper.proxies[vmRef].DestroyVirtualMachine();
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThread(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr,
		VirtualMachineArgumentValueWrapper* argPtr)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		ref VirtualMachineArgumentValueWrapper argRef = ref Unsafe.AsRef<VirtualMachineArgumentValueWrapper>(argPtr);
		return InvokeInterfaceHelper.proxies[vmRef]
		                            .AttachCurrentThread(envRef.GetUnsafeValPtr(), argRef.GetUnsafeValPtr());
	}
	[UnmanagedCallersOnly]
	private static JResult DetachCurrentThread(JVirtualMachineRef vmRef)
		=> InvokeInterfaceHelper.proxies[vmRef].DetachCurrentThread();
	[UnmanagedCallersOnly]
	private static JResult GetEnv(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr, Int32 version)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		return InvokeInterfaceHelper.proxies[vmRef].GetEnv(envRef.GetUnsafeValPtr(), version);
	}
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThreadAsDaemon(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr,
		VirtualMachineArgumentValueWrapper* argPtr)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		ref VirtualMachineArgumentValueWrapper argRef = ref Unsafe.AsRef<VirtualMachineArgumentValueWrapper>(argPtr);
		return InvokeInterfaceHelper.proxies[vmRef]
		                            .AttachCurrentThreadAsDaemon(envRef.GetUnsafeValPtr(), argRef.GetUnsafeValPtr());
	}
}