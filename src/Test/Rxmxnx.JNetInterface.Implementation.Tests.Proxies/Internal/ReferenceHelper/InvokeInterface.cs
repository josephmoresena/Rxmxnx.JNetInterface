namespace Rxmxnx.JNetInterface.Tests.Internal;

internal static unsafe partial class ReferenceHelper
{
	private static readonly ConcurrentDictionary<JVirtualMachineRef, InvokeInterfaceProxy> invokeProxies = new();
	private static readonly InternalInvokeInterface[] invokeInterface =
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
	private static readonly GCHandle
		invokeHandle = GCHandle.Alloc(ReferenceHelper.invokeInterface, GCHandleType.Pinned);
	private static readonly MemoryHelper invokeHelper =
		new(ReferenceHelper.invokeHandle.AddrOfPinnedObject(), Int16.MaxValue);

	[UnmanagedCallersOnly]
	private static JResult DestroyVirtualMachine(JVirtualMachineRef vmRef)
		=> ReferenceHelper.invokeProxies[vmRef].DestroyVirtualMachine();
	[UnmanagedCallersOnly]
	private static JResult GetEnv(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr, Int32 version)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		return ReferenceHelper.invokeProxies[vmRef].GetEnv(envRef.GetUnsafeValPtr(), version);
	}
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThread(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr,
		VirtualMachineArgumentValueWrapper* argPtr)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		ref VirtualMachineArgumentValueWrapper argRef = ref Unsafe.AsRef<VirtualMachineArgumentValueWrapper>(argPtr);
		return ReferenceHelper.invokeProxies[vmRef]
		                      .AttachCurrentThread(envRef.GetUnsafeValPtr(), argRef.GetUnsafeValPtr());
	}
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThreadAsDaemon(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr,
		VirtualMachineArgumentValueWrapper* argPtr)
	{
		ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
		ref VirtualMachineArgumentValueWrapper argRef = ref Unsafe.AsRef<VirtualMachineArgumentValueWrapper>(argPtr);
		return ReferenceHelper.invokeProxies[vmRef]
		                      .AttachCurrentThreadAsDaemon(envRef.GetUnsafeValPtr(), argRef.GetUnsafeValPtr());
	}
	[UnmanagedCallersOnly]
	private static JResult DetachCurrentThread(JVirtualMachineRef vmRef)
		=> ReferenceHelper.invokeProxies[vmRef].DetachCurrentThread();
}