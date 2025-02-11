namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	public static async Task<Launcher> Create(DirectoryInfo outputDirectory)
	{
		ConsoleNotifier.PlatformNotifier.BeginDetection();

		OSPlatform platform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSPlatform.Windows :
			RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OSPlatform.OSX :
			RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? OSPlatform.Linux : default;

		if (platform == OSPlatform.OSX)
			return await Launcher.Create<Mac>(outputDirectory);
		if (platform == OSPlatform.Windows)
			return await Launcher.Create<Windows>(outputDirectory);
		if (platform == OSPlatform.Linux)
			return await Launcher.Create<Linux>(outputDirectory);

		throw new InvalidOperationException("Unsupported platform");
	}

	private static async Task<TLauncher> Create<TLauncher>(DirectoryInfo outputDirectory)
		where TLauncher : Launcher, ILauncher<TLauncher>
	{
		TLauncher result = TLauncher.Create(outputDirectory, out Task initialize);
		ConsoleNotifier.PlatformNotifier.EndDetection(TLauncher.Platform, result.CurrentArch);

		await initialize;
		ConsoleNotifier.PlatformNotifier.Initialization(TLauncher.Platform, result.CurrentArch);
		return result;
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
	private static IReadOnlyDictionary<JdkVersion, String> GetEnvironmentVariables(Architecture arch)
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
		ConsoleNotifier.ZipNotifier.BeginExtraction(zipFile);
		archive.ExtractToDirectory(destinationPath);
		ConsoleNotifier.ZipNotifier.EndExtraction(zipFile, destinationPath);
	}
}