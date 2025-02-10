namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public interface IDownloadNotifier
{
	Int32 RefreshTime { get; }
	void Begin(String url, Int64? total);
	void Progress(String url, Int64? total, Int64 progress);
	void Progress(String url, Int64? total, Int64 progress, ref Int32 cursorTop, ref Int32 textLength);
	void End(String url, Int64 total, String destinationPath);
}