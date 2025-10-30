namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private partial class Linux
	{
		private static readonly Dictionary<Architecture, (String qemuExe, String qemuRoot)> qemu = new()
		{
			{ Architecture.Arm, ("qemu-arm", "/usr/arm-linux-gnueabihf") },
			{ Architecture.Armv6, ("qemu-arm", "/usr/arm-linux-gnueabihf") },
			{ Architecture.Arm64, ("qemu-aarch64", "/usr/aarch64-linux-gnu") },
			{ Architecture.X64, ("qemu-x86_64", "/usr/x86_64-linux-gnu") },
		};
		private static readonly Dictionary<JdkVersion, String> armhfUrl = new()
		{
			{
				JdkVersion.Jdk8,
				"https://cdn.azul.com/zulu-embedded/bin/zulu8.88.0.19-ca-jdk8.0.462-linux_aarch32hf.tar.gz"
			},
			{
				JdkVersion.Jdk11,
				"https://cdn.azul.com/zulu-embedded/bin/zulu11.82.19-ca-jdk11.0.28-linux_aarch32hf.tar.gz"
			},
			{
				JdkVersion.Jdk17,
				"https://cdn.azul.com/zulu-embedded/bin/zulu17.60.17-ca-jdk17.0.16-c2-linux_aarch32hf.tar.gz"	// Update to 17.0.17
			},
		};
		private static readonly Dictionary<JdkVersion, String> amd64Url = new()
		{
			{
				JdkVersion.Jdk6,
				"https://www.atteya.net/site/en/downloads/java-jdk?download=48:java-jdk-6u45-linux-x64"
			},
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.90.0.19-ca-jdk8.0.472-linux_x64.tar.gz" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.29-linux-x64.tar.gz" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.17-linux-x64.tar.gz" },
			{ JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_linux-x64_bin.tar.gz" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_linux-x64_bin.tar.gz" },
		};
		private static readonly Dictionary<JdkVersion, String> arm64Url = new()
		{
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.90.0.19-ca-jdk8.0.472-linux_aarch64.tar.gz" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.29-linux-aarch64.tar.gz" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.17-linux-aarch64.tar.gz" },
			{ JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_linux-aarch64_bin.tar.gz" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_linux-aarch64_bin.tar.gz" },
		};
	}
}