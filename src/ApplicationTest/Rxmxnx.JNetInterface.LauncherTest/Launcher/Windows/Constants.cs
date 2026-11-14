namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
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
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.98.0.107-ca-jdk8.0.512-win_i686.zip" },
			{ JdkVersion.Jdk11, "https://cdn.azul.com/zulu/bin/zulu11.92.109-ca-jdk11.0.33-win_i686.zip" },
			{ JdkVersion.Jdk17, "https://cdn.azul.com/zulu/bin/zulu17.70.113-ca-jdk17.0.21.0.3-win_i686.zip" },
		};
		private static readonly Dictionary<JdkVersion, String> amd64Url = new()
		{
			{
				JdkVersion.Jdk6,
				"https://www.atteya.net/site/en/downloads/java-jdk?download=46:java-jdk-6u45-windows-x64"
			},
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.98.0.107-ca-jdk8.0.512-win_x64.zip" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11-windows-x64.zip" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17-windows-x64.zip" },
			{ JdkVersion.Jdk21, "https://aka.ms/download-jdk/microsoft-jdk-21-windows-x64.zip" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_windows-x64_bin.zip" },
		};
		private static readonly Dictionary<JdkVersion, String> arm64Url = new()
		{
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11-windows-aarch64.zip" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17-windows-aarch64.zip" },
			{ JdkVersion.Jdk21, "https://aka.ms/download-jdk/microsoft-jdk-21-windows-aarch64.zip" },
			{ JdkVersion.Jdk25, "https://aka.ms/download-jdk/microsoft-jdk-25-windows-aarch64.zip" },
		};
		private static readonly EnumerationOptions searchOptions = new()
		{
			IgnoreInaccessible = true, RecurseSubdirectories = true, MaxRecursionDepth = 4,
		};
	}
}