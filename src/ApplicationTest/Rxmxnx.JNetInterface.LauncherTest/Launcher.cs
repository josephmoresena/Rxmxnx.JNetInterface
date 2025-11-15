namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	public DirectoryInfo OutputDirectory { get; }
	public Architecture CurrentArch { get; }

	public virtual NetVersion[] NetVersions => Enum.GetValues<NetVersion>();
	public virtual IEnumerable<Jdk> this[Architecture arch] => [];
	public abstract String RuntimeIdentifierPrefix { get; }

	public abstract Architecture[] Architectures { get; }

	protected abstract String JavaArchiverName { get; }
	protected abstract String JavaExecutableName { get; }
	protected abstract String JavaCompilerName { get; }

	public async Task Execute(String? pattern = default)
	{
		ConsoleNotifier.ShowDiskUsage();

		Dictionary<String, Int32> results = new();
		List<Task> executionTasks = [];
		try
		{
			String exeExtension = SystemInfo.IsWindows ? ".exe" : "";
			Dictionary<Architecture, FileInfo[]> archFiles = this.Architectures.ToDictionary(
				a => a,
				a => this.OutputDirectory
				         .GetFiles(
					         $"ApplicationTest.*.{this.RuntimeIdentifierPrefix}-{Enum.GetName(a)!.ToLower()}.net*.0{pattern}{exeExtension}")
				         .OrderBy(f => NetVersionParser.GetNetVersion(f.FullName)).ToArray());
			Jdk[] jdks = this.Architectures.SelectMany(a => this[a]).Distinct()
			                 .OrderBy(j => (j.JavaVersion, j.JavaArchitecture == this.CurrentArch, j.JavaArchitecture))
			                 .ToArray();
			foreach (Jdk jdk in jdks)
			{
				foreach (FileInfo appFile in archFiles[jdk.JavaArchitecture])
				{
					String executionName =
						$"{Path.GetRelativePath(this.OutputDirectory.FullName, appFile.FullName)} ({jdk.JavaVersion})";
					Task executionTask = AppendResult(executionName, this.RunAppFile(appFile, jdk, executionName));
					await Task.WhenAny(Task.Delay(20000, ConsoleNotifier.CancellationToken), executionTask)
					          .ConfigureAwait(false);
					if (!executionTask.IsCompleted)
						executionTasks.Add(executionTask);
				}
			}
		}
		finally
		{
			if (results.Count > 0)
				ConsoleNotifier.Results(results);
		}
		if (executionTasks.Any(t => !t.IsCompleted))
		{
			Console.WriteLine($"Waiting for pending tasks ({executionTasks.Count})...");
			await Task.WhenAny(Task.Delay(40000, ConsoleNotifier.CancellationToken), Task.WhenAll(executionTasks));
		}
		if (executionTasks.Any(t => !t.IsCompleted))
			Environment.Exit(0);
		return;
		async Task AppendResult(String executionName, Task<Int32> resultTask)
			=> results.Add(executionName, await resultTask.ConfigureAwait(false));
	}
	public async Task ExecuteJar(NetVersion[] netVersions)
	{
		ConsoleNotifier.ShowDiskUsage();

		Dictionary<String, Int32> results = new();
		try
		{
			FileInfo? jarFile = this.OutputDirectory.GetFiles("HelloJni.jar").FirstOrDefault();
			if (jarFile is null) return;

			Jdk[] jdks = this.Architectures.SelectMany(a => this[a]).Distinct()
			                 .OrderBy(j => (j.JavaVersion, j.JavaArchitecture == this.CurrentArch, j.JavaArchitecture))
			                 .ToArray();
			foreach (Jdk jdk in jdks)
			{
				foreach (NetVersion netVersion in netVersions)
					await this.RunJarFile(jdk, jarFile, netVersion, results);
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
	protected virtual async Task<Int32> RunAppFile(FileInfo appFile, Jdk jdk, String executionName,
		CancellationToken cancellationToken)
	{
		ExecuteState<AppArgs> state = new()
		{
			ExecutablePath = appFile.FullName,
			ArgState = jdk,
			AppendArgs = AppArgs.Append,
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		Int32 result = await Utilities.Execute(state, cancellationToken);
		ConsoleNotifier.Notifier.Result(result, executionName);
		if (Utilities.ShowDiagnostics)
			ConsoleNotifier.ShowDiskUsage();
		return result;
	}
	protected virtual async Task<Int32> RunJarFile(JarArgs jarArgs, Jdk jdk, CancellationToken cancellationToken)
	{
		ExecuteState<JarArgs> state = new()
		{
			ExecutablePath = jdk.JavaExecutable,
			ArgState = jarArgs,
			AppendArgs = JarArgs.Append,
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		Int32 result = await Utilities.Execute(state, cancellationToken);
		if (Utilities.ShowDiagnostics)
			ConsoleNotifier.ShowDiskUsage();
		return result;
	}
}