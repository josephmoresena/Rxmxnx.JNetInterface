using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	public DirectoryInfo OutputDirectory { get; set; }
	public OSPlatform Platform { get; }
	public Architecture CurrentArch { get; }

	public virtual IEnumerable<Jdk> this[Architecture arch] => [];
	public abstract String RuntimeIdentifierPrefix { get; }

	public abstract Architecture[] Architectures { get; }

	protected abstract String JavaArchiverName { get; }
	protected abstract String JavaExecutableName { get; }
	protected abstract String JavaCompilerName { get; }

	public async Task Execute()
	{
		Dictionary<Architecture, FileInfo[]> archFiles = this.Architectures.ToDictionary(
			a => a,
			a => this.OutputDirectory.GetFiles(
				$"ApplicationTest.*.{this.RuntimeIdentifierPrefix}-{Enum.GetName(a)!.ToLower()}.*"));
		FileInfo? jarFile = this.OutputDirectory.GetFiles("HelloJni.jar").FirstOrDefault();
		foreach (Jdk jdk in this.Architectures.SelectMany(a => this[a]))
		{
			if (jarFile is not null)
				foreach (TestCompiler.NetVersion netVersion in Enum.GetValues<TestCompiler.NetVersion>())
					await this.RunJarFile(jdk, jarFile, netVersion);

			foreach (FileInfo appFile in archFiles[jdk.JavaArchitecture])
				await this.RunAppFile(appFile, jdk);
		}
	}

	public abstract Jdk GetMinJdk();

	protected abstract String GetJavaLibraryName(Jdk.JdkVersion version);
	protected abstract Task<Jdk> DownloadJdk(Jdk.JdkVersion version, Architecture arch);

	protected virtual String? GetQemu(Architecture arch, out String qemuRoot)
	{
		qemuRoot = String.Empty;
		return default;
	}
	protected virtual async Task AppendJdk(IDictionary<Jdk.JdkVersion, Jdk> jdks, Jdk.JdkVersion version,
		Architecture arch)
	{
		ConsoleNotifier.PlatformNotifier.JdkDetection(version, arch);
		if (await this.GetJdk(version, arch) is { } jdk)
			jdks.TryAdd(version, jdk);
		else
			ConsoleNotifier.PlatformNotifier.JdkUnavailable(version, arch);
	}
}