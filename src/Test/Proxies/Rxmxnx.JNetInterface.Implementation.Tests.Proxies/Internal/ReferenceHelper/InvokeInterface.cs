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
	{
		try
		{
			return ReferenceHelper.GetProxy(vmRef).DestroyVirtualMachine();
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JResult GetEnv(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr, Int32 version)
	{
		try
		{
			ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
			return ReferenceHelper.GetProxy(vmRef).GetEnv(envRef.GetUnsafeValPtr(), version);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThread(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr,
		VirtualMachineArgumentValue* argPtr)
	{
		try
		{
			ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
			ref VirtualMachineArgumentValue argRef = ref Unsafe.AsRef<VirtualMachineArgumentValue>(argPtr);
			return ReferenceHelper.GetProxy(vmRef)
			                      .AttachCurrentThread(envRef.GetUnsafeValPtr(), argRef.GetUnsafeValPtr());
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JResult AttachCurrentThreadAsDaemon(JVirtualMachineRef vmRef, JEnvironmentRef* envRefPtr,
		VirtualMachineArgumentValue* argPtr)
	{
		try
		{
			ref JEnvironmentRef envRef = ref Unsafe.AsRef<JEnvironmentRef>(envRefPtr);
			ref VirtualMachineArgumentValue argRef = ref Unsafe.AsRef<VirtualMachineArgumentValue>(argPtr);
			return ReferenceHelper.GetProxy(vmRef)
			                      .AttachCurrentThreadAsDaemon(envRef.GetUnsafeValPtr(), argRef.GetUnsafeValPtr());
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JResult DetachCurrentThread(JVirtualMachineRef vmRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(vmRef).DetachCurrentThread();
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
}