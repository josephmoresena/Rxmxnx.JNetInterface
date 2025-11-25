namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class PInvokeLibraryTests
{
	private delegate JResult GetDefaultVirtualMachineInitArgsDelegate(ref VirtualMachineInitArgumentValue initArg);

	private delegate JResult CreateVirtualMachineDelegate(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue initArg);

	private delegate JResult GetCreatedVirtualMachinesDelegate(ValPtr<JVirtualMachineRef> arr, Int32 arrSize,
		out Int32 count);
}