using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public interface IPlatformNotifier
{
	void BeginDetection();
	void EndDetection(OSPlatform platform, Architecture arch);
	void Initialization(OSPlatform platform, Architecture arch);
	void JdkDetection(Jdk.JdkVersion version, Architecture arch);
	void JdkUnavailable(Jdk.JdkVersion version, Architecture arch);
	void JdkFound(Jdk.JdkVersion version, Architecture arch, String jdkPath);
	void JdkDownload(Jdk.JdkVersion version, Architecture arch, String jdkPath);
}