namespace Rxmxnx.JNetInterface.ApplicationTest;

public static partial class TestCompiler
{
	public static async Task CompileClass(Jdk jdk, DirectoryInfo outputDirectory)
	{
		DirectoryInfo directory = outputDirectory.CreateSubdirectory("jar");
		try
		{
			DirectoryInfo classDir = directory.CreateSubdirectory("com").CreateSubdirectory("rxmxnx")
			                                  .CreateSubdirectory("dotnet").CreateSubdirectory("test");
			DirectoryInfo nativeImageDir = directory.CreateSubdirectory("META-INF").CreateSubdirectory("native-image");

			String classFilePath = Path.Combine(classDir.FullName, "HelloDotnet.class");
			String jniConfigPath = Path.Combine(nativeImageDir.FullName, "jni-config.json");

			directory.Delete(true);

			classDir.Create();
			nativeImageDir.Create();

			await TestCompiler.CompileJavaClass(classDir.FullName, jdk);

			await File.WriteAllTextAsync(jniConfigPath, TestCompiler.JniConfig, ConsoleNotifier.CancellationToken);

			await TestCompiler.CreateJar(directory.FullName, jdk, outputDirectory.FullName);

			File.Move(classFilePath, Path.Combine(outputDirectory.FullName, "HelloDotnet.class"), true);
		}
		finally
		{
			directory.Delete(true);
		}
	}
	public static async Task CompileNet(DirectoryInfo projectDirectory, String os, String outputPath,
		Boolean onlyNativeAot = false)
	{
		Architecture[] architectures = OperatingSystem.IsWindows() ?
			[Architecture.X86, Architecture.X64, Architecture.Arm64,] :
			!OperatingSystem.IsLinux() ? [Architecture.X64, Architecture.Arm64,] :
				[Architecture.X64, Architecture.Arm, Architecture.Arm64,];

		String? libProjectFile = projectDirectory.GetDirectories("*.LibraryTest", SearchOption.AllDirectories)
		                                         .SelectMany(d => d.GetFiles("*.LibraryTest.csproj"))
		                                         .Select(f => f.FullName).FirstOrDefault();
		String? appGuiProjectFile = projectDirectory.GetDirectories("*.ApplicationGuiTest", SearchOption.AllDirectories)
		                                            .SelectMany(d => d.GetFiles("*.ApplicationGuiTest.csproj"))
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
					await TestCompiler.CompileNetLibrary(onlyNativeAot,
					                                     new()
					                                     {
						                                     ProjectFile = libProjectFile,
						                                     RuntimeIdentifier = rid,
						                                     Version = netVersion,
					                                     }, arch, outputPath);
				if (!String.IsNullOrEmpty(appGuiProjectFile))
					await TestCompiler.CompileNetGuiApp(onlyNativeAot,
					                                    new()
					                                    {
						                                    ProjectFile = appGuiProjectFile,
						                                    RuntimeIdentifier = rid,
						                                    Version = netVersion,
					                                    }, arch, outputPath);

				foreach (String appProjectFile in appProjectFiles)
				{
					await TestCompiler.CompileNetApp(onlyNativeAot,
					                                 new()
					                                 {
						                                 ProjectFile = appProjectFile,
						                                 RuntimeIdentifier = rid,
						                                 Version = netVersion,
					                                 }, arch, outputPath);
				}
			}
		}
	}
	public static async Task RunManagedTest(DirectoryInfo projectDirectory)
	{
		String? managedTestProjectFile = projectDirectory.GetDirectories("*.ManagedTest", SearchOption.AllDirectories)
		                                                 .SelectMany(d => d.GetFiles("*.ManagedTest.csproj"))
		                                                 .Select(f => f.FullName).FirstOrDefault();

		if (String.IsNullOrEmpty(managedTestProjectFile)) return;
		foreach (NetVersion netVersion in Enum.GetValues<NetVersion>())
			await TestCompiler.RunNetTest(new() { ProjectFile = managedTestProjectFile, Version = netVersion, });
	}
}