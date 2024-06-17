using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Io;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Lang.Annotation;
using Rxmxnx.JNetInterface.Lang.Reflect;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public static class Program
{
	public static async Task Main(String[] args)
	{
		if (IVirtualMachine.TypeMetadataToStringEnabled) Program.PrintMetadataInfo();

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
		Console.WriteLine(
			$"{nameof(IVirtualMachine.TypeMetadataToStringEnabled)}: {IVirtualMachine.TypeMetadataToStringEnabled}");
	}
	private static void PrintMetadataInfo()
	{
		Program.PrintBuiltInMetadata();
		Program.PrintArrayMetadata(JArrayObject<JBoolean>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JByte>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JChar>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JDouble>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JFloat>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JInt>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JLong>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JShort>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JLocalObject>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JClassObject>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JThrowableObject>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JStringObject>.Metadata, 5);
	}
	private static void PrintArrayMetadata(JArrayTypeMetadata arrMetadata, Int32 dimension)
	{
		Console.WriteLine(arrMetadata.ElementMetadata.Signature);
		Boolean stopMetadata = false;
		for (Int32 i = 0; i < dimension; i++)
		{
			Console.WriteLine(arrMetadata.Signature);
			if (arrMetadata.GetArrayMetadata() is not { } arrMet2)
			{
				stopMetadata = true;
				break;
			}
			arrMetadata = arrMet2;
		}
		if (!stopMetadata) Console.WriteLine(arrMetadata.Signature);
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
	private static void PrintBuiltInMetadata()
	{
		Console.WriteLine("====== Primitive types ======");
		Console.WriteLine(JPrimitiveTypeMetadata.VoidMetadata);
		Console.WriteLine(IDataType.GetMetadata<JBoolean>());
		Console.WriteLine(IDataType.GetMetadata<JByte>());
		Console.WriteLine(IDataType.GetMetadata<JChar>());
		Console.WriteLine(IDataType.GetMetadata<JShort>());
		Console.WriteLine(IDataType.GetMetadata<JInt>());
		Console.WriteLine(IDataType.GetMetadata<JLong>());
		Console.WriteLine(IDataType.GetMetadata<JFloat>());
		Console.WriteLine(IDataType.GetMetadata<JDouble>());

		Console.WriteLine("====== Reference types ======");
		Console.WriteLine(IDataType.GetMetadata<JLocalObject>());
		Console.WriteLine(IDataType.GetMetadata<JClassObject>());
		Console.WriteLine(IDataType.GetMetadata<JStringObject>());
		Console.WriteLine(IDataType.GetMetadata<JEnumObject>());
		Console.WriteLine(IDataType.GetMetadata<JNumberObject>());
		Console.WriteLine(IDataType.GetMetadata<JThrowableObject>());
		Console.WriteLine(IDataType.GetMetadata<JStackTraceElementObject>());
		Console.WriteLine(IDataType.GetMetadata<JExceptionObject>());
		Console.WriteLine(IDataType.GetMetadata<JRuntimeExceptionObject>());
		Console.WriteLine(IDataType.GetMetadata<JErrorObject>());

		Console.WriteLine("====== Wrapper types ======");
		Console.WriteLine(IDataType.GetMetadata<JVoidObject>());
		Console.WriteLine(IDataType.GetMetadata<JBooleanObject>());
		Console.WriteLine(IDataType.GetMetadata<JByteObject>());
		Console.WriteLine(IDataType.GetMetadata<JDoubleObject>());
		Console.WriteLine(IDataType.GetMetadata<JFloatObject>());
		Console.WriteLine(IDataType.GetMetadata<JIntegerObject>());
		Console.WriteLine(IDataType.GetMetadata<JCharacterObject>());
		Console.WriteLine(IDataType.GetMetadata<JLongObject>());
		Console.WriteLine(IDataType.GetMetadata<JShortObject>());

		Console.WriteLine("====== Array types ======");
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JBoolean>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JByte>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JChar>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JShort>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JInt>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JLong>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JFloat>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JDouble>>());

		Console.WriteLine("====== Interfaces types ======");
		Console.WriteLine(IDataType.GetMetadata<JCharSequenceObject>());
		Console.WriteLine(IDataType.GetMetadata<JCloneableObject>());
		Console.WriteLine(IDataType.GetMetadata<JComparableObject>());
		Console.WriteLine(IDataType.GetMetadata<JSerializableObject>());
		Console.WriteLine(IDataType.GetMetadata<JAnnotatedElementObject>());
		Console.WriteLine(IDataType.GetMetadata<JGenericDeclarationObject>());
		Console.WriteLine(IDataType.GetMetadata<JTypeObject>());
		Console.WriteLine(IDataType.GetMetadata<JAnnotationObject>());
	}
	private static void Execute(JVirtualMachineLibrary jvmLib, Byte[] classByteCode, params String[] args)
	{
		try
		{
			JVirtualMachineInitArg initArgs = jvmLib.GetDefaultArgument();
			if (IVirtualMachine.TypeMetadataToStringEnabled) Console.WriteLine(initArgs);
			using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);
			try
			{
				if (IVirtualMachine.TypeMetadataToStringEnabled) Program.PrintVirtualMachineInfo(env, vm, jvmLib);
				using JClassObject helloJniClass = JHelloDotnetObject.LoadClass(env, classByteCode);
				JMainMethodDefinition.Instance.Invoke(helloJniClass, args);
				JInt count = new JFieldDefinition<JInt>("COUNT_RANDOM"u8).StaticGet(helloJniClass);
				for (JInt i = 0; i < count; i++)
				{
					using JLocalObject? jLocal = GetRandomObjectDefinition.Instance.Invoke(helloJniClass, i);
					Console.WriteLine($"{i}: {jLocal}");
				}
			}
			catch (ThrowableException ex)
			{
				Console.WriteLine(ex.Message);
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