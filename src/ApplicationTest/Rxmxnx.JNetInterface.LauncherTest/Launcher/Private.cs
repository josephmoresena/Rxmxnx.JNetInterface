namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private Launcher(DirectoryInfo outputDirectory)
	{
		this.OutputDirectory = outputDirectory;
		this.CurrentArch = RuntimeInformation.OSArchitecture;
	}

	private async Task<Jdk?> GetJdk(JdkVersion version, Architecture arch)
	{
		if (!Launcher.GetEnvironmentVariables(arch).TryGetValue(version, out String? envVar)) return default;
		Jdk? result = default;
		String? jdkPath = Environment.GetEnvironmentVariable(envVar);

		if (!String.IsNullOrEmpty(jdkPath))
			result = this.GetJdk(version, arch, jdkPath);

		result ??= await this.DownloadJdk(version, arch);
		return result;
	}
	private Jdk? GetJdk(JdkVersion version, Architecture arch, String jdkPath)
	{
		Jdk? result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory);
		if (result is not null)
			ConsoleNotifier.PlatformNotifier.JdkFound(version, arch, jdkDirectory.FullName);
		return result;
	}
	private Jdk? GetJdk(JdkVersion version, Architecture arch, String jdkPath, out DirectoryInfo jdkDirectory)
	{
		jdkDirectory = new(jdkPath);
		return this.GetJdk(version, arch, jdkDirectory);
	}
	private Jdk? GetJdk(JdkVersion version, Architecture arch, DirectoryInfo jdkDirectory)
	{
		if (!jdkDirectory.Exists) return default;
		FileInfo? javaFile = jdkDirectory.GetFiles(this.JavaExecutableName, SearchOption.AllDirectories)
		                                 .FirstOrDefault();
		FileInfo? javacFile =
			jdkDirectory.GetFiles(this.JavaCompilerName, SearchOption.AllDirectories).FirstOrDefault();
		FileInfo? jarFile = jdkDirectory.GetFiles(this.JavaArchiverName, SearchOption.AllDirectories).FirstOrDefault();
		FileInfo? jvmFile = jdkDirectory.GetFiles(this.GetJavaLibraryName(version), SearchOption.AllDirectories)
		                                .Where(f => String.IsNullOrEmpty(f.LinkTarget)) // Exclude symbolic links
		                                .LastOrDefault(
			                                f => f.FullName.Contains(
				                                "server", StringComparison.InvariantCultureIgnoreCase));
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
	private async Task RunJarFile(Jdk jdk, FileInfo jarFile, NetVersion netVersion, Dictionary<String, Int32> results)
	{
		if (!Utilities.IsNativeAotSupported(jdk.JavaArchitecture, netVersion)) return;

		JarArgs jarArgs = new() { Version = netVersion, JarName = jarFile.Name, };
		String prefix = $"HelloJni.jar ({jdk.JavaVersion}, {jdk.JavaArchitecture}, {netVersion}";

		String name = $"{prefix})";
		Int32 result = await this.RunJarFile(jarArgs, jdk);

		ConsoleNotifier.Notifier.Result(result, name);
		results.Add(name, result);

		if (netVersion > NetVersion.Net80) return;

		jarArgs.NoReflection = true;
		result = await this.RunJarFile(jarArgs, jdk, ConsoleNotifier.CancellationToken);
		name = $"{prefix}, no-reflection)";

		ConsoleNotifier.Notifier.Result(result, name);
		results.Add(name, result);
	}
	private async Task<Int32> RunJarFile(JarArgs jarArgs, Jdk jdk)
	{
		using CancellationTokenSource source = new(TimeSpan.FromMinutes(5));
		CancellationTokenRegistration registry = ConsoleNotifier.RegisterCancellation(source);
		try
		{
			return await this.RunJarFile(jarArgs, jdk, source.Token);
		}
		catch (OperationCanceledException)
		{
			return -1;
		}
		finally
		{
			registry.Unregister();
		}
	}
	private async Task<Int32> RunAppFile(FileInfo appFile, Jdk jdk, String executionName)
	{
		using CancellationTokenSource source = new(TimeSpan.FromMinutes(5));
		CancellationTokenRegistration registry = ConsoleNotifier.RegisterCancellation(source);
		try
		{
			return await this.RunAppFile(appFile, jdk, executionName, ConsoleNotifier.CancellationToken);
		}
		catch (OperationCanceledException)
		{
			return -1;
		}
		finally
		{
			registry.Unregister();
		}
	}
}