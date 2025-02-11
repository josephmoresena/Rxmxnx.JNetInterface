namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private struct CompileNetArgs(RestoreNetArgs args, String outputPath)
	{
		private readonly RestoreNetArgs _args = args;
		private readonly String _outputPath = outputPath;

		public Publish Publish { get; set; }
		public Boolean BuildDependencies { get; set; }

		public static void Append(CompileNetArgs compileArgs, Collection<String> args)
		{
			args.Add("publish");
			args.Add(compileArgs._args.ProjectFile);
			args.Add("-c");
			args.Add("Release");
			args.Add("-r");
			args.Add(compileArgs._args.Rid);
			args.Add("/p:RestorePackages=false");
			args.Add($"/p:BuildProjectReferences={compileArgs.BuildDependencies}");
			args.Add($"/p:USE_NET80={compileArgs._args.Version is NetVersion.Net80}");
			args.Add($"/p:USE_NET90={compileArgs._args.Version is NetVersion.Net90}");
			args.Add($"/p:JNI_LIBRARY={compileArgs.Publish.HasFlag(Publish.JniLibrary)}");
			args.Add($"/p:PublishReadyToRun={compileArgs.Publish.HasFlag(Publish.ReadyToRun)}");
			args.Add($"/p:NativeAOT={compileArgs.Publish.HasFlag(Publish.NativeAot)}");
			args.Add($"/p:IlcDisableReflection={compileArgs.Publish.HasFlag(Publish.NoReflection)}");
			args.Add($"/p:CopyTargetTo={compileArgs._outputPath}");
		}
	}
}