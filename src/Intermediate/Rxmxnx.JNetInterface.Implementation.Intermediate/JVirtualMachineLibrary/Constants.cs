namespace Rxmxnx.JNetInterface;

public sealed partial class JVirtualMachineLibrary
{
	/// <summary>
	/// Name of <c>JNI_GetDefaultJavaVMInitArgs</c> function.
	/// </summary>
	private const String getDefaultVirtualMachineInitArgsName = "JNI_GetDefaultJavaVMInitArgs";
	/// <summary>
	/// Name of <c>JNI_CreateJavaVM</c> function.
	/// </summary>
	private const String createVirtualMachineName = "JNI_CreateJavaVM";
	/// <summary>
	/// Name of <c>JNI_GetCreatedJavaVMs</c> function.
	/// </summary>
	private const String getCreatedVirtualMachinesName = "JNI_GetCreatedJavaVMs";

	/// <summary>
	/// Support JNI versions.
	/// </summary>
	private static readonly Int32[] jniVersions =
	[
		0x00010006, //JNI_VERSION_1_6
		0x00010008, //JNI_VERSION_1_8
		0x00090000, //JNI_VERSION_9
		0x000a0000, //JNI_VERSION_10
		0x00130000, //JNI_VERSION_19
		0x00140000, //JNI_VERSION_20
		0x00150000, //JNI_VERSION_21
		0x00180000, //JNI_VERSION_24
	];
}