namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private static readonly Dictionary<Jdk.JdkVersion, String> javaHomeX86 = new()
	{
		{ Jdk.JdkVersion.Jdk6, "JAVA_HOME_6_X86" }, // Oracle
		{ Jdk.JdkVersion.Jdk8, "JAVA_HOME_8_X86" }, // Azul
		{ Jdk.JdkVersion.Jdk11, "JAVA_HOME_11_X86" },
		{ Jdk.JdkVersion.Jdk17, "JAVA_HOME_17_X86" },
	};
	private static readonly Dictionary<Jdk.JdkVersion, String> javaHomeX64 = new()
	{
		{ Jdk.JdkVersion.Jdk6, "JAVA_HOME_6_X64" }, // Oracle
		{ Jdk.JdkVersion.Jdk8, "JAVA_HOME_8_X64" }, // Azul
		{ Jdk.JdkVersion.Jdk11, "JAVA_HOME_11_X64" },
		{ Jdk.JdkVersion.Jdk17, "JAVA_HOME_17_X64" },
		{ Jdk.JdkVersion.Jdk21, "JAVA_HOME_21_X64" },
	};
	private static readonly Dictionary<Jdk.JdkVersion, String> javaHomeArm = new()
	{
		{ Jdk.JdkVersion.Jdk8, "JAVA_HOME_8_arm" }, // Azul
		{ Jdk.JdkVersion.Jdk11, "JAVA_HOME_11_arm" }, // Azul
		{ Jdk.JdkVersion.Jdk17, "JAVA_HOME_17_arm" }, // Azul
	};
	private static readonly Dictionary<Jdk.JdkVersion, String> javaHomeArm64 = new()
	{
		{ Jdk.JdkVersion.Jdk8, "JAVA_HOME_8_arm64" }, // Azul
		{ Jdk.JdkVersion.Jdk11, "JAVA_HOME_11_arm64" },
		{ Jdk.JdkVersion.Jdk17, "JAVA_HOME_17_arm64" },
		{ Jdk.JdkVersion.Jdk21, "JAVA_HOME_21_arm64" },
	};
}