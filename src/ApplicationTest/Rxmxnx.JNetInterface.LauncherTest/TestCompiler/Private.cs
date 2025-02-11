namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private static async Task CompileJavaClass(String classPath, Jdk jdk)
	{
		String javaFilePath = Path.Combine(classPath, "HelloDotnet.java");

		try
		{
			await File.WriteAllTextAsync(javaFilePath, TestCompiler.JavaCode);
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

		try
		{
			await File.WriteAllTextAsync(outputPath, TestCompiler.JarManifest);
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
	private static async Task CompileNetLibrary(RestoreNetArgs restoreArgs, String outputPath)
	{
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
	private static async Task CompileNetApp(RestoreNetArgs restoreArgs, String outputPath)
	{
		CompileNetArgs compileArgs = new(restoreArgs, outputPath)
		{
			BuildDependencies = true, Publish = Publish.SelfContained,
		};

		await TestCompiler.RestoreNet(restoreArgs);

		await TestCompiler.CompileNet(compileArgs);
		compileArgs.BuildDependencies = false;

		compileArgs.Publish = Publish.ReadyToRun;
		await TestCompiler.CompileNet(compileArgs);

		compileArgs.Publish = Publish.NativeAot;
		await TestCompiler.CompileNet(compileArgs);
		if (!restoreArgs.ProjectFile.EndsWith(".csproj") || restoreArgs.Version > NetVersion.Net80) return;

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