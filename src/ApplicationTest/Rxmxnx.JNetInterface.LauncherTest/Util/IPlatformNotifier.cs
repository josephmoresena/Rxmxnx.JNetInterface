namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public interface IPlatformNotifier
{
	void BeginDetection();
	void EndDetection(OSPlatform platform, Architecture arch);
	void Initialization(OSPlatform platform, Architecture arch);
	void JdkDetection(JdkVersion version, Architecture arch);
	void JdkUnavailable(JdkVersion version, Architecture arch);
	void JdkFound(JdkVersion version, Architecture arch, String jdkPath);
	void JdkDownload(JdkVersion version, Architecture arch, String jdkPath);
}