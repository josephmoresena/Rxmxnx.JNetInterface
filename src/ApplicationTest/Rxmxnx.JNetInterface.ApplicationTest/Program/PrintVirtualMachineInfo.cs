using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Program
{
	private static void PrintVirtualMachineInfo(IEnvironment env, IInvokedVirtualMachine vm,
		JVirtualMachineLibrary jvmLib)
	{
		Program.PrintAttachedThreadInfo(env);
		Program.PrintAttachThreadInfo(vm, new(() => "Main thread Re-Attached"u8), env);
		Task jvmT = Task.Factory.StartNew(Program.PrintAttachedThreadInfo, vm, TaskCreationOptions.LongRunning);
		jvmT.Wait();
		Console.WriteLine($"Supported version: 0x{jvmLib.GetLatestSupportedVersion():x8}");
		IVirtualMachine[] vms = jvmLib.GetCreatedVirtualMachines();
		foreach (IVirtualMachine jvm in vms)
			Console.WriteLine($"VM: {jvm.Reference} Type: {jvm.GetType()}");
	}
	private static void PrintAttachedThreadInfo(Object? obj)
	{
		if (obj is not IVirtualMachine vm) return;
		Console.WriteLine($"New Thread {vm.GetEnvironment() is null}");
		Program.PrintAttachThreadInfo(vm, new(() => "New thread 1"u8));
		Program.PrintAttachThreadInfo(vm, new(() => "New thread 2"u8));
	}
	private static void PrintAttachedThreadInfo(IEnvironment env)
	{
		IVirtualMachine vm = env.VirtualMachine;
		Console.WriteLine(vm.Reference);
		Console.WriteLine($"VM Version: 0x{env.Version:x8}");
		Console.WriteLine(
			$"Ref Equality: {env.Equals(vm.GetEnvironment())} - Instance Equality: {Object.ReferenceEquals(env, vm.GetEnvironment())}");
		Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId} {env.Reference} Type: {env.GetType()}");
	}
	private static void PrintAttachThreadInfo(IVirtualMachine vm, CString threadName, IEnvironment? env = default)
	{
		Boolean attached = vm.GetEnvironment() is null;
		Console.WriteLine(env is null ? $"Thread new: {attached}" : $"Thread attached: {attached}");
		using (IThread thread = vm.InitializeThread(threadName))
		{
			Program.PrintAttachedThreadInfo(thread);

			if (env is null)
				Program.PrintAttachThreadInfo(vm, CString.Concat(threadName, " Nested"u8), thread);
		}
		Console.WriteLine($"Thread detached: {vm.GetEnvironment() is null}");
	}
}