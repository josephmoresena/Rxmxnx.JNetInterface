namespace Rxmxnx.JNetInterface.ApplicationTest;

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
		Dictionary<Architecture, FileInfo[]> archFiles = this.Architectures.ToDictionary(
			a => a,
			a => this.OutputDirectory.GetFiles(
				$"ApplicationTest.*.{this.RuntimeIdentifierPrefix}-{Enum.GetName(a)!.ToLower()}.*"));
		FileInfo? jarFile = this.OutputDirectory.GetFiles("HelloJni.jar").FirstOrDefault();
		foreach (Jdk jdk in this.Architectures.SelectMany(a => this[a]))
		{
			if (jarFile is not null)
				foreach (NetVersion netVersion in Enum.GetValues<NetVersion>())
					await this.RunJarFile(jdk, jarFile, netVersion);

			foreach (FileInfo appFile in archFiles[jdk.JavaArchitecture])
				await this.RunAppFile(appFile, jdk);
		}
	}

	public abstract Jdk GetMinJdk();

	protected abstract String GetJavaLibraryName(JdkVersion version);
	protected abstract Task<Jdk> DownloadJdk(JdkVersion version, Architecture arch);

	protected virtual async Task AppendJdk(IDictionary<JdkVersion, Jdk> jdks, JdkVersion version, Architecture arch)
	{
		ConsoleNotifier.PlatformNotifier.JdkDetection(version, arch);
		if (await this.GetJdk(version, arch) is { } jdk)
			jdks.TryAdd(version, jdk);
		else
			ConsoleNotifier.PlatformNotifier.JdkUnavailable(version, arch);
	}
	protected virtual async Task RunAppFile(FileInfo appFile, Jdk jdk)
	{
		ExecuteState<AppArgs> state = new()
		{
			ExecutablePath = appFile.FullName,
			ArgState = jdk,
			AppendArgs = AppArgs.Append,
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		Int32 result = await Utilities.Execute(state);
		ConsoleNotifier.Notifier.Result(result, appFile.Name);
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
		return await Utilities.Execute(state);
	}
}