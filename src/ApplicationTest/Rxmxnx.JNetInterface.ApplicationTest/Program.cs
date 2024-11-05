using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;

namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public static class Program
{
	public static async Task Main(String[] args)
	{
		if (IVirtualMachine.TypeMetadataToStringEnabled) JRuntimeInfo.PrintMetadataInfo();
		Boolean reflectionDisabled = !$"{typeof(Program)}".Contains(nameof(Program));

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

		String[] jMainArgs = reflectionDisabled ?
			[$"System Path: {Environment.SystemDirectory}",] :
			[
				$"System Path: {Environment.SystemDirectory}",
				$"Runtime Name: {RuntimeInformation.FrameworkDescription}",
			];
		Program.Execute(jvmLib, helloJniByteCode, jMainArgs);

		IManagedCallback.PrintSwitches();
	}
	private static void Execute(JVirtualMachineLibrary jvmLib, Byte[] classByteCode, params String[] args)
	{
		try
		{
			JVirtualMachineInitArg initArgs = jvmLib.GetDefaultArgument();
			if (IVirtualMachine.TypeMetadataToStringEnabled) Console.WriteLine(initArgs);
			if (IVirtualMachine.TraceEnabled)
				initArgs = new(initArgs.Version) { Options = new("-verbose:jni", "-verbose:class", "-verbose:gc"), };
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
		}
		finally
		{
			NativeLibrary.Free(jvmLib.Handle);
		}
	}
}