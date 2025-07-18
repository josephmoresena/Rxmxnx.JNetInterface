namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private partial class Windows
	{
		private static readonly Dictionary<JdkVersion, String> i686Url = new()
		{
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.84.0.15-ca-jdk8.0.442-win_i686.zip" },
			{ JdkVersion.Jdk11, "https://cdn.azul.com/zulu/bin/zulu11.78.15-ca-jdk11.0.26-win_i686.zip" },
			{ JdkVersion.Jdk17, "https://cdn.azul.com/zulu/bin/zulu17.56.15-ca-jdk17.0.14-win_i686.zip" },
		};
		private static readonly Dictionary<JdkVersion, String> amd64Url = new()
		{
			{
				JdkVersion.Jdk6,
				"https://www.atteya.net/site/en/downloads/java-jdk?download=46:java-jdk-6u45-windows-x64"
			},
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.84.0.15-ca-jdk8.0.442-win_x64.zip" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.26-windows-x64.zip" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.14-windows-x64.zip" },
			{ JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_windows-x64_bin.zip" },
		};
		private static readonly Dictionary<JdkVersion, String> arm64Url = new()
		{
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.26-windows-aarch64.zip" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.14-windows-aarch64.zip" },
			{ JdkVersion.Jdk21, "https://aka.ms/download-jdk/microsoft-jdk-21.0.6-windows-aarch64.zip" },
		};
		private static readonly EnumerationOptions searchOptions = new()
		{
			IgnoreInaccessible = true, RecurseSubdirectories = true, MaxRecursionDepth = 4,
		};
	}
}