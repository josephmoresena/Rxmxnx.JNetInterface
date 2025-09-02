namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to use Java library through Invocation API.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct InvocationFunctionSet
{
	/// <summary>
	/// Pointer to <c>JNI_GetDefaultJavaVMInitArgs</c> exported function.
	/// Returns a default configuration for the Java VM.
	/// </summary>
	private readonly delegate* unmanaged<void*, Int32> _getDefaultVirtualMachineInitArgs;
	/// <summary>
	/// Pointer to <c>JNI_CreateJavaVM</c> exported function.
	/// Loads and initializes a Java VM. The current thread becomes the main thread.
	/// </summary>
	private readonly delegate* unmanaged<void*, void*, void*, Int32> _createVirtualMachine;
	/// <summary>
	/// Pointer to <c>JNI_GetCreatedJavaVMs</c> exported function.
	/// Returns all Java VMs that have been created.
	/// </summary>
	public readonly delegate* unmanaged<void*, Int32, Int32*, Int32> _getCreatedVirtualMachines;

	/// <summary>
	/// <c>JNI_GetDefaultJavaVMInitArgs</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg)
	{
		fixed (VirtualMachineInitArgumentValue* initArgPtr = &initArg)
			return (JResult)this._getDefaultVirtualMachineInitArgs(initArgPtr);
	}
	/// <summary>
	/// <c>JNI_CreateJavaVM</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue initArg)
	{
		fixed (JVirtualMachineRef* vmRefPtr = &vmRef)
		fixed (JEnvironmentRef* envRefPtr = &envRef)
		fixed (VirtualMachineInitArgumentValue* initArgPtr = &initArg)
			return (JResult)this._createVirtualMachine(vmRefPtr, envRefPtr, initArgPtr);
	}
	/// <summary>
	/// <c>JNI_GetCreatedJavaVMs</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult GetCreatedVirtualMachines(JVirtualMachineRef* arr, Int32 arrSize, out Int32 count)
	{
		fixed (Int32* coutPtr = &count)
			return (JResult)this._getCreatedVirtualMachines(arr, arrSize, coutPtr);
	}
}