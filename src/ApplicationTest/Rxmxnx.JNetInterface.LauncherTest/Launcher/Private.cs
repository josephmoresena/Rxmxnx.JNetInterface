using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;
using Rxmxnx.PInvoke;

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
	private async Task RunJarFile(Jdk jdk, FileInfo jarFile, TestCompiler.NetVersion netVersion)
	{
		String? qemuExe = this.GetQemu(jdk.JavaArchitecture, out String qemuRoot);
		IMutableWrapper<Boolean> noReflection = IMutableWrapper.Create(false);
		await (String.IsNullOrEmpty(qemuRoot) ?
			this.RunJarNoQemu(jdk, jarFile, netVersion, noReflection) :
			this.RunJarQemu(qemuExe!, qemuRoot, jdk, jarFile, netVersion, noReflection));
	}
	private async Task RunAppFile(FileInfo appFile, Jdk jdk)
	{
		String? qemuExe = this.GetQemu(jdk.JavaArchitecture, out String qemuRoot);
		await (String.IsNullOrEmpty(qemuRoot) ?
			this.RunAppNoQemu(jdk, appFile) :
			this.RunAppQemu(qemuExe!, qemuRoot, jdk, appFile));
	}
	private async Task RunJarNoQemu(Jdk jdk, FileInfo jarFile, TestCompiler.NetVersion netVersion,
		IMutableWrapper<Boolean> noReflection)
	{
		ExecuteState<ValueTuple<Byte, IWrapper<Boolean>, String>> state = new()
		{
			ExecutablePath = jdk.JavaExecutable,
			ArgState = ((Byte)netVersion, noReflection, jarFile.FullName),
			AppendArgs = (s, a) =>
			{
				a.Add($"-Ddotnet.runtime.version=net{s.Item1}.0");
				if (s.Item2.Value)
					a.Add("-Ddotnet.reflection.disable=true");
				a.Add("-jar");
				a.Add(jarFile.Name);
			},
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		ConsoleNotifier.Notifier.Result(await Utilities.Execute(state),
		                                $"HelloJni.jar {jdk.JavaVersion} {jdk.JavaArchitecture} {netVersion} Reflection: {!noReflection.Value}");
		if (netVersion > TestCompiler.NetVersion.Net80) return;
		noReflection.Value = true;
		ConsoleNotifier.Notifier.Result(await Utilities.Execute(state),
		                                $"HelloJni.jar {jdk.JavaVersion} {jdk.JavaArchitecture} {netVersion} Reflection: {!noReflection.Value}");
	}
	private async Task RunJarQemu(String qemuExe, String qemuRoot, Jdk jdk, FileInfo jarFile,
		TestCompiler.NetVersion netVersion, IMutableWrapper<Boolean> noReflection)
	{
		QemuExecuteState<ValueTuple<Byte, IWrapper<Boolean>, String>> state = new()
		{
			QemuRoot = qemuRoot,
			QemuExecutable = qemuExe,
			ExecutablePath = jdk.JavaExecutable,
			ArgState = ((Byte)netVersion, noReflection, jarFile.FullName),
			AppendArgs = (s, a) =>
			{
				a.Add($"-Ddotnet.runtime.version=net{s.Item1}.0");
				if (s.Item2.Value)
					a.Add("-Ddotnet.reflection.disable=true");
				a.Add("-jar");
				a.Add(jarFile.Name);
			},
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		ConsoleNotifier.Notifier.Result(await Utilities.QemuExecute(state),
		                                $"HelloJni.jar {jdk.JavaVersion} {jdk.JavaArchitecture} {netVersion} Reflection: {!noReflection.Value}");
		if (netVersion > TestCompiler.NetVersion.Net80) return;
		noReflection.Value = true;
		ConsoleNotifier.Notifier.Result(await Utilities.QemuExecute(state),
		                                $"HelloJni.jar {jdk.JavaVersion} {jdk.JavaArchitecture} {netVersion} Reflection: {!noReflection.Value}");
	}
	private async Task RunAppNoQemu(Jdk jdk, FileInfo appFile)
	{
		ExecuteState<String> state = new()
		{
			ExecutablePath = appFile.FullName,
			ArgState = jdk.JavaLibrary,
			AppendArgs = (s, a) => a.Add(s),
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		Int32 result = await Utilities.Execute(state);
		ConsoleNotifier.Notifier.Result(result, appFile.Name);
	}
	private async Task RunAppQemu(String qemuExe, String qemuRoot, Jdk jdk, FileInfo appFile)
	{
		QemuExecuteState<String> state = new()
		{
			QemuRoot = qemuRoot,
			QemuExecutable = qemuExe,
			ExecutablePath = appFile.FullName,
			ArgState = jdk.JavaLibrary,
			AppendArgs = (s, a) => a.Add(s),
			WorkingDirectory = this.OutputDirectory.FullName,
			Notifier = ConsoleNotifier.Notifier,
		};
		Int32 result = await Utilities.QemuExecute(state);
		ConsoleNotifier.Notifier.Result(result, appFile.Name);
	}
}