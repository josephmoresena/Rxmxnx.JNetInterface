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
			args.Add("-r");
			args.Add(restoreArgs.RuntimeIdentifier);
			args.Add($"/p:USE_NET80={restoreArgs.Version is NetVersion.Net80}");
			args.Add($"/p:USE_NET90={restoreArgs.Version is NetVersion.Net90}");
		}
	}
}