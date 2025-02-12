namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public abstract partial class Launcher
{
	public DirectoryInfo OutputDirectory { get; set; }
	public Architecture CurrentArch { get; }

	public virtual IEnumerable<Jdk> this[Architecture arch] => [];
	public abstract String RuntimeIdentifierPrefix { get; }

	public abstract Architecture[] Architectures { get; }

	protected abstract String JavaArchiverName { get; }
	protected abstract String JavaExecutableName { get; }
	protected abstract String JavaCompilerName { get; }

	public async Task Execute()
	{
		Dictionary<String, Int32> results = new();

		try
		{
			Dictionary<Architecture, FileInfo[]> archFiles = this.Architectures.ToDictionary(
				a => a,
				a => this.OutputDirectory.GetFiles(
					$"ApplicationTest.*.{this.RuntimeIdentifierPrefix}-{Enum.GetName(a)!.ToLower()}.*"));
			FileInfo? jarFile = this.OutputDirectory.GetFiles("HelloJni.jar").FirstOrDefault();

			foreach (Jdk jdk in this.Architectures.SelectMany(a => this[a]).ToHashSet())
			{
				if (jarFile is not null)
					foreach (NetVersion netVersion in Enum.GetValues<NetVersion>())
						await this.RunJarFile(jdk, jarFile, netVersion, results);

				foreach (FileInfo appFile in archFiles[jdk.JavaArchitecture])
				{
					String executionName =
						$"{Path.GetRelativePath(this.OutputDirectory.FullName, appFile.FullName)} ({jdk.JavaVersion})";
					results.Add(executionName, await this.RunAppFile(appFile, jdk, executionName));
				}
			}
		}
		finally
		{
			if (results.Count > 0)
				ConsoleNotifier.Results(results);
		}
	}

	public abstract Jdk GetMinJdk();

	protected abstract String GetJavaLibraryName(JdkVersion version);
	protected abstract Task<Jdk?> DownloadJdk(JdkVersion version, Architecture arch);

	protected virtual async Task AppendJdk(IDictionary<JdkVersion, Jdk> jdks, JdkVersion version, Architecture arch)
	{
		ConsoleNotifier.PlatformNotifier.JdkDetection(version, arch);
		if (await this.GetJdk(version, arch) is { } jdk)
			jdks.TryAdd(version, jdk);
		else
			ConsoleNotifier.PlatformNotifier.JdkUnavailable(version, arch);
	}
	protected virtual async Task<Int32> RunAppFile(FileInfo appFile, Jdk jdk, String executionName)
	{
		ExecuteState<AppArgs> state = new()
		{
			ExecutablePath = appFile.FullName,
			ArgState = jdk,
			AppendArgs = AppArgs.Append,
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		Int32 result = await Utilities.Execute(state, ConsoleNotifier.CancellationToken);
		ConsoleNotifier.Notifier.Result(result, executionName);
		return result;
	}
	protected virtual async Task<Int32> RunJarFile(JarArgs jarArgs, Jdk jdk)
	{
		ExecuteState<JarArgs> state = new()
		{
			ExecutablePath = jdk.JavaExecutable,
			ArgState = jarArgs,
			AppendArgs = JarArgs.Append,
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		return await Utilities.Execute(state, ConsoleNotifier.CancellationToken);
	}
}