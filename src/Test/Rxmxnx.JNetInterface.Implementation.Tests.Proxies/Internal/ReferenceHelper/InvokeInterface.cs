namespace Rxmxnx.JNetInterface.Tests.Internal;

internal static unsafe partial class ReferenceHelper
{
	public static readonly InternalInvokeInterface[] InvokeInterface =
	[
		new()
		{
			DestroyVirtualMachine = &ReferenceHelper.DestroyVirtualMachine,
			AttachCurrentThread = &ReferenceHelper.AttachCurrentThread,
			DetachCurrentThread = &ReferenceHelper.DetachCurrentThread,
			AttachCurrentThreadAsDaemon = &ReferenceHelper.AttachCurrentThreadAsDaemon,
			GetEnv = &ReferenceHelper.GetEnv,
		},
	];

	[UnmanagedCallersOnly]
	private static JResult DestroyVirtualMachine(JVirtualMachineRef vmRef)
		=> ReferenceHelper.InvokeProxies[vmRef].DestroyVirtualMachine();
	[UnmanagedCallersOnly]
	private static JResult GetEnv(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr, Int32 version)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		return ReferenceHelper.InvokeProxies[vmRef].GetEnv(envRef.GetUnsafeValPtr(), version);
	}
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThread(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr,
		VirtualMachineArgumentValueWrapper* argPtr)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		ref VirtualMachineArgumentValueWrapper argRef = ref Unsafe.AsRef<VirtualMachineArgumentValueWrapper>(argPtr);
		return ReferenceHelper.InvokeProxies[vmRef]
		                      .AttachCurrentThread(envRef.GetUnsafeValPtr(), argRef.GetUnsafeValPtr());
	}
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThreadAsDaemon(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr,
		VirtualMachineArgumentValueWrapper* argPtr)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		ref VirtualMachineArgumentValueWrapper argRef = ref Unsafe.AsRef<VirtualMachineArgumentValueWrapper>(argPtr);
		return ReferenceHelper.InvokeProxies[vmRef]
		                      .AttachCurrentThreadAsDaemon(envRef.GetUnsafeValPtr(), argRef.GetUnsafeValPtr());
	}
	[UnmanagedCallersOnly]
	private static JResult DetachCurrentThread(JVirtualMachineRef vmRef)
		=> ReferenceHelper.InvokeProxies[vmRef].DetachCurrentThread();
}