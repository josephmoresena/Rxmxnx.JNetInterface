using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Io;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Lang.Annotation;
using Rxmxnx.JNetInterface.Lang.Reflect;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

/// <summary>
/// Java compiler.
/// </summary>
public static class JRuntimeInfo
{
	public static Boolean MatchArch = RuntimeInformation.OSArchitecture == RuntimeInformation.ProcessArchitecture;
	public static String JniCheckOption
		=> Boolean.TryParse(Environment.GetEnvironmentVariable("JNETINTERFACE_JNI_CHECK"), out Boolean useJniCheck) &&
			useJniCheck ?
				"-Xcheck:jni" :
				String.Empty;
	public static void PrintMetadataInfo()
	{
		JRuntimeInfo.PrintBuiltInMetadata();
		JRuntimeInfo.PrintJaggedArrayMetadataInfo();
	}
	public static void PrintJaggedArrayMetadataInfo()
	{
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JBoolean>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JByte>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JChar>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JDouble>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JFloat>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JInt>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JLong>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JShort>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JLocalObject>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JClassObject>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JThrowableObject>.Metadata);
		JRuntimeInfo.PrintArrayMetadata(JArrayObject<JStringObject>.Metadata);
	}
	public static void PrintVirtualMachineInfo(IEnvironment env, IInvokedVirtualMachine vm,
		JVirtualMachineLibrary jvmLib)
	{
		JRuntimeInfo.PrintAttachedThreadInfo(env);
		JRuntimeInfo.PrintAttachThreadInfo(vm, new(() => "Main thread Re-Attached"u8), env);
		JRuntimeInfo.PrintVirtualMachineInfoAsync(vm).Wait();
		Console.WriteLine($"Supported version: 0x{jvmLib.GetLatestSupportedVersion():x8}");
		IVirtualMachine[] vms = jvmLib.GetCreatedVirtualMachines();
		foreach (IVirtualMachine jvm in vms)
			Console.WriteLine($"VM: {jvm.Reference} Type: {jvm.GetType()}");
	}
	private static async Task PrintVirtualMachineInfoAsync(IInvokedVirtualMachine vm)
	{
		try
		{
			await Task.Factory.StartNew(JRuntimeInfo.PrintAttachedThreadInfo, vm, TaskCreationOptions.LongRunning);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}
	private static void PrintArrayMetadata(JArrayTypeMetadata arrMetadata)
	{
		ReadOnlySpan<Byte> signature = arrMetadata.Signature;
		Console.Write(arrMetadata.ElementMetadata.Signature);
		Console.Write("->");
		Console.Write(arrMetadata.Signature);
		for (Int32 i = 0; i < 32; i++)
		{
			if (arrMetadata.GetArrayMetadata() is not { } arrMet2) break;
			arrMetadata = arrMet2;
		}
		if (!signature.SequenceEqual(arrMetadata.Signature))
		{
			Console.Write("->");
			Console.WriteLine(arrMetadata.Signature);
		}
		Console.WriteLine();
	}
	private static void PrintBuiltInMetadata()
	{
		Console.WriteLine("====== Primitive types ======");
		Console.WriteLine(JPrimitiveTypeMetadata.VoidMetadata);
		Console.WriteLine(IDataType.GetMetadata<JBoolean>());
		Console.WriteLine(IDataType.GetMetadata<JChar>());
		Console.WriteLine(IDataType.GetMetadata<JInt>());
		Console.WriteLine(IDataType.GetMetadata<JDouble>());

		Console.WriteLine("====== Reference types ======");
		Console.WriteLine(IDataType.GetMetadata<JLocalObject>());
		Console.WriteLine(IDataType.GetMetadata<JClassObject>());
		Console.WriteLine(IDataType.GetMetadata<JRuntimeExceptionObject>());
		Console.WriteLine(IDataType.GetMetadata<JErrorObject>());

		Console.WriteLine("====== Wrapper types ======");
		Console.WriteLine(IDataType.GetMetadata<JVoidObject>());
		Console.WriteLine(IDataType.GetMetadata<JByteObject>());
		Console.WriteLine(IDataType.GetMetadata<JFloatObject>());
		Console.WriteLine(IDataType.GetMetadata<JLongObject>());

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
		Console.WriteLine(IDataType.GetMetadata<JSerializableObject>());
		Console.WriteLine(IDataType.GetMetadata<JGenericDeclarationObject>());
		Console.WriteLine(IDataType.GetMetadata<JAnnotationObject>());
	}
	private static void PrintAttachedThreadInfo(Object? obj)
	{
		if (obj is not IVirtualMachine vm) return;
		Console.WriteLine($"New Thread {vm.GetEnvironment() is null}");
		JRuntimeInfo.PrintAttachThreadInfo(vm, new(() => "New thread 1"u8));
		JRuntimeInfo.PrintAttachThreadInfo(vm, new(() => "New thread 2"u8));
	}
	private static void PrintAttachedThreadInfo(IEnvironment env)
	{
		IVirtualMachine vm = env.VirtualMachine;
		Console.WriteLine(vm.Reference);
		Console.WriteLine(
			$"VM Version: 0x{env.Version:x8} Thread: {Environment.CurrentManagedThreadId} {env.Reference} Type: {env.GetType()}");
		Console.WriteLine(
			$"Ref Equality: {env.Equals(vm.GetEnvironment())} - Instance Equality: {Object.ReferenceEquals(env, vm.GetEnvironment())}");
	}
	private static void PrintAttachThreadInfo(IVirtualMachine vm, CString threadName, IEnvironment? env = default)
	{
		Boolean attached = vm.GetEnvironment() is null;
		Console.WriteLine(env is null ? $"Thread new: {attached}" : $"Thread attached: {attached}");
		using (IThread thread = vm.InitializeThread(threadName))
		{
			JRuntimeInfo.PrintAttachedThreadInfo(thread);

			if (env is null)
				JRuntimeInfo.PrintAttachThreadInfo(vm, CString.Concat(threadName, " Nested"u8), thread);
		}
		Console.WriteLine($"Thread detached: {vm.GetEnvironment() is null}");
	}
}