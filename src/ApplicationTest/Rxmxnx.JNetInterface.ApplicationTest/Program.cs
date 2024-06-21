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

		Program.Execute(jvmLib, helloJniByteCode, $"System Path: {Environment.SystemDirectory}",
		                $"Runtime Name: {RuntimeInformation.FrameworkDescription}");

		Console.WriteLine($"{nameof(Program)}: {typeof(Program)}");
		Console.WriteLine($"{nameof(IVirtualMachine.TraceEnabled)}: {IVirtualMachine.TraceEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.MetadataValidationEnabled)}: {IVirtualMachine.MetadataValidationEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.NestingArrayAutoGenerationEnabled)}: {IVirtualMachine.NestingArrayAutoGenerationEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.TypeMetadataToStringEnabled)}: {IVirtualMachine.TypeMetadataToStringEnabled}");
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
				JMainMethodDefinition.Instance.Invoke(helloJniClass, args);
				JInt count = new JFieldDefinition<JInt>("COUNT_RANDOM"u8).StaticGet(helloJniClass);
				for (JInt i = 0; i < count; i++)
				{
					using JLocalObject? jLocal = GetRandomObjectDefinition.Instance.Invoke(helloJniClass, i);
					Console.WriteLine($"getRandomObject({i}) -> {jLocal}");
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
}