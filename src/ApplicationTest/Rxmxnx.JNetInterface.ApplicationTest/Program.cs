using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public static class Program
{
	public static async Task Main(String[] args)
	{
#if NET8_0
		if (IVirtualMachine.TypeMetadataToStringEnabled) JRuntimeInfo.PrintMetadataInfo();
#endif

		if (args.Length < 1)
			throw new ArgumentException("Please set JVM library path.");

		Byte[] helloJniByteCode = await File.ReadAllBytesAsync("HelloDotnet.class");
		JVirtualMachineLibrary jvmLib = JVirtualMachineLibrary.LoadLibrary(args[0]) ??
			throw new ArgumentException("Invalid JVM library.");

		String[] jMainArgs = AotInfo.IsReflectionDisabled ?
			[$"System Path: {Environment.SystemDirectory}",] :
			[
				$"System Path: {Environment.SystemDirectory}",
				$"Runtime Name: {RuntimeInformation.FrameworkDescription}",
			];

		ConsoleTraceListener? listener = default;

		if (IVirtualMachine.TraceEnabled)
		{
			listener = new();
			Trace.Listeners.Add(listener);
		}

		try
		{
			using MemoryHandle handle = helloJniByteCode.AsMemory().Pin();
			Program.Execute(jvmLib, helloJniByteCode, jMainArgs);
		}
		finally
		{
			if (listener is not null)
			{
				Trace.Listeners.Remove(listener);
				listener.Dispose();
			}
		}

		IManagedCallback.PrintSwitches();
	}
	private static void Execute(JVirtualMachineLibrary jvmLib, Byte[] classByteCode, params String[] args)
	{
		try
		{
			JVirtualMachineInitArg initArgs = jvmLib.GetDefaultArgument();
#if NET8_0
			if (IVirtualMachine.TypeMetadataToStringEnabled) Console.WriteLine(initArgs);
#endif
			if (IVirtualMachine.TraceEnabled)
				initArgs = new(initArgs.Version)
				{
					Options = new("-verbose:jni", "-verbose:class", "-verbose:gc", "-DjniLib.load.disable=true"),
				};
			else
				initArgs = new(initArgs.Version) { Options = new("-DjniLib.load.disable=true"), };
			using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);
			try
			{
#if NET8_0
				if (IVirtualMachine.TypeMetadataToStringEnabled) JRuntimeInfo.PrintVirtualMachineInfo(env, vm, jvmLib);
#endif
				IManagedCallback.Default managedInstance = new(vm);
				using JClassObject helloJniClass = JHelloDotnetObject.LoadClass(env, classByteCode, managedInstance);
				Console.WriteLine("==== Begin psvm ===");
				JMainMethodDefinition.Instance.Invoke(helloJniClass, args);
				Console.WriteLine("==== End psvm ===");
				JHelloDotnetObject.GetObject(helloJniClass);
				helloJniClass.UnregisterNativeCalls();
			}
			catch (ThrowableException ex)
			{
				Console.WriteLine(ex.WithSafeInvoke(t => t.ToString()));
				env.PendingException = default;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
		finally
		{
			NativeLibrary.Free(jvmLib.Handle);
		}
	}
}