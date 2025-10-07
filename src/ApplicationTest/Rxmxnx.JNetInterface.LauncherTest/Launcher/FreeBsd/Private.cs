namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private sealed partial class FreeBsd
	{
		private static readonly Architecture[] currentArch = [RuntimeInformation.OSArchitecture,];

		private readonly ConcurrentDictionary<JdkVersion, Jdk> _jdks = new();
		private String _runtimeIdentifier = "freebsd";

		private FreeBsd(DirectoryInfo outputDirectory, out Task initClass) : base(outputDirectory)
			=> initClass = this.Initialize();

		private async Task Initialize()
		{
			ExecuteState state = new()
			{
				ExecutablePath = "freebsd-version",
				AppendArgs = a => a.Add("-r"),
				Notifier = ConsoleNotifier.Notifier,
			};
			String freeBsdVersion = await Utilities.ExecuteWithOutput(state, ConsoleNotifier.CancellationToken);
			this._runtimeIdentifier += $".{freeBsdVersion[..2]}";

			List<Task> tasks = [];
			foreach (JdkVersion version in Enum.GetValues<JdkVersion>().AsSpan())
				tasks.Add(this.AppendJdk(this._jdks, version, this.CurrentArch));
			await Task.WhenAll(tasks);
		}
	}
}