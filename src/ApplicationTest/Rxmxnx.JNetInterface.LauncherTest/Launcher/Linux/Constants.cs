namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
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
				JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.96.0.205-ca-jdk8.0.504-linux_aarch32hf.tar.gz"
			},
			{ JdkVersion.Jdk11, "https://cdn.azul.com/zulu/bin/zulu11.90.205-ca-jdk11.0.32.1-linux_aarch32hf.tar.gz" },
			{
				JdkVersion.Jdk17,
				"https://cdn.azul.com/zulu/bin/zulu17.68.203-ca-jdk17.0.20.1-c2-linux_aarch32hf.tar.gz"
			},
		};
		private static readonly Dictionary<JdkVersion, String> amd64Url = new()
		{
			{
				JdkVersion.Jdk6, "https://www.atteya.net/site/en/downloads/java-jdk?download=48:java-jdk-6u45-linux-x64"
			},
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.98.0.107-ca-jdk8.0.512-linux_x64.tar.gz" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11-linux-x64.tar.gz" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17-linux-x64.tar.gz" },
			{ JdkVersion.Jdk21, "https://aka.ms/download-jdk/microsoft-jdk-21-linux-x64.tar.gz" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_linux-x64_bin.tar.gz" },
		};
		private static readonly Dictionary<JdkVersion, String> arm64Url = new()
		{
			{ JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.98.0.107-ca-jdk8.0.512-linux_aarch64.tar.gz" },
			{ JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11-linux-aarch64.tar.gz" },
			{ JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17-linux-aarch64.tar.gz" },
			{ JdkVersion.Jdk21, "https://aka.ms/download-jdk/microsoft-jdk-21-linux-aarch64.tar.gz" },
			{ JdkVersion.Jdk25, "https://download.oracle.com/java/25/latest/jdk-25_linux-aarch64_bin.tar.gz" },
		};
	}
}