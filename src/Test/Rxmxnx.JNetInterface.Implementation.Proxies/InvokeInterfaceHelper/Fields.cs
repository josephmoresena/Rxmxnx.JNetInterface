namespace Rxmxnx.JNetInterface.Tests;

public static unsafe partial class InvokeInterfaceHelper
{
	private static readonly InternalInvokeInterface invokeInterface = new()
	{
		DestroyVirtualMachine = &InvokeInterfaceHelper.DestroyVirtualMachine,
		AttachCurrentThread = &InvokeInterfaceHelper.AttachCurrentThread,
		DetachCurrentThread = &InvokeInterfaceHelper.DetachCurrentThread,
		AttachCurrentThreadAsDaemon = &InvokeInterfaceHelper.AttachCurrentThreadAsDaemon,
		GetEnv = &InvokeInterfaceHelper.GetEnv,
	};
	private static readonly ConcurrentDictionary<JVirtualMachineRef, InvokeInterfaceProxy> proxies = new();
	private static readonly MemoryHelper helper =
		new(NativeUtilities.GetUnsafeValPtr(InvokeInterfaceHelper.invokeInterface), Int16.MaxValue);
}