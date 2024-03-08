using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Io;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Lang.Annotation;
using Rxmxnx.JNetInterface.Lang.Reflect;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Nio;
using Rxmxnx.JNetInterface.Nio.Ch;
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
		Program.PrintBuiltIntMetadata();
		Program.PrintArrayMetadata(JArrayObject<JBoolean>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JByte>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JChar>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JDouble>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JFloat>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JInt>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JLong>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JShort>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JArrayObject<JLocalObject>>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JArrayObject<JClassObject>>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JArrayObject<JThrowableObject>>.Metadata, 10);
		Program.PrintArrayMetadata(JArrayObject<JArrayObject<JStringObject>>.Metadata, 10);

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
	private static void PrintArrayMetadata(JArrayTypeMetadata arrMetadata, Int32 dimension)
	{
		Console.WriteLine(arrMetadata.ElementMetadata.Signature);
		for (Int32 i = 0; i < dimension; i++)
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
		Console.WriteLine(IDataType.GetMetadata<JModifierObject>());

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
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JEnumObject>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JNumberObject>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JThrowableObject>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JModifierObject>>());

		Console.WriteLine("====== Wrapper types ======");
		Console.WriteLine(IDataType.GetMetadata<JBooleanObject>());
		Console.WriteLine(IDataType.GetMetadata<JByteObject>());
		Console.WriteLine(IDataType.GetMetadata<JDoubleObject>());
		Console.WriteLine(IDataType.GetMetadata<JFloatObject>());
		Console.WriteLine(IDataType.GetMetadata<JIntegerObject>());
		Console.WriteLine(IDataType.GetMetadata<JCharacterObject>());
		Console.WriteLine(IDataType.GetMetadata<JLongObject>());
		Console.WriteLine(IDataType.GetMetadata<JShortObject>());

		Console.WriteLine("====== NIO types ======");
		Console.WriteLine(IDataType.GetMetadata<JByteBufferObject>());
		Console.WriteLine(IDataType.GetMetadata<JMappedByteBufferObject>());
		Console.WriteLine(IDataType.GetMetadata<JDirectByteBufferObject>());

		Console.WriteLine("====== Reflect types ======");
		Console.WriteLine(IDataType.GetMetadata<JAccessibleObject>());
		Console.WriteLine(IDataType.GetMetadata<JFieldObject>());
		Console.WriteLine(IDataType.GetMetadata<JExecutableObject>());
		Console.WriteLine(IDataType.GetMetadata<JMethodObject>());
		Console.WriteLine(IDataType.GetMetadata<JConstructorObject>());

		Console.WriteLine("====== Interfaces types ======");
		Console.WriteLine(IDataType.GetMetadata<JSerializableObject>());
		Console.WriteLine(IDataType.GetMetadata<JComparableObject>());
		Console.WriteLine(IDataType.GetMetadata<JCharSequenceObject>());
		Console.WriteLine(IDataType.GetMetadata<JDirectBufferObject>());
		Console.WriteLine(IDataType.GetMetadata<JAnnotatedElementObject>());
		Console.WriteLine(IDataType.GetMetadata<JMemberObject>());
		Console.WriteLine(IDataType.GetMetadata<JTypeObject>());
		Console.WriteLine(IDataType.GetMetadata<JGenericDeclarationObject>());
		Console.WriteLine(IDataType.GetMetadata<JAnnotationObject>());

		Console.WriteLine("==== Annotation package ====");
		Console.WriteLine(IDataType.GetMetadata<JElementTypeObject>());
		Console.WriteLine(IDataType.GetMetadata<JTargetObject>());
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
				using JClassObject helloJniClass = JHelloDotnetObject.LoadClass(env, classByteCode);
				JMainMethodDefinition.Instance.Invoke(helloJniClass, args);
				JInt count = new JFieldDefinition<JInt>("COUNT_RANDOM"u8).StaticGet(helloJniClass);
				GetRandomObjectDefinition getRandomObjectDefinition = new("getRandomObject"u8);
				for (JInt i = 0; i < count; i++)
				{
					using JLocalObject? jLocal = getRandomObjectDefinition.Invoke(helloJniClass, i);
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