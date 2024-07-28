using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public partial class Program
{
	public static async Task Main(String[] args)
	{
		if (IVirtualMachine.TypeMetadataToStringEnabled) Program.PrintMetadataInfo();
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

		Console.WriteLine("==== Feature Switches ====");
		Console.WriteLine($"{nameof(Program)}: {typeof(Program)}");
		Console.WriteLine($"{nameof(IVirtualMachine.TraceEnabled)}: {IVirtualMachine.TraceEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.MetadataValidationEnabled)}: {IVirtualMachine.MetadataValidationEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.NestingArrayAutoGenerationEnabled)}: {IVirtualMachine.NestingArrayAutoGenerationEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.TypeMetadataToStringEnabled)}: {IVirtualMachine.TypeMetadataToStringEnabled}");
		Console.WriteLine(
			$"{nameof(JVirtualMachine.FinalUserTypeRuntimeEnabled)}: {JVirtualMachine.FinalUserTypeRuntimeEnabled}");
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
				Program programInstance = new(vm);
				using JClassObject helloJniClass = JHelloDotnetObject.LoadClass(env, classByteCode, programInstance);
				Console.WriteLine("==== Begin psvm ===");
				JMainMethodDefinition.Instance.Invoke(helloJniClass, args);
				Console.WriteLine("==== End psvm ===");
				JInt count = new JFieldDefinition<JInt>("COUNT"u8).StaticGet(helloJniClass);
				for (JInt i = 0; i < count; i++)
				{
					using JLocalObject? jLocal = GetObjectDefinition.Instance.Invoke(helloJniClass, i);
					Console.WriteLine($"getObject({i}) -> {jLocal}");
				}
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