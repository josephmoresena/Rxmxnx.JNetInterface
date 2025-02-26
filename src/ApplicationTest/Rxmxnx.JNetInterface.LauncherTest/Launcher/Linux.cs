namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private sealed partial class Linux : Launcher, ILauncher<Linux>
	{
		public static OSPlatform Platform => OSPlatform.Linux;
		public override Architecture[] Architectures { get; }
		public override String RuntimeIdentifierPrefix => "linux";
		public override IEnumerable<Jdk> this[Architecture arch]
			=> arch switch
			{
				Architecture.Arm => this._armhf.Values,
				Architecture.Armv6 => this._armhf.Values,
				Architecture.X64 => this._amd64.Values,
				Architecture.Arm64 => this._arm64.Values,
				_ => base[arch],
			};

		protected override String JavaArchiverName => "jar";
		protected override String JavaExecutableName => "java";
		protected override String JavaCompilerName => "javac";
		protected override String GetJavaLibraryName(JdkVersion version) => "libjvm.so";

		public override Jdk GetMinJdk()
			=> this.CurrentArch is Architecture.X64 ? this._amd64[JdkVersion.Jdk6] :
				this._isArmHf ? this._armhf[JdkVersion.Jdk8] : this._arm64[JdkVersion.Jdk8];

		protected override async Task<Jdk?> DownloadJdk(JdkVersion version, Architecture arch)
		{
			String jdkPath = $"jdk_{arch}_{version}";
			if (this.GetJdk(version, arch, jdkPath) is { } result) return result;

			IReadOnlyDictionary<JdkVersion, String> urls = arch is Architecture.X64 ? Linux.amd64Url :
				Linux.IsArmHf(arch) ? Linux.armhfUrl : Linux.arm64Url;
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
					await this.SelfExtractBinary(tempFileName, jdkPath);
			}
			finally
			{
				File.Delete(tempFileName);
			}

			result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory);
			ConsoleNotifier.PlatformNotifier.JdkDownload(version, arch, jdkDirectory.FullName);
			return result;
		}
		protected override Task<Int32> RunAppFile(FileInfo appFile, Jdk jdk, String executionName,
			CancellationToken cancellationToken)
		{
			Architecture arch = jdk.JavaArchitecture;
			if (this.IsCurrentArch(arch) || (this.CurrentArch is Architecture.Arm64 && Linux.IsArmHf(arch)))
				return base.RunAppFile(appFile, jdk, executionName, cancellationToken);
			return this.RunAppQemu(appFile, jdk, executionName, cancellationToken);
		}
		protected override Task<Int32> RunJarFile(JarArgs jarArgs, Jdk jdk, CancellationToken cancellationToken)
		{
			Architecture arch = jdk.JavaArchitecture;
			if (this.IsCurrentArch(arch) || (this.CurrentArch is Architecture.Arm64 && Linux.IsArmHf(arch)))
				return base.RunJarFile(jarArgs, jdk, cancellationToken);
			return this.RunJarQemu(jarArgs, jdk, cancellationToken);
		}
		public static Linux Create(DirectoryInfo outputDirectory, out Task initTask)
			=> new(outputDirectory, out initTask);
	}
}