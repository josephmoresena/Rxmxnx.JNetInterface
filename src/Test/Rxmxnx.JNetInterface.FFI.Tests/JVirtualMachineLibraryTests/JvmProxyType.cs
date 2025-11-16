namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class JVirtualMachineLibraryTests
{
	[Flags]
	internal enum JvmProxyType : Byte
	{
		Complete,
		NoCreate = 0x1,
		NoVMs = 0x2,
		NoCreateAndNoVMs = JvmProxyType.NoCreate | JvmProxyType.NoVMs,
	}
}