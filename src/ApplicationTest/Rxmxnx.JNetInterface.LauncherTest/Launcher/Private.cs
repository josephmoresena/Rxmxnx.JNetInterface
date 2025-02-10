using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private Launcher(DirectoryInfo outputDirectory, OSPlatform platform)
	{
		this.OutputDirectory = outputDirectory;
		this.Platform = platform;
		this.CurrentArch = RuntimeInformation.OSArchitecture;
		ConsoleNotifier.PlatformNotifier.EndDetection(this.Platform, this.CurrentArch);
	}

	private async Task<Jdk?> GetJdk(Jdk.JdkVersion version, Architecture arch)
	{
		if (!Launcher.GetEnvironmentVariables(arch).TryGetValue(version, out String? envVar)) return default;
		Jdk? result = default;
		String? jdkPath = Environment.GetEnvironmentVariable(envVar);

		if (!String.IsNullOrEmpty(jdkPath))
			result = this.GetJdk(version, arch, jdkPath);

		result ??= await this.DownloadJdk(version, arch);
		return result;
	}
	private Jdk? GetJdk(Jdk.JdkVersion version, Architecture arch, String jdkPath)
	{
		Jdk? result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory);
		if (result is not null)
			ConsoleNotifier.PlatformNotifier.JdkFound(version, arch, jdkDirectory.FullName);
		return result;
	}
	private Jdk? GetJdk(Jdk.JdkVersion version, Architecture arch, String jdkPath, out DirectoryInfo jdkDirectory)
	{
		jdkDirectory = new(jdkPath);
		return this.GetJdk(version, arch, jdkDirectory);
	}
	private Jdk? GetJdk(Jdk.JdkVersion version, Architecture arch, DirectoryInfo jdkDirectory)
	{
		if (!jdkDirectory.Exists) return default;
		FileInfo? javaFile = jdkDirectory.GetFiles(this.JavaExecutableName, SearchOption.AllDirectories)
		                                 .FirstOrDefault();
		FileInfo? javacFile =
			jdkDirectory.GetFiles(this.JavaCompilerName, SearchOption.AllDirectories).FirstOrDefault();
		FileInfo? jarFile = jdkDirectory.GetFiles(this.JavaArchiverName, SearchOption.AllDirectories).FirstOrDefault();
		FileInfo? jvmFile = jdkDirectory.GetFiles(this.GetJavaLibraryName(version), SearchOption.AllDirectories)
		                                .FirstOrDefault();
		if (javaFile is not null && javacFile is not null && jarFile is not null && jvmFile is not null)
			return new()
			{
				JavaVersion = version,
				JavaArchitecture = arch,
				JavaExecutable = javaFile.FullName,
				JavaCompiler = javacFile.FullName,
				JavaArchiver = jarFile.FullName,
				JavaLibrary = jvmFile.FullName,
			};
		return default;
	}
}