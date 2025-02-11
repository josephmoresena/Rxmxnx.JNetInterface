namespace Rxmxnx.JNetInterface.ApplicationTest;

[ExcludeFromCodeCoverage]
public static partial class TestCompiler
{
	public static async Task CompileClass(Jdk jdk, DirectoryInfo outputDirectory)
	{
		DirectoryInfo directory = outputDirectory.CreateSubdirectory("jar");
		try
		{
			DirectoryInfo classDirectoryPath = directory.CreateSubdirectory("com").CreateSubdirectory("rxmxnx")
			                                            .CreateSubdirectory("dotnet").CreateSubdirectory("test");
			DirectoryInfo nativeImagePath = directory.CreateSubdirectory("META-INF").CreateSubdirectory("native-image");
			String javaFilePath = Path.Combine(classDirectoryPath.FullName, "HelloDotnet.java");
			String classFilePath = Path.Combine(classDirectoryPath.FullName, "HelloDotnet.class");
			String manifestPath = Path.Combine(outputDirectory.FullName, "MANIFEST.TXT");
			String jniConfigPath = Path.Combine(nativeImagePath.FullName, "jni-config.json");

			directory.Delete(true);

			classDirectoryPath.Create();
			nativeImagePath.Create();

			await File.WriteAllTextAsync(javaFilePath, TestCompiler.JavaCode);
			await File.WriteAllTextAsync(manifestPath, TestCompiler.JarManifest);
			await File.WriteAllTextAsync(jniConfigPath, TestCompiler.JniConfig);

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

			File.Delete(javaFilePath);

			await Utilities.Execute<JarCreationArgs>(new()
			{
				ExecutablePath = jdk.JavaArchiver,
				ArgState = new()
				{
					JarRoot = Path.GetRelativePath(
						outputDirectory.FullName, directory.FullName),
					JarFileName = "HelloJni.jar",
					ManifestFileName = "MANIFEST.TXT",
				},
				AppendArgs = JarCreationArgs.Append,
				Notifier = ConsoleNotifier.Notifier,
				WorkingDirectory = outputDirectory.FullName,
			});

			File.Delete(manifestPath);

			File.Move(classFilePath, Path.Combine(outputDirectory.FullName, "HelloDotnet.class"), true);
		}
		finally
		{
			directory.Delete(true);
		}
	}
	public static async Task CompileNet(DirectoryInfo projectDirectory, String os, String outputPath)
	{
		Architecture[] architectures = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
			[Architecture.X86, Architecture.X64, Architecture.Arm64,] :
			RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? [Architecture.X64, Architecture.Arm64,] :
				[Architecture.X64, Architecture.Arm, Architecture.Arm64,];

		String? libProjectFile = projectDirectory.GetDirectories("*.LibraryTest", SearchOption.AllDirectories)
		                                         .SelectMany(d => d.GetFiles("*.LibraryTest.csproj"))
		                                         .Select(f => f.FullName).FirstOrDefault();
		String[] appProjectFiles = projectDirectory.GetDirectories("*.*ApplicationTest", SearchOption.AllDirectories)
		                                           .SelectMany(d => d.GetFiles("*.*proj")).Select(f => f.FullName)
		                                           .ToArray();

		foreach (Architecture arch in architectures)
		{
			if (!TestCompiler.ArchSupported(arch)) continue;

			String rid = $"{os}-{Enum.GetName(arch)!.ToLower()}";
			foreach (NetVersion netVersion in Enum.GetValues<NetVersion>())
			{
				if (!String.IsNullOrEmpty(libProjectFile))
					await TestCompiler.CompileNetLibrary(libProjectFile, netVersion, rid, outputPath);

				foreach (String appProjectFile in appProjectFiles)
					await TestCompiler.CompileNetApp(appProjectFile, netVersion, rid, outputPath);
			}
		}
	}
	private static async Task CompileNetLibrary(String libProjectFile, NetVersion netVersion, String rid,
		String outputPath)
	{
		await TestCompiler.RestoreNet(libProjectFile, rid, netVersion);
		await TestCompiler.CompileNet(libProjectFile, rid, netVersion, Publish.JniLibrary, outputPath);
		if (netVersion > NetVersion.Net80) return;
		await TestCompiler.CompileNet(libProjectFile, rid, netVersion, Publish.JniLibrary | Publish.NoReflection,
		                              outputPath, false);
	}
	private static async Task CompileNetApp(String appProjectFile, NetVersion netVersion, String rid, String outputPath)
	{
		await TestCompiler.RestoreNet(appProjectFile, rid, netVersion);
		await TestCompiler.CompileNet(appProjectFile, rid, netVersion, Publish.SelfContained, outputPath);
		await TestCompiler.CompileNet(appProjectFile, rid, netVersion, Publish.ReadyToRun, outputPath, false);
		await TestCompiler.CompileNet(appProjectFile, rid, netVersion, Publish.NativeAot, outputPath, false);
		if (!appProjectFile.EndsWith(".csproj") || netVersion > NetVersion.Net80) return;
		await TestCompiler.CompileNet(appProjectFile, rid, netVersion, Publish.NativeAot | Publish.NoReflection,
		                              outputPath, false);
	}
}