using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.References;

namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
internal static class ExportedMethods
{
	[UnmanagedCallersOnly(EntryPoint = "JNI_OnLoad")]
	private static Int32 LoadLibrary(JVirtualMachineRef vmRef, IntPtr _)
	{
		IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(vmRef);
		using IThread thread = vm.InitializeThread(new(() => "Load Thread"u8));
		JHelloDotnetObject.SetCallback(thread, new IManagedCallback.Default(vm));
		return IVirtualMachine.MinimalVersion;
	}
	[UnmanagedCallersOnly(EntryPoint = "JNI_OnUnload")]
	private static void UnloadLibrary(JVirtualMachineRef vmRef, IntPtr _)
	{
		try
		{
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(vmRef);
			using IThread thread = vm.InitializeThread(new(() => "Unload Thread"u8));
			JClassObject.GetClass<JHelloDotnetObject>(thread).UnregisterNativeCalls();
		}
		finally
		{
			JVirtualMachine.RemoveVirtualMachine(vmRef);
		}
	}
}