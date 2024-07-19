namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	internal enum ClassLoadingError : Byte
	{
		None = 0x0,
		FindClass = 0x1,
		TypeIdError = 0x2,
		CreateGlobal = 0x4,
	}
}