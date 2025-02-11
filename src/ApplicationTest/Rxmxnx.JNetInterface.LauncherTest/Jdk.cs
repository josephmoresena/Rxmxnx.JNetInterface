namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public class Jdk
{
	public JdkVersion JavaVersion { get; init; } = default;
	public Architecture JavaArchitecture { get; init; } = default;
	public String JavaCompiler { get; init; } = String.Empty;
	public String JavaArchiver { get; init; } = String.Empty;
	public String JavaExecutable { get; init; } = String.Empty;
	public String JavaLibrary { get; init; } = String.Empty;
}