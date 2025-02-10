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
		if (IVirtualMachine.TypeMetadataToStringEnabled) JRuntimeInfo.PrintMetadataInfo();

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

		Program.Execute(jvmLib, helloJniByteCode, jMainArgs);

		IManagedCallback.PrintSwitches();
	}
	private static JCompiler? GetCompiler()
	{
		JCompiler[] compilers = JCompiler.GetCompilers();
		JCompiler? selected = compilers.LastOrDefault();
		foreach (JCompiler compiler in compilers)
			Console.WriteLine($"{compiler.JdkPath} {(compiler == selected ? '*' : ' ')}");
		return selected;
	}
	private static void Execute(JVirtualMachineLibrary jvmLib, Byte[] classByteCode, params String[] args)
	{
		try
		{
			JVirtualMachineInitArg initArgs = jvmLib.GetDefaultArgument();
			if (IVirtualMachine.TypeMetadataToStringEnabled) Console.WriteLine(initArgs);
			if (IVirtualMachine.TraceEnabled)
				initArgs = new(initArgs.Version)
				{
					Options = new("-verbose:jni", "-verbose:class", "-verbose:gc", "-Dno-native-load=true"),
				};
			else
				initArgs = new(initArgs.Version) { Options = new("-Dno-native-load=true"), };
			using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);
			try
			{
				if (IVirtualMachine.TypeMetadataToStringEnabled) JRuntimeInfo.PrintVirtualMachineInfo(env, vm, jvmLib);
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