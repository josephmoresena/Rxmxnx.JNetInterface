using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JCompiler
{
	public static JCompiler[] GetCompilers()
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			return JCompiler.GetWindowsCompilers();
		if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			return JCompiler.GetMacCompilers();
		return RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? JCompiler.GetLinuxCompilers() : [];
	}

	private static JCompiler[] GetWindowsCompilers()
	{
		Environment.SpecialFolder folder = !Environment.Is64BitOperatingSystem || Environment.Is64BitProcess ?
			Environment.SpecialFolder.ProgramFiles :
			Environment.SpecialFolder.ProgramFilesX86; //WOW64 support
		String rootPath;
		try
		{
			rootPath = Environment.GetFolderPath(folder);
		}
		catch (Exception)
		{
			rootPath = folder is Environment.SpecialFolder.ProgramFiles ?
				@"C:\Program Files" :
				@"C:\Program Files (x86)";
		}
		String javaPath = Path.Combine(rootPath, "Java");
		return JCompiler.GetCompilers(javaPath, "java.exe", "javac.exe", ["jvm.dll",]);
	}
	private static JCompiler[] GetMacCompilers()
	{
		String javaPath = Path.Combine("/Library/", "Java", "JavaVirtualMachines");
		return JCompiler.GetCompilers(javaPath, "java", "javac", ["libserver.dylib", "libjvm.dylib",]);
	}
	private static JCompiler[] GetLinuxCompilers()
	{
		String javaPath = Path.Combine("/usr", "lib", "jvm");
		return JCompiler.GetCompilers(javaPath, "java", "javac", ["libjvm.so",]);
	}
	private static JCompiler[] GetCompilers(String javaPath, String javaName, String javacName,
		ReadOnlySpan<String> jvmNames)
	{
		DirectoryInfo javaDirectory = new(javaPath);
		DirectoryInfo[] jdkDirectories = javaDirectory.Exists ? javaDirectory.GetDirectories("*jdk*") : [];
		if (jdkDirectories.Length == 0) return [];
		List<JCompiler> result = new(jdkDirectories.Length);
		foreach (DirectoryInfo jdkDirectory in jdkDirectories)
		{
			FileInfo? javaFile = jdkDirectory.GetFiles(javaName, SearchOption.AllDirectories).FirstOrDefault();
			FileInfo? javacFile = jdkDirectory.GetFiles(javacName, SearchOption.AllDirectories).FirstOrDefault();
			FileInfo? jvmFile = default;
			foreach (String jvmName in jvmNames)
			{
				if (jvmFile is not null) break;
				jvmFile = jdkDirectory.GetFiles(jvmName, SearchOption.AllDirectories).FirstOrDefault();
			}
			if (javaFile is not null && javacFile is not null && jvmFile is not null)
				result.Add(new()
				{
					JdkPath = jdkDirectory.FullName,
					ExecutablePath = Path.GetRelativePath(jdkDirectory.FullName, javaFile.FullName),
					CompilerPath = Path.GetRelativePath(jdkDirectory.FullName, javacFile.FullName),
					LibraryPath = Path.GetRelativePath(jdkDirectory.FullName, jvmFile.FullName),
					JdkVersion = JCompiler.GetJavacVersion(javacFile.FullName),
				});
		}
		result.Sort();
		return result.ToArray();
	}
	private static Version GetJavacVersion(String javacPath)
	{
		ReadOnlySpan<Char> javacName = "javac";
		String version = JCompiler.ExecuteJavacVersion(javacPath);
		Int32 offset = version.AsSpan().IndexOf(javacName);
		Int32 endIndex = version.AsSpan().IndexOf(Environment.NewLine.AsSpan());

		if (offset >= 0) offset += javacName.Length;
		if (endIndex < 0) endIndex = 0;

		Span<Char> versionSpan = stackalloc Char[endIndex - offset];

		version.AsSpan()[offset..endIndex].CopyTo(versionSpan);
		while (versionSpan.IndexOf('_') is var idx && idx != -1)
			versionSpan[idx] = '.';

		return Version.Parse(versionSpan);
	}
	private static String ExecuteJavacVersion(String javacPath)
	{
		ProcessStartInfo info = new(javacPath)
		{
			ArgumentList = { "-version", }, RedirectStandardOutput = true, RedirectStandardError = true,
		};
		using Process javac = Process.Start(info)!;
		javac.WaitForExit();
		String std = javac.StandardOutput.ReadToEnd();
		String err = javac.StandardError.ReadToEnd();
		return !String.IsNullOrEmpty(std) ? std : err;
	}
}