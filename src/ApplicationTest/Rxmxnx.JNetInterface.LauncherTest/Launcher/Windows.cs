namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private sealed partial class Windows : Launcher, ILauncher<Windows>
	{
		public static OSPlatform Platform => OSPlatform.Windows;
		public override Architecture[] Architectures { get; }
		public override String RuntimeIdentifierPrefix => "win";
		public override IEnumerable<Jdk> this[Architecture arch]
			=> arch switch
			{
				Architecture.X86 => this._i686.Values,
				Architecture.X64 => this._amd64.Values,
				Architecture.Arm64 => this._arm64.Values,
				_ => base[arch],
			};

		protected override String JavaArchiverName => "jar.exe";
		protected override String JavaExecutableName => "java.exe";
		protected override String JavaCompilerName => "javac.exe";

		public override Jdk GetMinJdk()
			=> this.CurrentArch is Architecture.X86 ? this._i686[JdkVersion.Jdk6] : this._amd64[JdkVersion.Jdk6];

		protected override String GetJavaLibraryName(JdkVersion version) => "jvm.dll";
		protected override async Task<Jdk?> DownloadJdk(JdkVersion version, Architecture arch)
		{
			String jdkPath = $"jdk_{arch}_{version}";
			if (this.GetJdk(version, arch, jdkPath) is { } result) return result;

			String? installationPath = Windows.GetInstallationPath(arch, version);

			if (!String.IsNullOrEmpty(installationPath) &&
			    this.GetJdk(version, arch, installationPath) is { } installed)
				return installed;

			IReadOnlyDictionary<JdkVersion, String> urls = arch is Architecture.X64 ? Windows.amd64Url :
				arch is Architecture.X86 ? Windows.i686Url : Windows.arm64Url;
			String tempFileName = Path.GetTempFileName();
			try
			{
				if (!urls.ContainsKey(version)) return default;

				Directory.CreateDirectory(jdkPath);
				await Utilities.DownloadFileAsync(
					new() { Url = urls[version], Destination = tempFileName, Notifier = ConsoleNotifier.Notifier, },
					ConsoleNotifier.CancellationToken);
				if (version is not JdkVersion.Jdk6)
				{
					Launcher.ExtractZip(tempFileName, jdkPath);
				}
				else
				{
					String tempExeFileName = tempFileName + ".exe";
					File.Move(tempFileName, tempExeFileName);
					tempFileName = tempExeFileName;
					await Windows.InstallFromExeAsync(tempExeFileName);
					jdkPath = Windows.GetInstallationPath(arch, version)!;
				}
			}
			finally
			{
				File.Delete(tempFileName);
			}

			result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory);
			ConsoleNotifier.PlatformNotifier.JdkDownload(version, arch, jdkDirectory.FullName);
			return result;
		}
		protected override Task AppendJdk(IDictionary<JdkVersion, Jdk> jdks, JdkVersion version, Architecture arch)
		{
			if ((arch == Architecture.Arm64 && version is JdkVersion.Jdk8) ||
			    (this.CurrentArch is not Architecture.X86 && arch == Architecture.X86 && version is JdkVersion.Jdk6))
				return Task.CompletedTask;
			return base.AppendJdk(jdks, version, arch);
		}
		public static Windows Create(DirectoryInfo outputDirectory, out Task initTask)
			=> new(outputDirectory, out initTask);
	}
}