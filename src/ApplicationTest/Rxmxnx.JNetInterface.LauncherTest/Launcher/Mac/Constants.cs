namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private partial class Mac
	{
		private static readonly Dictionary<Jdk.JdkVersion, String> amd64Url = new()
		{
			{
				Jdk.JdkVersion.Jdk6,
				"https://updates.cdn-apple.com/2019/cert/041-88384-20191011-3d8da658-dca4-4a5b-b67c-26e686876403/JavaForOSX.dmg"
			},
			{ Jdk.JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.84.0.15-ca-jdk8.0.442-macosx_x64.tar.gz" },
			{ Jdk.JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.26-macos-x64.tar.gz" },
			{ Jdk.JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.14-macos-x64.tar.gz" },
			{ Jdk.JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_macos-x64_bin.tar.gz" },
		};
		private static readonly Dictionary<Jdk.JdkVersion, String> arm64Url = new()
		{
			{
				Jdk.JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.84.0.15-ca-jdk8.0.442-macosx_aarch64.tar.gz"
			},
			{ Jdk.JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.26-macos-aarch64.tar.gz" },
			{ Jdk.JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.14-macos-aarch64.tar.gz" },
			{ Jdk.JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_macos-aarch64_bin.tar.gz" },
		};
	}
}