using System.Reflection;
using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial record JCompiler
{
	public static JCompiler[] GetCompilers()
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			return JCompiler.GetWindowsCompilers();
		if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			return JCompiler.GetMacCompilers();
		return RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ?
			JCompiler.GetLinuxCompilers() :
			Array.Empty<JCompiler>();
	}

	private static JCompiler[] GetWindowsCompilers()
	{
		String javaPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Java");
		return JCompiler.GetCompilers(javaPath, "jdk-*", "javac.exe", "jvm.dll");
	}
	private static JCompiler[] GetMacCompilers()
	{
		String javaPath = Path.Combine("/Library/", "Java", "JavaVirtualMachines");
		return JCompiler.GetCompilers(javaPath, "*.jdk", "javac", "libjvm.dylib");
	}
	private static JCompiler[] GetLinuxCompilers()
	{
		String javaPath = Path.Combine("/Library/", "Java", "JavaVirtualMachines");
		return JCompiler.GetCompilers(javaPath, "jdk-*", "javac", "libjvm.so");
	}
	private static JCompiler[] GetCompilers(String javaPath, String jdkPattern, String javacName, String jvmName)
	{
		DirectoryInfo javaDirectory = new(javaPath);
		DirectoryInfo[] jdkDirectories = javaDirectory.Exists ?
			javaDirectory.GetDirectories(jdkPattern) :
			Array.Empty<DirectoryInfo>();
		if (jdkDirectories.Length == 0) return Array.Empty<JCompiler>();
		List<JCompiler> result = new(jdkDirectories.Length);
		foreach (DirectoryInfo jdkDirectory in jdkDirectories)
		{
			FileInfo? javacFile = jdkDirectory.GetFiles(javacName, SearchOption.AllDirectories).FirstOrDefault();
			FileInfo? jvmFile = jdkDirectory.GetFiles(jvmName, SearchOption.AllDirectories).FirstOrDefault();
			if (javacFile is not null && jvmFile is not null)
				result.Add(new()
				{
					JdkPath = jdkDirectory.FullName,
					CompilerPath = Path.GetRelativePath(jdkDirectory.FullName, javacFile.FullName),
					LibraryPath = Path.GetRelativePath(jdkDirectory.FullName, jvmFile.FullName),
				});
		}
		return result.ToArray();
	}
	private static async Task<String> GetHelloJniJavaAsync()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		String resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("HelloDotnet.java"));
		await using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
		using StreamReader reader = new(stream);
		return await reader.ReadToEndAsync();
	}
}