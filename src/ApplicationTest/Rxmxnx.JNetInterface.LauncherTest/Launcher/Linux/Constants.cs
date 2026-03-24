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
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.92.0.21-ca-jdk8.0.482-linux_aarch32hf.tar.gz" },
			{ JdkVersion.Jdk11, "https://cdn.azul.com/zulu/bin/zulu11.86.21-ca-jdk11.0.30-linux_aarch32hf.tar.gz" },
			{
				JdkVersion.Jdk17,
				"https://cdn.azul.com/zulu/bin/zulu17.64.17-ca-jdk17.0.18-c2-linux_aarch32hf.tar.gz"
			},
		};
		private static readonly Dictionary<JdkVersion, String> amd64Url = new()
		{
			{
				JdkVersion.Jdk6, "https://www.atteya.net/site/en/downloads/java-jdk?download=48:java-jdk-6u45-linux-x64"
			},
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.92.0.21-ca-jdk8.0.482-linux_x64.tar.gz" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.30-linux-x64.tar.gz" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.18-linux-x64.tar.gz" },
			{ JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_linux-x64_bin.tar.gz" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_linux-x64_bin.tar.gz" },
		};
		private static readonly Dictionary<JdkVersion, String> arm64Url = new()
		{
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.92.0.21-ca-jdk8.0.482-linux_aarch64.tar.gz" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.30-linux-aarch64.tar.gz" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.18-linux-aarch64.tar.gz" },
			{ JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_linux-aarch64_bin.tar.gz" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_linux-aarch64_bin.tar.gz" },
		};
	}
}