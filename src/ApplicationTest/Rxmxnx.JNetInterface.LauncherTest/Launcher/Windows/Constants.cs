namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private partial class Windows
	{
		private static readonly Dictionary<JdkVersion, String> i686Url = new()
		{
			{
				JdkVersion.Jdk6,
				"https://www.atteya.net/site/en/downloads/java-jdk?download=47:java-jdk-6u45-windows-i586"
			},
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.90.0.19-ca-jdk8.0.472-win_i686.zip" },
			{ JdkVersion.Jdk11, "https://cdn.azul.com/zulu/bin/zulu11.84.17-ca-jdk11.0.29-win_i686.zip" },
			{ JdkVersion.Jdk17, "https://cdn.azul.com/zulu/bin/zulu17.62.17-ca-jdk17.0.17-win_i686.zip" },
		};
		private static readonly Dictionary<JdkVersion, String> amd64Url = new()
		{
			{
				JdkVersion.Jdk6,
				"https://www.atteya.net/site/en/downloads/java-jdk?download=46:java-jdk-6u45-windows-x64"
			},
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.90.0.19-ca-jdk8.0.472-win_x64.zip" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.29-windows-x64.zip" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.17-windows-x64.zip" },
			{ JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_windows-x64_bin.zip" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_windows-x64_bin.zip" },
		};
		private static readonly Dictionary<JdkVersion, String> arm64Url = new()
		{
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.29-windows-aarch64.zip" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.16-windows-aarch64.zip" },
			{
				JdkVersion.Jdk21, "https://aka.ms/download-jdk/microsoft-jdk-21.0.8-windows-aarch64.zip"
			}, // Update to 21.0.9
			{
				JdkVersion.Jdk25, "https://cdn.azul.com/zulu/bin/zulu25.30.17-ca-jdk25.0.1-win_aarch64.zip"
			}, // Update to MS Version
		};
		private static readonly EnumerationOptions searchOptions = new()
		{
			IgnoreInaccessible = true, RecurseSubdirectories = true, MaxRecursionDepth = 4,
		};
	}
}