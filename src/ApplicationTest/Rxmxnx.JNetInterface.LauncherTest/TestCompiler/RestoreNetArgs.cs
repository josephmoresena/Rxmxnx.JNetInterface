namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private readonly struct RestoreNetArgs
	{
		public String ProjectFile { get; init; }
		public String RuntimeIdentifier { get; init; }
		public NetVersion Version { get; init; }

		public static void Append(RestoreNetArgs restoreArgs, Collection<String> args)
		{
			args.Add("restore");
			args.Add(restoreArgs.ProjectFile);
			if (!String.IsNullOrEmpty(restoreArgs.RuntimeIdentifier))
			{
				args.Add("-r");
				args.Add(restoreArgs.RuntimeIdentifier);
			}
			args.Add($"/p:RequiredFramework={restoreArgs.Version.GetTargetFramework()}");
			args.Add($"/p:TargetFramework={restoreArgs.Version.GetTargetFramework()}");
		}
		public static void AppendList(RestoreNetArgs restoreArgs, Collection<String> args)
		{
			args.Add("list");
			args.Add(restoreArgs.ProjectFile);
			args.Add("package");
			args.Add("--include-transitive");
		}
		public static void AppendTest(RestoreNetArgs restoreArgs, Collection<String> args)
		{
			args.Add("test");
			args.Add(restoreArgs.ProjectFile);
			args.Add($"/p:RequiredFramework={restoreArgs.Version.GetTargetFramework()}");
			args.Add($"/p:TargetFramework={restoreArgs.Version.GetTargetFramework()}");
			args.Add("--logger");
			args.Add("\"console;verbosity=detailed\"");
		}
	}
}