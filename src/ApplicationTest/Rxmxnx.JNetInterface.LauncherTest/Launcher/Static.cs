using System.IO.Compression;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	public static async Task<Launcher> Create(DirectoryInfo outputDirectory)
	{
		Console.WriteLine("Detecting platform...");

		OSPlatform platform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSPlatform.Windows :
			RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OSPlatform.OSX :
			RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? OSPlatform.Linux : default;

		if (platform == OSPlatform.OSX)
			return await Mac.CreateInstance(outputDirectory);

		throw new InvalidOperationException("Unsupported platform");
	}

	private static async Task ExtractTarGz(String tarGzFile, String destinationPath,
		CancellationToken cancellationToken = default)
	{
		ExecuteState<ValueTuple<String, String>> state = new()
		{
			ExecutablePath = "tar",
			ArgState = (tarGzFile, destinationPath),
			AppendArgs = (s, a) =>
			{
				a.Add("-xf");
				a.Add(s.Item1);
				a.Add("-C");
				a.Add(s.Item2);
			},
			Notifier = ConsoleNotifier.Notifier,
		};
		await Utilities.Execute(state, cancellationToken);
	}
	private static IReadOnlyDictionary<Jdk.JdkVersion, String> GetEnvironmentVariables(Architecture arch)
		=> arch switch
		{
			Architecture.Arm64 => Launcher.javaHomeArm64,
			Architecture.X64 => Launcher.javaHomeX64,
			Architecture.X86 => Launcher.javaHomeX86,
			Architecture.Arm => Launcher.javaHomeArm,
			Architecture.Armv6 => Launcher.javaHomeArm,
			_ => throw new ArgumentOutOfRangeException(nameof(arch)),
		};
	private static void ExtractZip(String zipFile, String destinationPath)
	{
		using ZipArchive archive = ZipFile.OpenRead(zipFile);
		Console.WriteLine($"Extracting... {zipFile}.");
		archive.ExtractToDirectory(destinationPath);
		Console.WriteLine($"{zipFile} extracted to {destinationPath}.");
	}
}