namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private static async Task CompileNet(String projectFile, String rid, NetVersion netVersion, Publish publish,
		String outputPath, Boolean buildDependencies = true)
	{
		ExecuteState<ValueTuple<String, String, NetVersion, Publish, String, Boolean>> state = new()
		{
			ExecutablePath = "dotnet",
			ArgState = (projectFile, rid, netVersion, publish, outputPath, buildDependencies),
			AppendArgs = (s, a) =>
			{
				a.Add("publish");
				a.Add(s.Item1);
				a.Add("-c");
				a.Add("Release");
				a.Add("-r");
				a.Add(s.Item2);
				a.Add("/p:RestorePackages=false");
				a.Add($"/p:BuildProjectReferences={buildDependencies}");
				a.Add($"/p:USE_NET80={s.Item3 is NetVersion.Net80}");
				a.Add($"/p:USE_NET90={s.Item3 is NetVersion.Net90}");
				a.Add($"/p:JNI_LIBRARY={s.Item4.HasFlag(Publish.JniLibrary)}");
				a.Add($"/p:PublishReadyToRun={s.Item4.HasFlag(Publish.ReadyToRun)}");
				a.Add($"/p:NativeAOT={s.Item4.HasFlag(Publish.NativeAot)}");
				a.Add($"/p:IlcDisableReflection={s.Item4.HasFlag(Publish.NoReflection)}");
				a.Add($"/p:CopyTargetTo={s.Item5}");
			},
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state);
	}
	private static async Task RestoreNet(String projectFile, String rid, NetVersion netVersion)
	{
		ExecuteState<ValueTuple<String, String, NetVersion>> state = new()
		{
			ExecutablePath = "dotnet",
			ArgState = (projectFile, rid, netVersion),
			AppendArgs = (s, a) =>
			{
				a.Add("restore");
				a.Add(s.Item1);
				a.Add("-r");
				a.Add(s.Item2);
				a.Add($"/p:USE_NET80={s.Item3 is NetVersion.Net80}");
				a.Add($"/p:USE_NET90={s.Item3 is NetVersion.Net90}");
			},
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state);
	}
	private static Boolean ArchSupported(Architecture arch)
	{
		Architecture currentArch = RuntimeInformation.OSArchitecture;
		return arch == currentArch || currentArch switch
		{
			Architecture.X86 => false,
			Architecture.Arm => false,
			Architecture.Armv6 => false,
			_ => true,
		};
	}
}