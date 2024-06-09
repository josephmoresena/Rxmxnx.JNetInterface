using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public static class Program
{
	private const Boolean ShowInfo = true;

	public static async Task Main(String[] args)
	{
		JCompiler? compiler = args.Length == 3 ?
			new() { JdkPath = args[0], CompilerPath = args[1], LibraryPath = args[2], } :
			JCompiler.GetCompilers().FirstOrDefault();

		if (compiler is null)
		{
			Console.WriteLine("JDK not found.");
			return;
		}

		Byte[] helloJniByteCode = await compiler.CompileHelloJniClassAsync();
		JVirtualMachineLibrary jvmLib = compiler.GetLibrary();

		JHelloDotnetObject.PassStringEvent += Console.WriteLine;
		JHelloDotnetObject.GetIntegerEvent += () => Environment.CurrentManagedThreadId;
		JHelloDotnetObject.GetStringEvent += () => "Hola desde .NET";

		Program.Execute(jvmLib, helloJniByteCode, "jiji", "esto es una coima mk");

		Console.WriteLine($"{nameof(Program)}: {typeof(Program)}");
		Console.WriteLine($"{nameof(IVirtualMachine.TraceEnabled)}: {IVirtualMachine.TraceEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.MetadataValidationEnabled)}: {IVirtualMachine.MetadataValidationEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.NestingArrayAutoGenerationEnabled)}: {IVirtualMachine.NestingArrayAutoGenerationEnabled}");
	}
	private static void Execute(JVirtualMachineLibrary jvmLib, Byte[] classByteCode, params String[] args)
	{
		try
		{
			JVirtualMachineInitArg initArgs = jvmLib.GetDefaultArgument();
			if (Program.ShowInfo) Console.WriteLine(initArgs);
			using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);
			try
			{
				if (Program.ShowInfo) Program.PrintVirtualMachineInfo(env, vm, jvmLib);
				using JClassObject helloJniClass = JHelloDotnetObject.LoadClass(env, classByteCode);
				JMainMethodDefinition.Instance.Invoke(helloJniClass, args);
				JInt count = new JFieldDefinition<JInt>("COUNT_RANDOM"u8).StaticGet(helloJniClass);
				for (JInt i = 0; i < count; i++)
				{
					using JLocalObject? jLocal = GetRandomObjectDefinition.Instance.Invoke(helloJniClass, i);
					Console.WriteLine($"{i}: {jLocal?.ToString() ?? "null"}");
				}
			}
			catch (JniException jex)
			{
				if (jex is ThrowableException tex)
					Console.WriteLine(tex.WithSafeInvoke(t => t.ToString()));
				else
					Console.WriteLine(jex.Message);
				env.PendingException = default;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		finally
		{
			NativeLibrary.Free(jvmLib.Handle);
		}
	}
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