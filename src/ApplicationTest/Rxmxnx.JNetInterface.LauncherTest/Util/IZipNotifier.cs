namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public interface IZipNotifier
{
	void BeginExtraction(String zipPath);
	void EndExtraction(String zipPath, String destinationPath);
}