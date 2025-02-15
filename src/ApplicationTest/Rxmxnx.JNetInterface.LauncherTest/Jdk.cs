namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public record Jdk
{
	public JdkVersion JavaVersion { get; init; }
	public Architecture JavaArchitecture { get; init; }
	public String JavaCompiler { get; init; } = String.Empty;
	public String JavaArchiver { get; init; } = String.Empty;
	public String JavaExecutable { get; init; } = String.Empty;
	public String JavaLibrary { get; init; } = String.Empty;
}