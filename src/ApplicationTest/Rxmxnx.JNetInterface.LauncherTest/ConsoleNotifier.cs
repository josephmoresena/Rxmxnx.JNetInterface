using System.Diagnostics;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public sealed class ConsoleNotifier : IDownloadNotifier, IExecutionNotifier, IPlatformNotifier, IZipNotifier
{
	public static readonly ConsoleNotifier Notifier = new();
	public static IPlatformNotifier PlatformNotifier => ConsoleNotifier.Notifier;
	public static IZipNotifier ZipNotifier => ConsoleNotifier.Notifier;

	private ConsoleNotifier() { }

	public Int32 RefreshTime => 250;

	public void Begin(String url, Int64? total)
	{
		if (!total.HasValue)
		{
			ConsoleNotifier.WriteColoredLine(ConsoleColor.Blue, $"Starting download... {url}");
			return;
		}
		Double value = ConsoleNotifier.GetValue(total.Value, out String unitName);
		ConsoleNotifier.WriteColoredLine(ConsoleColor.Blue, $"Downloading... {url} [{value:0.##} {unitName}]");
	}

	public void Progress(String url, Int64? total, Int64 progress)
	{
		Double value;
		if (!total.HasValue)
		{
			value = ConsoleNotifier.GetValue(progress, out String unitName);
			Console.WriteLine($"Downloading... {url} [{value:0.##} {unitName}]");
			return;
		}

		value = progress / (Double)total.Value;
		Console.WriteLine($"Downloading... {url} [{value:P}]");
	}
	public void Progress(String url, Int64? total, Int64 progress, ref Int32 cursorTop, ref Int32 textLength)
	{
		Double value;
		String text;
		if (!total.HasValue)
		{
			value = ConsoleNotifier.GetValue(progress, out String unitName);
			text = $"Downloading... {url} [{value:0.##} {unitName}]";
		}
		else
		{
			value = progress / (Double)total.Value;
			text = $"Downloading... {url} [{value:P}]";
		}

		if (cursorTop == -1)
			cursorTop = Console.CursorTop;

		Console.SetCursorPosition(0, cursorTop);
		Console.WriteLine(text.PadRight(textLength));
		textLength = text.Length;
	}

	public void End(String url, Int64 total, String destinationPath)
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Green,
		                                    $"Downloaded. {url} -> {destinationPath} [{ConsoleNotifier.GetValue(total, out String unitName):0.##} {unitName}]");

	public void Begin(ProcessStartInfo info)
	{
		String args = String.Join(' ', info.ArgumentList);
		ConsoleNotifier.WriteColoredLine(ConsoleColor.Blue,
		                                 $"Starting... [{info.WorkingDirectory}] {info.FileName} {args}");
	}

	public void End(ProcessStartInfo info)
	{
		String args = String.Join(' ', info.ArgumentList);
		ConsoleNotifier.WriteColoredLine(ConsoleColor.Green,
		                                 $"Finished. [{info.WorkingDirectory}] {info.FileName} {args}");
	}

	public void Result(Int32 result, String executionName)
	{
		ConsoleColor color = result == 0 ? ConsoleColor.Green : ConsoleColor.Red;
		ConsoleNotifier.WriteColoredLine(color, $"{executionName}: {result}");
	}

	void IPlatformNotifier.BeginDetection()
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Blue, "Detecting platform...");
	void IPlatformNotifier.EndDetection(OSPlatform platform, Architecture arch)
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Green, $"{platform} {arch} detected.");
	void IPlatformNotifier.Initialization(OSPlatform platform, Architecture arch)
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Green, $"{platform} {arch} initialized.");
	void IPlatformNotifier.JdkDetection(Jdk.JdkVersion version, Architecture arch)
		=> Console.WriteLine($"Looking for jdk {(Byte)version} {arch}...");
	void IPlatformNotifier.JdkUnavailable(Jdk.JdkVersion version, Architecture arch)
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Red, $"Jdk {(Byte)version} {arch} unavailable.");
	void IPlatformNotifier.JdkDownload(Jdk.JdkVersion version, Architecture arch, String jdkPath)
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Green, $"Jdk {(Byte)version} {arch} downloaded [{jdkPath}].");
	void IPlatformNotifier.JdkFound(Jdk.JdkVersion version, Architecture arch, String jdkPath)
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Green, $"Jdk {(Byte)version} {arch} found [{jdkPath}].");
	void IZipNotifier.BeginExtraction(String zipPath)
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Blue, $"Extracting... {zipPath}.");
	void IZipNotifier.EndExtraction(String zipPath, String destinationPath)
		=> ConsoleNotifier.WriteColoredLine(ConsoleColor.Green, $"{zipPath} extracted to {destinationPath}.");

	private static Double GetValue(Int64 total, out String unitName)
	{
		const Int32 threshold = 1024;
		ReadOnlySpan<String> unitNames = ["B", "KiB", "MiB", "GiB",];
		Double value = total;
		Int32 unit = 0;
		while (unit < unitNames.Length && value >= threshold)
		{
			value /= 1024;
			unit++;
		}
		unitName = unitNames[unit];
		return value;
	}
	private static void WriteColoredLine(ConsoleColor color, String message)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(message);
		Console.ResetColor();
	}
}