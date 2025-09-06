using System.Diagnostics;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.References;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal static class ExportedMethods
{
	private static ConsoleTraceListener? traceListener;

	[UnmanagedCallersOnly(EntryPoint = "JNI_OnLoad")]
	private static Int32 LoadLibrary(JVirtualMachineRef vmRef, IntPtr _)
	{
		if (JVirtualMachine.TraceEnabled)
		{
			ExportedMethods.traceListener = new();
			Trace.Listeners.Add(ExportedMethods.traceListener);
		}

		IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(vmRef);
		if (vm.GetEnvironment() is { } env)
		{
			Trace.WriteLine($"Initialized JEnv: {env.Reference}");
			SetCallback(env);
		}
		else
		{
			using IThread thread = vm.InitializeThread(new(() => "Load Thread"u8));
			Trace.WriteLine($"Initialized JThread: {thread.Reference}");
			thread.WithFrame(10, thread, SetCallback);
		}
		return IVirtualMachine.MinimalVersion;
		static void SetCallback(IEnvironment env)
		{
			JHelloDotnetObject.SetCallback(env, new IManagedCallback.Default(env.VirtualMachine, Console.Out));
		}
	}
	[UnmanagedCallersOnly(EntryPoint = "JNI_OnUnload")]
	private static void UnloadLibrary(JVirtualMachineRef vmRef, IntPtr _)
	{
		try
		{
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(vmRef);
			using IThread thread = vm.InitializeThread(new(() => "Unload Thread"u8));
			using JClassObject jClass = JClassObject.GetClass<JHelloDotnetObject>(thread);
			jClass.UnregisterNativeCalls();
		}
		finally
		{
			JVirtualMachine.RemoveVirtualMachine(vmRef);

			if (ExportedMethods.traceListener is { } consoleTrace)
			{
				Trace.Listeners.Remove(consoleTrace);
				ExportedMethods.traceListener.Dispose();
				ExportedMethods.traceListener = null;
			}
		}
	}
}