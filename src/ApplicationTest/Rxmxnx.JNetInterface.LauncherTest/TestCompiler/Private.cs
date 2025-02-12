namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private static async Task CompileJavaClass(String classPath, Jdk jdk)
	{
		String javaFilePath = Path.Combine(classPath, "HelloDotnet.java");

		await File.WriteAllTextAsync(javaFilePath, TestCompiler.JavaCode);
		try
		{
			await Utilities.Execute<CompileClassArgs>(new()
			{
				ExecutablePath = jdk.JavaCompiler,
				ArgState = new()
				{
					JavaFilePath = javaFilePath,
					Target = jdk.JavaVersion > JdkVersion.Jdk6 ?
						"1.6" :
						default,
				},
				AppendArgs = CompileClassArgs.Append,
				Notifier = ConsoleNotifier.Notifier,
			});
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
			await Utilities.Execute<JarCreationArgs>(new()
			{
				ExecutablePath = jdk.JavaArchiver,
				ArgState = new()
				{
					JarRoot =
						Path.GetRelativePath(outputPath, jarRootPath),
					JarFileName = "HelloJni.jar",
					ManifestFileName = manifestName,
				},
				AppendArgs = JarCreationArgs.Append,
				Notifier = ConsoleNotifier.Notifier,
				WorkingDirectory = outputPath,
			});
		}
		finally
		{
			File.Delete(manifestPath);
		}
	}
	private static async Task CompileNetLibrary(RestoreNetArgs restoreArgs, Architecture arch, String outputPath)
	{
		if (!Utilities.IsNativeAotSupported(arch, restoreArgs.Version)) return;

		CompileNetArgs compileArgs = new(restoreArgs, outputPath)
		{
			BuildDependencies = true, Publish = Publish.JniLibrary,
		};

		await TestCompiler.RestoreNet(restoreArgs);

		await TestCompiler.CompileNet(compileArgs);
		if (restoreArgs.Version > NetVersion.Net80) return;
		compileArgs.BuildDependencies = false;

		compileArgs.Publish |= Publish.NoReflection;
		await TestCompiler.CompileNet(compileArgs);
	}
	private static async Task CompileNetApp(Boolean onlyNativeAot, RestoreNetArgs restoreArgs, Architecture arch,
		String outputPath)
	{
		CompileNetArgs compileArgs = new(restoreArgs, outputPath)
		{
			BuildDependencies = true, Publish = Publish.SelfContained,
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
		await Utilities.Execute(state);
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
		await Utilities.Execute(state);

		state = new()
		{
			ExecutablePath = "dotnet",
			ArgState = args,
			AppendArgs = RestoreNetArgs.AppendList,
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state);
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