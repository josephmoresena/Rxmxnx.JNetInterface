namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private static readonly Dictionary<JdkVersion, String> javaHomeX86 = new()
	{
		{ JdkVersion.Jdk6, "JAVA_HOME_6_X86" }, // Oracle
		{ JdkVersion.Jdk8, "JAVA_HOME_8_X86" }, // Azul
		{ JdkVersion.Jdk11, "JAVA_HOME_11_X86" },
		{ JdkVersion.Jdk17, "JAVA_HOME_17_X86" },
	};
	private static readonly Dictionary<JdkVersion, String> javaHomeX64 = new()
	{
		{ JdkVersion.Jdk6, "JAVA_HOME_6_X64" }, // Oracle
		{ JdkVersion.Jdk8, "JAVA_HOME_8_X64" }, // Azul
		{ JdkVersion.Jdk11, "JAVA_HOME_11_X64" },
		{ JdkVersion.Jdk17, "JAVA_HOME_17_X64" },
		{ JdkVersion.Jdk21, "JAVA_HOME_21_X64" },
	};
	private static readonly Dictionary<JdkVersion, String> javaHomeArm = new()
	{
		{ JdkVersion.Jdk8, "JAVA_HOME_8_arm" }, // Azul
		{ JdkVersion.Jdk11, "JAVA_HOME_11_arm" }, // Azul
		{ JdkVersion.Jdk17, "JAVA_HOME_17_arm" }, // Azul
	};
	private static readonly Dictionary<JdkVersion, String> javaHomeArm64 = new()
	{
		{ JdkVersion.Jdk8, "JAVA_HOME_8_arm64" }, // Azul
		{ JdkVersion.Jdk11, "JAVA_HOME_11_arm64" },
		{ JdkVersion.Jdk17, "JAVA_HOME_17_arm64" },
		{ JdkVersion.Jdk21, "JAVA_HOME_21_arm64" },
	};
}