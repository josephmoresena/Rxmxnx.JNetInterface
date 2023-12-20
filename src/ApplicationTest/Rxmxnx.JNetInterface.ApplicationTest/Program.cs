using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public static class Program
{
	public static void Main(String[] args)
	{
		Console.WriteLine("Hello, World!");
		Program.PrintBuiltIntMetadata();
		//Program.PrintArrayMetadata(JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JInt>>>>>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JInt>.Metadata, 10);
		//Program.PrintVirtualMachineInfo("/Library/Java/JavaVirtualMachines/jdk-21.jdk/Contents/Home/lib/server/libjvm.dylib");
		//Program.PrintVirtualMachineInfo("/Library/Java/JavaVirtualMachines/jdk-17.jdk/Contents/Home/lib/server/libjvm.dylib");
		//Program.PrintVirtualMachineInfo("/Library/Java/JavaVirtualMachines/jdk-11.jdk/Contents/Home/lib/server/libjvm.dylib");
		//Program.PrintVirtualMachineInfo("/Library/Java/JavaVirtualMachines/jdk-1.8.jdk/Contents/Home/jre/lib/server/libjvm.dylib");
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
	private static void PrintVirtualMachineInfo(String path)
	{
		JVirtualMachineLibrary? jvmLib = JVirtualMachineLibrary.LoadLibrary(path);
		if (jvmLib is null)
		{
			Console.WriteLine("Invalid JVM library.");
			return;
		}
		try
		{
			JVirtualMachineInitArg args = jvmLib.GetDefaultArgument();
			Console.WriteLine(args);
			using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(args, out IEnvironment env);
			Program.PrintAttachedThreadInfo(env);
			Program.PrintAttachThreadInfo(vm, new(() => "Main thread Re-Attached"u8), env);
			Task jvmT = Task.Factory.StartNew(Program.PrintAttachedThreadInfo, vm);
			jvmT.Wait();
			Console.WriteLine($"Supported version: 0x{jvmLib.GetLatestSupportedVersion():x8}");
			IVirtualMachine[] vms = jvmLib.GetCreatedVirtualMachines();
			foreach (IVirtualMachine jvm in vms)
				Console.WriteLine($"VM: {jvm.Reference} Type: {jvm.GetType()}");
		}
		finally
		{
			NativeLibrary.Free(jvmLib.Handler);
		}
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
				Program.PrintAttachThreadInfo(vm, new(() => CString.Concat(threadName, " Nested"u8)), thread);
		}
		Console.WriteLine($"Thread detached: {vm.GetEnvironment() is null}");
	}
}