using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
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
		private static readonly Dictionary<Jdk.JdkVersion, String> armhfUrl = new()
		{
			{
				Jdk.JdkVersion.Jdk8,
				"https://cdn.azul.com/zulu-embedded/bin/zulu8.84.0.15-ca-jdk8.0.442-linux_aarch32hf.tar.gz"
			},
			{
				Jdk.JdkVersion.Jdk11,
				"https://cdn.azul.com/zulu-embedded/bin/zulu11.78.15-ca-jdk11.0.26-linux_aarch32hf.tar.gz"
			},
			{
				Jdk.JdkVersion.Jdk17,
				"https://cdn.azul.com/zulu-embedded/bin/zulu17.56.15-ca-jdk17.0.14-c2-linux_aarch32hf.tar.gz"
			},
		};
		private static readonly Dictionary<Jdk.JdkVersion, String> amd64Url = new()
		{
			{
				Jdk.JdkVersion.Jdk6,
				"https://www.atteya.net/site/en/downloads/java-jdk?download=48:java-jdk-6u45-linux-x64"
			},
			{ Jdk.JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.84.0.15-ca-jdk8.0.442-linux_x64.tar.gz" },
			{ Jdk.JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.26-linux-x64.tar.gz" },
			{ Jdk.JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.14-linux-x64.tar.gz" },
			{ Jdk.JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_linux-x64_bin.tar.gz" },
		};
		private static readonly Dictionary<Jdk.JdkVersion, String> arm64Url = new()
		{
			{
				Jdk.JdkVersion.Jdk8, "https://cdn.azul.com/zulu/bin/zulu8.84.0.15-ca-jdk8.0.442-linux_aarch64.tar.gz"
			},
			{ Jdk.JdkVersion.Jdk11, "https://aka.ms/download-jdk/microsoft-jdk-11.0.26-linux-aarch64.tar.gz" },
			{ Jdk.JdkVersion.Jdk17, "https://aka.ms/download-jdk/microsoft-jdk-17.0.14-linux-aarch64.tar.gz" },
			{ Jdk.JdkVersion.Jdk21, "https://download.oracle.com/java/21/latest/jdk-21_linux-aarch64_bin.tar.gz" },
		};
	}
}