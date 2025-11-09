using System.Diagnostics;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public static class Program
{
	public static async Task Main(String[] args)
	{
#if NET8_0
		if (IVirtualMachine.TypeMetadataToStringEnabled && JRuntimeInfo.MatchArch) JRuntimeInfo.PrintMetadataInfo();
#endif
		if (args.Length < 1) throw new ArgumentException("Please set JVM library path.");

		Byte[] helloJniByteCode = await (args.Length > 1 && !String.IsNullOrWhiteSpace(args[1]) ?
			File.ReadAllBytesAsync(Path.Combine(args[1], "HelloDotnet.class")) :
			File.ReadAllBytesAsync("HelloDotnet.class"));
		Console.WriteLine($"HelloDotnet.class: {helloJniByteCode.Length} bytes");

		JVirtualMachineLibrary jvmLib = JVirtualMachineLibrary.LoadLibrary(args[0]) ??
			throw new ArgumentException("Invalid JVM library.");

		Console.WriteLine($"JVM Library handle: 0x{jvmLib.Handle:x8}");

		String[] jMainArgs = AotInfo.IsReflectionDisabled ?
			[$"System Path: {Environment.SystemDirectory}",] :
			[
				$"System Path: {Environment.SystemDirectory}",
				$"Runtime Name: {RuntimeInformation.FrameworkDescription}",
			];

		ConsoleTraceListener? listener = default;

		if (JVirtualMachine.TraceEnabled)
		{
			listener = new();
			Trace.Listeners.Add(listener);
		}

		try
		{
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
			Int32 jdkVersion = jvmLib.GetLatestSupportedVersion();
			if (IVirtualMachine.MinimalVersion != initArgs.Version || initArgs.Options.Count != 0)
				if (IVirtualMachine.TypeMetadataToStringEnabled && JRuntimeInfo.MatchArch)
					Console.WriteLine(initArgs);
				else
					Console.WriteLine($"Min JDK Version: 0x{initArgs.Version:x8}");
			Console.WriteLine($"Supported JNI Version: 0x{jdkVersion:x8}");

			initArgs = new(jdkVersion)
			{
				Options = new("-DjniLib.load.disable=true", JRuntimeInfo.JniCheckOption, "-Xrs",
				              jdkVersion > (Int32)JRuntimeVersion.J8 ? "-XX:+ErrorFileToStdout" :
				              OperatingSystem.IsWindows() ? "-XX:ErrorFile=/dev/stderr" : default,
				              JVirtualMachine.TraceEnabled ? "-verbose:jni" : default,
				              JVirtualMachine.TraceEnabled ? "-verbose:class" : default,
				              JVirtualMachine.TraceEnabled ? "-verbose:gc" : default,
				              JVirtualMachine.TraceEnabled ? "-XX:+UnlockDiagnosticVMOptions" : default),
			};
			using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);
			try
			{
#if NET8_0
				if (IVirtualMachine.TypeMetadataToStringEnabled && JRuntimeInfo.MatchArch) 
					JRuntimeInfo.PrintVirtualMachineInfo(env, vm, jvmLib);
#endif
				String jreVersion = !AotInfo.IsReflectionDisabled ?
					$"{env.VirtualMachine.Version}" :
					$"0x{env.VirtualMachine.Version:x8}";
				Console.WriteLine($"==== JNI 0x{env.Version:x8} - JRE {jreVersion} ====");

				IManagedCallback.Default managedInstance = new(vm, Console.Out);
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