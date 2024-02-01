using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public static class Program
{
	public static async Task Main(String[] args)
	{
		Program.PrintBuiltIntMetadata();
		Program.PrintArrayMetadata(JArrayObject<JInt>.Metadata, 10);

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

		Program.PrintVirtualMachineInfo(jvmLib, helloJniByteCode, "jiji", "esto es una coima mk");
	}
	private static void PrintArrayMetadata(JArrayTypeMetadata arrMetadata, Int32 deep)
	{
		Console.WriteLine(arrMetadata.ElementMetadata.Signature);
		for (Int32 i = 0; i < deep; i++)
		{
			Console.WriteLine(arrMetadata.Signature);
			if (JReferenceTypeMetadata.GetArrayMetadata(arrMetadata) is not { } arrMet2)
				break;
			arrMetadata = arrMet2;
		}
		Program.PrintNestedArrayMetadata(arrMetadata);
	}
	private static void PrintNestedArrayMetadata(JArrayTypeMetadata? arrMetadata, Boolean printCurrent = false)
	{
		if (printCurrent && arrMetadata is not null) Console.WriteLine(arrMetadata.Signature);
		while (arrMetadata is not null)
		{
			Console.WriteLine(arrMetadata.ElementMetadata.Signature);
			arrMetadata = arrMetadata.ElementMetadata as JArrayTypeMetadata;
		}
	}
	private static void PrintBuiltIntMetadata()
	{
		Console.WriteLine(IDataType.GetMetadata<JBoolean>());
		Console.WriteLine(IDataType.GetMetadata<JByte>());
		Console.WriteLine(IDataType.GetMetadata<JChar>());
		Console.WriteLine(IDataType.GetMetadata<JShort>());
		Console.WriteLine(IDataType.GetMetadata<JInt>());
		Console.WriteLine(IDataType.GetMetadata<JLong>());
		Console.WriteLine(IDataType.GetMetadata<JFloat>());
		Console.WriteLine(IDataType.GetMetadata<JDouble>());
		Console.WriteLine(IDataType.GetMetadata<JLocalObject>());
		Console.WriteLine(IDataType.GetMetadata<JClassObject>());
		Console.WriteLine(IDataType.GetMetadata<JStringObject>());
		Console.WriteLine(IDataType.GetMetadata<JEnumObject>());
		Console.WriteLine(IDataType.GetMetadata<JNumberObject>());
		Console.WriteLine(IDataType.GetMetadata<JThrowableObject>());

		Console.WriteLine("====== Array types ======");

		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JBoolean>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JByte>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JChar>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JShort>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JInt>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JLong>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JFloat>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JDouble>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JLocalObject>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JClassObject>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JStringObject>>());

		Console.WriteLine("====== Wrapper types ======");
		Console.WriteLine(IDataType.GetMetadata<JBooleanObject>());
		Console.WriteLine(IDataType.GetMetadata<JByteObject>());
		Console.WriteLine(IDataType.GetMetadata<JDoubleObject>());
		Console.WriteLine(IDataType.GetMetadata<JFloatObject>());
		Console.WriteLine(IDataType.GetMetadata<JIntegerObject>());
		Console.WriteLine(IDataType.GetMetadata<JCharacterObject>());
		Console.WriteLine(IDataType.GetMetadata<JLongObject>());
		Console.WriteLine(IDataType.GetMetadata<JShortObject>());
	}
	private static void PrintVirtualMachineInfo(JVirtualMachineLibrary jvmLib, Byte[] classByteCode,
		params String[] args)
	{
		try
		{
			JVirtualMachineInitArg initArgs = jvmLib.GetDefaultArgument();
			Console.WriteLine(initArgs);
			using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);
			try
			{
				Program.PrintVirtualMachineInfo(env, vm, jvmLib);
				JClassObject helloJniClass = JHelloDotnetObject.LoadClass(env, classByteCode);
				JMainMethodDefinition.Instance.Invoke(helloJniClass, args);
			}
			catch (JThrowableException ex)
			{
				Console.WriteLine(ex.WithSafeInvoke(t => t.Message));
			}
		}
		finally
		{
			NativeLibrary.Free(jvmLib.Handler);
		}
	}
	private static void PrintVirtualMachineInfo(IEnvironment env, IInvokedVirtualMachine vm,
		JVirtualMachineLibrary jvmLib)
	{
		Program.PrintAttachedThreadInfo(env);
		Program.PrintAttachThreadInfo(vm, new(() => "Main thread Re-Attached"u8), env);
		Task jvmT = Task.Factory.StartNew(Program.PrintAttachedThreadInfo, vm);
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
			$"Ref Equality: {env.Equals(vm.GetEnvironment())} - Instance Equality: {env == vm.GetEnvironment()}");
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