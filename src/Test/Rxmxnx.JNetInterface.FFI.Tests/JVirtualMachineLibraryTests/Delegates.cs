namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class JVirtualMachineLibraryTests
{
	private delegate JResult GetDefaultVirtualMachineInitArgsDelegate(ref VirtualMachineInitArgumentValue initValue);

	private delegate JResult CreateVirtualMachineDelegate(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue value);

	private delegate JResult GetCreatedJavaVMsDelegate(ValPtr<JVirtualMachineRef> vmBuf, Int32 bufLen, out Int32 nVMs);
}