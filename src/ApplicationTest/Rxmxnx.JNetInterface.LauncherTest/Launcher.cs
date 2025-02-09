using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	public DirectoryInfo OutputDirectory { get; set; }
	public OSPlatform Platform { get; }
	public Architecture CurrentArch { get; }

	public virtual IEnumerable<Jdk> this[Architecture arch] => [];
	public abstract String RuntimeIdentifierPrefix { get; }

	protected abstract String JavaArchiverName { get; }
	protected abstract String JavaExecutableName { get; }
	protected abstract String JavaCompilerName { get; }

	public abstract Jdk GetMinJdk();

	public abstract Task Execute();

	protected abstract String GetJavaLibraryName(Jdk.JdkVersion version);
	protected abstract Task<Jdk> DownloadJdk(Jdk.JdkVersion version, Architecture arch);
}