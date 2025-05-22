using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.Tests;

/// <summary>
/// Native proxy for JVM library functions.
/// </summary>
[ExcludeFromCodeCoverage]
[SuppressMessage("csharpsquid", "S6640:Using unsafe code blocks is security-sensitive",
                 Justification = "JNI code is secure to use.")]
internal static unsafe class NativeProxy
{
	[UnmanagedCallersOnly(EntryPoint = "JNI_GetDefaultJavaVMInitArgs_Impl")]
	public static Int32 JNI_GetDefaultJavaVMInitArgs(IntPtr args)
		=>
#if !DISABLE_CREATE_VM && !DISABLE_GET_CREATED_VMS
			NativeProxy.getDefaultVmInitArgs != null ?
				NativeProxy.getDefaultVmInitArgs(args) :
#endif
				-1;

#if !DISABLE_CREATE_VM
	[UnmanagedCallersOnly(EntryPoint = "JNI_CreateJavaVM")]
#endif
	public static Int32 JNI_CreateJavaVM(IntPtr pvm, IntPtr penv, IntPtr args)
		=>
#if !DISABLE_CREATE_VM && !DISABLE_GET_CREATED_VMS
			NativeProxy.createVm != null ?
				NativeProxy.createVm(pvm, penv, args) :
#endif
				-1;

#if !DISABLE_GET_CREATED_VMS
	[UnmanagedCallersOnly(EntryPoint = "JNI_GetCreatedJavaVMs")]
#endif
	public static Int32 JNI_GetCreatedJavaVMs(IntPtr vmBuf, Int32 bufLen, IntPtr nVMs)
		=>
#if !DISABLE_CREATE_VM && !DISABLE_GET_CREATED_VMS
			NativeProxy.getCreatedVMs != null ?
				NativeProxy.getCreatedVMs(vmBuf, bufLen, nVMs) :
#endif
				-1;
#if !DISABLE_CREATE_VM && !DISABLE_GET_CREATED_VMS
	[ThreadStatic]
	private static delegate*<IntPtr, Int32> getDefaultVmInitArgs;
	[ThreadStatic]
	private static delegate*<IntPtr, IntPtr, IntPtr, Int32> createVm;
	[ThreadStatic]
	private static delegate*<IntPtr, Int32, IntPtr, Int32> getCreatedVMs;
#endif

#if !DISABLE_CREATE_VM && !DISABLE_GET_CREATED_VMS
	/// <summary>
	/// Sets up function pointers for the current thread.
	/// </summary>
	[UnmanagedCallersOnly(EntryPoint = "ArrangeInvocation")]
	public static void ArrangeInvocation(delegate*<IntPtr, Int32> getDefaultVmInitArgsFunc,
		delegate*<IntPtr, IntPtr, IntPtr, Int32> createVmFunc,
		delegate*<IntPtr, Int32, IntPtr, Int32> getCreatedVMsFunc)
	{
		NativeProxy.getDefaultVmInitArgs = getDefaultVmInitArgsFunc;
		NativeProxy.createVm = createVmFunc;
		NativeProxy.getCreatedVMs = getCreatedVMsFunc;
	}

	/// <summary>
	/// Resets all function pointers for the current thread.
	/// </summary>
	[UnmanagedCallersOnly(EntryPoint = "Reset")]
	public static void Reset()
	{
		NativeProxy.getDefaultVmInitArgs = null;
		NativeProxy.createVm = null;
		NativeProxy.getCreatedVMs = null;
	}
#endif
}