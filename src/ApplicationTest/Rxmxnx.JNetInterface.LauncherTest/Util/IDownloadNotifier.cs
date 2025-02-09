namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public interface IDownloadNotifier
{
	Int32 RefreshTime { get; }
	void Begin(String url, Int64? total);
	void Progress(String url, Int64? total, Int64 progress);
	void End(String url, Int64 total, String destinationPath);
}