using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private sealed partial class Mac : Launcher
	{
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

		public override Jdk GetMinJdk() => this._amd64[Jdk.JdkVersion.Jdk6];

		protected override String GetJavaLibraryName(Jdk.JdkVersion version)
			=> version is Jdk.JdkVersion.Jdk6 ? "libserver.dylib" : "libjvm.dylib";
		public override Task Execute() => throw new NotImplementedException();

		protected override async Task<Jdk> DownloadJdk(Jdk.JdkVersion version, Architecture arch)
		{
			String jdkPath = $"jdk_{arch}_{version}";
			if (this.GetJdk(version, arch, jdkPath) is { } result) return result;
			IReadOnlyDictionary<Jdk.JdkVersion, String> urls = arch is Architecture.X64 ? Mac.amd64Url : Mac.arm64Url;
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
					await Mac.ExtractFromDmgPkgAsync(tempFileName, jdkPath);
			}
			finally
			{
				File.Delete(tempFileName);
			}
			result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory)!;
			ConsoleNotifier.PlatformNotifier.JdkDownload(version, arch, jdkDirectory.FullName);
			return result;
		}

		public static async Task<Launcher> CreateInstance(DirectoryInfo publishDirectory)
		{
			Mac result = new(publishDirectory, out Task initialize);
			await initialize;
			ConsoleNotifier.PlatformNotifier.Initialization(result.Platform, result.CurrentArch);
			return result;
		}
	}
}