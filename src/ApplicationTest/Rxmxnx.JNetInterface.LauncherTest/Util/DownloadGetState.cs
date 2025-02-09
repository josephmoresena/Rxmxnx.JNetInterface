namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public readonly struct DownloadGetState
{
	public String Url { get; init; }
	public String Destination { get; init; }
	public IDownloadNotifier? Notifier { get; init; }
}