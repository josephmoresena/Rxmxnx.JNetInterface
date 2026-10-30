namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
public partial class Launcher
{
	private partial class Mac
	{
		private static readonly Dictionary<JdkVersion, String> amd64Url = new()
		{
			{
				JdkVersion.Jdk6,
				"https://updates.cdn-apple.com/2019/cert/041-88384-20191011-3d8da658-dca4-4a5b-b67c-26e686876403/JavaForOSX.dmg"
			},
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.98.0.107-ca-jdk8.0.512-macosx_x64.tar.gz" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11-macOS-x64.tar.gz" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17-macOS-x64.tar.gz" },
			{ JdkVersion.Jdk21, "https://aka.ms/download-jdk/microsoft-jdk-21-macOS-x64.tar.gz" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_macos-x64_bin.tar.gz" },
		};
		private static readonly Dictionary<JdkVersion, String> arm64Url = new()
		{
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.98.0.107-ca-jdk8.0.512-macosx_aarch64.tar.gz" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11-macOS-aarch64.tar.gz" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17-macOS-aarch64.tar.gz" },
			{ JdkVersion.Jdk21, "https://aka.ms/download-jdk/microsoft-jdk-21-macOS-aarch64.tar.gz" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_macos-aarch64_bin.tar.gz" },
		};
	}
}