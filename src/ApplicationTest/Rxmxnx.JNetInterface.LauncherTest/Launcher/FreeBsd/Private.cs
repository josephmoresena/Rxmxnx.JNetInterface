namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private sealed partial class FreeBsd
	{
		private static readonly SemaphoreSlim pkgSemaphore = new(1, 1);
		private static readonly Architecture[] currentArch = [RuntimeInformation.OSArchitecture,];
		private static readonly NetVersion[] netVersions =
		[
#if NET10_0_OR_GREATER
			NetVersion.Net100,
#elif NET9_0_OR_GREATER
			NetVersion.Net90,
#elif NET8_0_OR_GREATER
			NetVersion.Net80,
#elif NET7_0_OR_GREATER
			NetVersion.Net70,
#endif
		];

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