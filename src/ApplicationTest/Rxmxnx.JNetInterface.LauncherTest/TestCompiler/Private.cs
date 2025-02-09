using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private static async Task CompileNet(String projectFile, String rid, NetVersion netVersion, Publish publish,
		String publishDir)
	{
		ExecuteState<ValueTuple<String, String, NetVersion, Publish, String>> state = new()
		{
			ExecutablePath = "dotnet",
			ArgState = (projectFile, rid, netVersion, publish, publishDir),
			AppendArgs = (s, a) =>
			{
				a.Add("publish");
				a.Add(s.Item1);
				a.Add("-c");
				a.Add("Release");
				a.Add("-r");
				a.Add(s.Item2);
				a.Add($"/p:USE_NET80={s.Item3 is NetVersion.Net80}");
				a.Add($"/p:USE_NET90={s.Item3 is NetVersion.Net90}");
				a.Add($"/p:JNI_LIBRARY={s.Item4.HasFlag(Publish.JniLibrary)}");
				a.Add($"/p:PublishReadyToRun={s.Item4.HasFlag(Publish.ReadyToRun)}");
				a.Add($"/p:NativeAOT={s.Item4.HasFlag(Publish.NativeAot)}");
				a.Add($"/p:IlcDisableReflection={s.Item4.HasFlag(Publish.NoReflection)}");
				a.Add($"/p:PublishDir={s.Item5}");
			},
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state);
	}
	private static Boolean NativeAotSupported(String os, Architecture arch)
	{
		Architecture currentArch = RuntimeInformation.OSArchitecture;
		return arch == currentArch || os switch
		{
			"win" => arch is not Architecture.Arm64 && currentArch is not Architecture.X86,
			"osx" => arch is not Architecture.Arm64,
			_ => true,
		};
	}
}