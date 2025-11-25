namespace Rxmxnx.JNetInterface.Tests;

public abstract class LibraryBaseTests
{
	[ThreadStatic]
	private protected static Exception? NativeException;

	private protected delegate JResult GetDefaultVirtualMachineInitArgsDelegate(
		ref VirtualMachineInitArgumentValue initArg);

	private protected delegate JResult CreateVirtualMachineDelegate(out JVirtualMachineRef vmRef,
		out JEnvironmentRef envRef, in VirtualMachineInitArgumentValue initArg);

	private protected delegate JResult GetCreatedJavaVMsDelegate(ValPtr<JVirtualMachineRef> arr, Int32 arrSize,
		out Int32 count);
}