namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to use Java library through Invocation API.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct InvocationFunctionSet
{
	/// <summary>
	/// Pointer to <c>JNI_GetDefaultJavaVMInitArgs</c> exported function.
	/// Returns a default configuration for the Java VM.
	/// </summary>
	public readonly delegate* unmanaged<ref VirtualMachineInitArgumentValue, JResult> GetDefaultVirtualMachineInitArgs;
	/// <summary>
	/// Pointer to <c>JNI_CreateJavaVM</c> exported function.
	/// Loads and initializes a Java VM. The current thread becomes the main thread.
	/// </summary>
	public readonly delegate* unmanaged<out JVirtualMachineRef, out JEnvironmentRef, in VirtualMachineInitArgumentValue,
		JResult> CreateVirtualMachine;
	/// <summary>
	/// Pointer to <c>JNI_GetCreatedJavaVMs</c> exported function.
	/// Returns all Java VMs that have been created.
	/// </summary>
	public readonly delegate* unmanaged<ValPtr<JVirtualMachineRef>, Int32, out Int32, JResult>
		GetCreatedVirtualMachines;
}