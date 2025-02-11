using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private sealed partial class Windows : Launcher
	{
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
			=> this.CurrentArch is Architecture.X86 ?
				this._i686[Jdk.JdkVersion.Jdk6] :
				this._amd64[Jdk.JdkVersion.Jdk6];

		protected override String GetJavaLibraryName(Jdk.JdkVersion version) => "jvm.dll";
		protected override async Task<Jdk> DownloadJdk(Jdk.JdkVersion version, Architecture arch)
		{
			String jdkPath = $"jdk_{arch}_{version}";
			if (this.GetJdk(version, arch, jdkPath) is { } result) return result;

			String? installationPath = Windows.GetInstallationPath(arch, version);

			if (!String.IsNullOrEmpty(installationPath) && this.GetJdk(version, arch, jdkPath) is { } installed)
				return installed;

			IReadOnlyDictionary<Jdk.JdkVersion, String> urls = arch is Architecture.X64 ? Windows.amd64Url :
				arch is Architecture.X86 ? Windows.i686Url : Windows.arm64Url;
			String tempFileName = Path.GetTempFileName();
			try
			{
				Directory.CreateDirectory(jdkPath);
				await Utilities.DownloadFileAsync(new()
				{
					Url = urls[version],
					Destination = tempFileName,
					Notifier = ConsoleNotifier.Notifier,
				});
				if (version is not Jdk.JdkVersion.Jdk6)
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

			result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory)!;
			ConsoleNotifier.PlatformNotifier.JdkDownload(version, arch, jdkDirectory.FullName);
			return result;
		}
		protected override Task AppendJdk(IDictionary<Jdk.JdkVersion, Jdk> jdks, Jdk.JdkVersion version,
			Architecture arch)
		{
			if (arch == Architecture.Arm64 && version is Jdk.JdkVersion.Jdk8) return Task.CompletedTask;
			return base.AppendJdk(jdks, version, arch);
		}

		public static async Task<Launcher> CreateInstance(DirectoryInfo publishDirectory)
		{
			Windows result = new(publishDirectory, out Task initialize);
			await initialize;
			ConsoleNotifier.PlatformNotifier.Initialization(result.Platform, result.CurrentArch);
			return result;
		}
	}
}