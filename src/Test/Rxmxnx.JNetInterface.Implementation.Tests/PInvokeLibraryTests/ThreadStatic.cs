namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class PInvokeLibraryTests
{
	[ThreadStatic]
	private static Boolean isStatic;
	[ThreadStatic]
	private static GetDefaultVirtualMachineInitArgsDelegate? getDefaultVirtualMachineInitArgs;
	[ThreadStatic]
	private static CreateVirtualMachineDelegate? createVirtualMachine;
	[ThreadStatic]
	private static GetCreatedJavaVMsDelegate? getCreatedVirtualMachines;
}