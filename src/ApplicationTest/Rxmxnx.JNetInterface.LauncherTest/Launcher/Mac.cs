namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private sealed partial class Mac : Launcher, ILauncher<Mac>
	{
		public static OSPlatform Platform => OSPlatform.OSX;
		public override Architecture[] Architectures { get; }
		public override String RuntimeIdentifierPrefix => "osx";
		public override IEnumerable<Jdk> this[Architecture arch]
			=> arch switch
			{
				Architecture.X64 => this._amd64.Values,
				Architecture.Arm64 => this._arm64.Values,
				_ => base[arch],
			};

		protected override String JavaArchiverName => "jar";
		protected override String JavaExecutableName => "java";
		protected override String JavaCompilerName => "javac";

		public override Jdk GetMinJdk()
			=> this.CurrentArch is Architecture.X64 ? this._amd64[JdkVersion.Jdk6] : this._arm64[JdkVersion.Jdk8];

		protected override String GetJavaLibraryName(JdkVersion version)
			=> version is JdkVersion.Jdk6 ? "libserver.dylib" : "libjvm.dylib";

		protected override async Task<Jdk?> DownloadJdk(JdkVersion version, Architecture arch)
		{
			if (this.CurrentArch is Architecture.Arm64 && version is JdkVersion.Jdk6) return default;

			String jdkPath = $"jdk_{arch}_{version}";
			if (this.GetJdk(version, arch, jdkPath) is { } result) return result;
			IReadOnlyDictionary<JdkVersion, String> urls = arch is Architecture.X64 ? Mac.amd64Url : Mac.arm64Url;
			String tempFileName = Path.GetTempFileName();
			try
			{
				if (!urls.ContainsKey(version)) return default;

				Directory.CreateDirectory(jdkPath);
				await Utilities.DownloadFileAsync(
					new() { Url = urls[version], Destination = tempFileName, Notifier = ConsoleNotifier.Notifier, },
					ConsoleNotifier.CancellationToken);
				if (version is not JdkVersion.Jdk6)
					await Launcher.ExtractTarGz(tempFileName, jdkPath, ConsoleNotifier.CancellationToken);
				else
					await Mac.ExtractFromDmgPkgAsync(tempFileName, jdkPath, ConsoleNotifier.CancellationToken);
			}
			finally
			{
				File.Delete(tempFileName);
			}

			result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory);
			ConsoleNotifier.PlatformNotifier.JdkDownload(version, arch, jdkDirectory.FullName);
			return result;
		}
		protected override async Task<Int32> RunJarFile(JarArgs jarArgs, Jdk jdk)
		{
			using CancellationTokenSource source = new(TimeSpan.FromMinutes(5));
			CancellationTokenRegistration registry = ConsoleNotifier.RegisterCancellation(source);
			try
			{
				ExecuteState<JarArgs> state = new()
				{
					ExecutablePath = jdk.JavaExecutable,
					ArgState = jarArgs,
					AppendArgs = JarArgs.Append,
					WorkingDirectory = this.OutputDirectory.FullName,
					Notifier = ConsoleNotifier.Notifier,
				};
				return await Utilities.Execute(state, source.Token);
			}
			catch (OperationCanceledException)
			{
				return -1;
			}
			finally
			{
				registry.Unregister();
			}
		}
		protected override async Task<Int32> RunAppFile(FileInfo appFile, Jdk jdk, String executionName)
		{
			using CancellationTokenSource source = new(TimeSpan.FromMinutes(5));
			CancellationTokenRegistration registry = ConsoleNotifier.RegisterCancellation(source);
			Int32 result = -1;
			try
			{
				ExecuteState<AppArgs> state = new()
				{
					ExecutablePath = appFile.FullName,
					ArgState = jdk,
					AppendArgs = AppArgs.Append,
					WorkingDirectory = this.OutputDirectory.FullName,
					Notifier = ConsoleNotifier.Notifier,
				};
				result = await Utilities.Execute(state, source.Token);
			}
			catch (OperationCanceledException)
			{
				result = -1;
			}
			finally
			{
				registry.Unregister();
				ConsoleNotifier.Notifier.Result(result, executionName);
			}
			return result;
		}

		public static Mac Create(DirectoryInfo outputDirectory, out Task initTask)
			=> new(outputDirectory, out initTask);
	}
}