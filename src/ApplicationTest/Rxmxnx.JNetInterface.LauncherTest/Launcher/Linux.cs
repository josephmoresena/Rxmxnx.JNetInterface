using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private sealed partial class Linux : Launcher
	{
		public override Architecture[] Architectures { get; }
		public override String RuntimeIdentifierPrefix => "win";
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
		protected override String GetJavaLibraryName(Jdk.JdkVersion version) => "libjvm.so";

		public override Jdk GetMinJdk()
			=> this.CurrentArch is Architecture.X64 ? this._amd64[Jdk.JdkVersion.Jdk6] :
				this._isArmHf ? this._armhf[Jdk.JdkVersion.Jdk8] : this._arm64[Jdk.JdkVersion.Jdk8];
		protected override async Task<Jdk> DownloadJdk(Jdk.JdkVersion version, Architecture arch)
		{
			String jdkPath = $"jdk_{arch}_{version}";
			if (this.GetJdk(version, arch, jdkPath) is { } result) return result;

			IReadOnlyDictionary<Jdk.JdkVersion, String> urls = arch is Architecture.X64 ? Linux.amd64Url :
				Linux.IsArmHf(arch) ? Linux.armhfUrl : Linux.arm64Url;
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
					await Launcher.ExtractTarGz(tempFileName, jdkPath);
				else
					await this.SelfExtractBinary(tempFileName, jdkPath);
			}
			finally
			{
				File.Delete(tempFileName);
			}

			result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory)!;
			ConsoleNotifier.PlatformNotifier.JdkDownload(version, arch, jdkDirectory.FullName);
			return result;
		}
		protected override String? GetQemu(Architecture arch, out String qemuRoot)
		{
			if (this.IsCurrentArch(arch) || (this.CurrentArch is Architecture.Arm64 && Linux.IsArmHf(arch)))
				return base.GetQemu(arch, out qemuRoot);
			(String qemuExe, qemuRoot) = Linux.qemu[arch];
			return qemuExe;
		}

		public static async Task<Launcher> CreateInstance(DirectoryInfo publishDirectory)
		{
			Linux result = new(publishDirectory, out Task initialize);
			await initialize;
			ConsoleNotifier.PlatformNotifier.Initialization(result.Platform, result.CurrentArch);
			return result;
		}
	}
}