namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Exposes an P/Invoke type implementing the <c>jvm</c> library.
/// </summary>
public interface IVirtualMachineLibraryType
{
	/// <summary>
	/// Name of <c>JNI_GetDefaultJavaVMInitArgs</c> function.
	/// </summary>
	public const String GetDefaultVirtualMachineInitArgsSymbol = "JNI_GetDefaultJavaVMInitArgs";
	/// <summary>
	/// Name of <c>JNI_CreateJavaVM</c> function.
	/// </summary>
	public const String CreateVirtualMachineSymbol = "JNI_CreateJavaVM";
	/// <summary>
	/// Name of <c>JNI_GetCreatedJavaVMs</c> function.
	/// </summary>
	public const String GetCreatedVirtualMachinesSymbol = "JNI_GetCreatedJavaVMs";

	/// <summary>
	/// Indicates whether the current <c>jvm</c> library is statically linked.
	/// </summary>
	static virtual Boolean IsStatic => false;
	/// <summary>
	/// Indicates whether the function <c>JNI_GetCreatedJavaVMs</c> is available on the current library.
	/// </summary>
	static virtual Boolean HasCreatedVmMethod => true;

	/// <summary>
	/// <c>JNI_GetDefaultJavaVMInitArgs</c>.
	/// </summary>
	/// <param name="initArg">Reference. Initial <see cref="VirtualMachineInitArgumentValue"/> instance.</param>
	static abstract JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg);
	/// <summary>
	/// <c>JNI_CreateJavaVM</c>.
	/// </summary>
	/// <param name="vmRef">Output. Initial <see cref="JVirtualMachineRef"/> instance.</param>
	/// <param name="envRef">Output. Initialized <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="initArg">Input. Initial <see cref="VirtualMachineInitArgumentValue"/> instance.</param>
	static abstract JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue initArg);
	/// <summary>
	/// <c>JNI_GetCreatedJavaVMs</c>.
	/// </summary>
	/// <param name="arr">Pointer to <see cref="JVirtualMachineRef"/> buffer.</param>
	/// <param name="arrSize">Length buffer.</param>
	/// <param name="count">Output. Total virtual machines.</param>
	static abstract JResult GetCreatedVirtualMachines(ValPtr<JVirtualMachineRef> arr, Int32 arrSize, out Int32 count);
}