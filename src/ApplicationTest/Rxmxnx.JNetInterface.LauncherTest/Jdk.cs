using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public class Jdk
{
	public enum JdkVersion : Byte
	{
		Jdk6 = 6,
		Jdk8 = 8,
		Jdk11 = 11,
		Jdk17 = 17,
		Jdk21 = 21,
	}

	public JdkVersion JavaVersion { get; init; } = default;
	public Architecture JavaArchitecture { get; init; } = default;
	public String JavaCompiler { get; init; } = String.Empty;
	public String JavaArchiver { get; init; } = String.Empty;
	public String JavaExecutable { get; init; } = String.Empty;
	public String JavaLibrary { get; init; } = String.Empty;
}