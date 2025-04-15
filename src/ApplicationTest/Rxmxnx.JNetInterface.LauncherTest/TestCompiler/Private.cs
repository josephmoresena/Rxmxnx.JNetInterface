namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private static async Task CompileJavaClass(String classPath, Jdk jdk)
	{
		String javaFilePath = Path.Combine(classPath, "HelloDotnet.java");

		await File.WriteAllTextAsync(javaFilePath, TestCompiler.JavaCode);
		try
		{
			await Utilities.Execute<CompileClassArgs>(
				new()
				{
					ExecutablePath = jdk.JavaCompiler,
					WorkingDirectory = classPath,
					ArgState = new() { JavaFilePath = javaFilePath, },
					AppendArgs = CompileClassArgs.Append,
					Notifier = ConsoleNotifier.Notifier,
				}, ConsoleNotifier.CancellationToken);
		}
		finally
		{
			File.Delete(javaFilePath);
		}
	}
	private static async Task CreateJar(String jarRootPath, Jdk jdk, String outputPath)
	{
		const String manifestName = "MANIFEST.TXT";
		String manifestPath = Path.Combine(outputPath, manifestName);

		await File.WriteAllTextAsync(manifestPath, TestCompiler.JarManifest);
		try
		{
			await Utilities.Execute<JarCreationArgs>(
				new()
				{
					ExecutablePath = jdk.JavaArchiver,
					ArgState = new()
					{
						JarRoot = Path.GetRelativePath(outputPath, jarRootPath),
						JarFileName = "HelloJni.jar",
						ManifestFileName = manifestName,
					},
					AppendArgs = JarCreationArgs.Append,
					Notifier = ConsoleNotifier.Notifier,
					WorkingDirectory = outputPath,
				}, ConsoleNotifier.CancellationToken);
		}
		finally
		{
			File.Delete(manifestPath);
		}
	}
	private static async Task RunNetTest(RestoreNetArgs restoreArgs)
	{
		await TestCompiler.RestoreNet(restoreArgs);
		ExecuteState<RestoreNetArgs> state = new()
		{
			ExecutablePath = "dotnet",
			ArgState = restoreArgs,
			AppendArgs = RestoreNetArgs.AppendTest,
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state, ConsoleNotifier.CancellationToken);
	}
	private static async Task CompileNetLibrary(Boolean onlyNativeAot, RestoreNetArgs restoreArgs, Architecture arch,
		String outputPath)
	{
		if (!Utilities.IsNativeAotSupported(arch, restoreArgs.Version)) return;

		CompileNetArgs compileArgs = new(restoreArgs, outputPath)
		{
			EnableTrace = onlyNativeAot, BuildDependencies = true, Publish = Publish.JniLibrary,
		};

		await TestCompiler.RestoreNet(restoreArgs);

		await TestCompiler.CompileNet(compileArgs);
		if (restoreArgs.Version > NetVersion.Net80) return;
		compileArgs.BuildDependencies = false;

		compileArgs.Publish |= Publish.NoReflection;
		await TestCompiler.CompileNet(compileArgs);
	}
	private static async Task CompileNetGuiApp(Boolean onlyNativeAot, RestoreNetArgs restoreArgs, Architecture arch,
		String outputPath)
	{
		if (!Utilities.IsNativeAotSupported(arch, restoreArgs.Version)) return;
		if (!OperatingSystem.IsLinux() & !OperatingSystem.IsWindows() && !OperatingSystem.IsFreeBSD()) return;

		CompileNetArgs compileArgs = new(restoreArgs, outputPath)
		{
			EnableTrace = onlyNativeAot, BuildDependencies = true, Publish = Publish.JniLibrary,
		};

		await TestCompiler.RestoreNet(restoreArgs);

		await TestCompiler.CompileNet(compileArgs);
	}
	private static async Task CompileNetApp(Boolean onlyNativeAot, RestoreNetArgs restoreArgs, Architecture arch,
		String outputPath)
	{
		CompileNetArgs compileArgs = new(restoreArgs, outputPath)
		{
			EnableTrace = onlyNativeAot, BuildDependencies = true, Publish = Publish.SelfContained,
		};

		await TestCompiler.RestoreNet(restoreArgs);

		if (!onlyNativeAot)
		{
			await TestCompiler.CompileNet(compileArgs);
			compileArgs.BuildDependencies = false;

			compileArgs.Publish = Publish.ReadyToRun;
			await TestCompiler.CompileNet(compileArgs);
		}

		if (!Utilities.IsNativeAotSupported(arch, restoreArgs.Version)) return;

		compileArgs.Publish = Publish.NativeAot;
		await TestCompiler.CompileNet(compileArgs);

		if (!restoreArgs.ProjectFile.EndsWith(".csproj") || restoreArgs.Version > NetVersion.Net80) return;

		compileArgs.BuildDependencies = false;
		compileArgs.Publish |= Publish.NoReflection;
		await TestCompiler.CompileNet(compileArgs);
	}
	private static async Task CompileNet(CompileNetArgs args)
	{
		ExecuteState<CompileNetArgs> state = new()
		{
			ExecutablePath = "dotnet",
			ArgState = args,
			AppendArgs = CompileNetArgs.Append,
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state, ConsoleNotifier.CancellationToken);
	}
	private static async Task RestoreNet(RestoreNetArgs args)
	{
		ExecuteState<RestoreNetArgs> state = new()
		{
			ExecutablePath = "dotnet",
			ArgState = args,
			AppendArgs = RestoreNetArgs.Append,
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state, ConsoleNotifier.CancellationToken);

		state = new()
		{
			ExecutablePath = "dotnet",
			ArgState = args,
			AppendArgs = RestoreNetArgs.AppendList,
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state, ConsoleNotifier.CancellationToken);
	}
	private static Boolean ArchSupported(Architecture arch)
	{
		Architecture currentArch = RuntimeInformation.OSArchitecture;
		return arch == currentArch || currentArch switch
		{
			Architecture.X86 => false,
			Architecture.Arm => false,
			Architecture.Armv6 => false,
			_ => true,
		};
	}
}