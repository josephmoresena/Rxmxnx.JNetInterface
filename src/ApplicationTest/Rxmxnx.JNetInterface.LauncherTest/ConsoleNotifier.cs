using System.Diagnostics;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public sealed class ConsoleNotifier : IDownloadNotifier, IExecutionNotifier
{
	public static readonly ConsoleNotifier Notifier = new();

	private ConsoleNotifier() { }

	public Int32 RefreshTime => 250;
	public void Begin(String url, Int64? total)
	{
		if (!total.HasValue)
		{
			Console.WriteLine($"Starting download... {url}");
			return;
		}
		Double value = ConsoleNotifier.GetValue(total.Value, out String unitName);
		Console.WriteLine($"Downloading... {url} [{value:0.##} {unitName}]");
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

		value = progress;
		value /= total.Value;
		Console.WriteLine($"Downloading... {url} [{value:P}]");
	}
	public void End(String url, Int64 total, String destinationPath)
		=> Console.WriteLine(
			$"Downloaded. {url} -> {destinationPath} [{ConsoleNotifier.GetValue(total, out String unitName):0.##} {unitName}]");

	public void Begin(ProcessStartInfo info)
	{
		String args = String.Join(' ', info.ArgumentList);
		Console.WriteLine($"Starting... {info.WorkingDirectory} {info.FileName} {args}");
	}
	public void End(ProcessStartInfo info)
	{
		String args = String.Join(' ', info.ArgumentList);
		Console.WriteLine($"Finished. {info.WorkingDirectory} {info.FileName} {args}");
	}

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
}