namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to use Java library through Invocation API.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct InvocationFunctionSet
{
	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>JNI_GetDefaultJavaVMInitArgs</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg)
		=> OperatingSystem.IsWindows() ?
			this._windows.GetDefaultVirtualMachineInitArgs(ref initArg) :
			this._unix.GetDefaultVirtualMachineInitArgs(ref initArg);
	/// <summary>
	/// <c>JNI_CreateJavaVM</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue initArg)
		=> OperatingSystem.IsWindows() ?
			this._windows.CreateVirtualMachine(out vmRef, out envRef, in initArg) :
			this._unix.CreateVirtualMachine(out vmRef, out envRef, in initArg);
	/// <summary>
	/// <c>JNI_GetCreatedJavaVMs</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult GetCreatedVirtualMachines(JVirtualMachineRef* arr, Int32 arrSize, out Int32 count)
		=> OperatingSystem.IsWindows() ?
			this._windows.GetCreatedVirtualMachines(arr, arrSize, out count) :
			this._unix.GetCreatedVirtualMachines(arr, arrSize, out count);

	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>JNI_GetDefaultJavaVMInitArgs</c> exported function.
		/// Returns a default configuration for the Java VM.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<ref VirtualMachineInitArgumentValue, JResult>
			GetDefaultVirtualMachineInitArgs;
		/// <summary>
		/// Pointer to <c>JNI_CreateJavaVM</c> exported function.
		/// Loads and initializes a Java VM. The current thread becomes the main thread.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<out JVirtualMachineRef, out JEnvironmentRef, in
			VirtualMachineInitArgumentValue, JResult> CreateVirtualMachine;
		/// <summary>
		/// Pointer to <c>JNI_GetCreatedJavaVMs</c> exported function.
		/// Returns all Java VMs that have been created.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef*, Int32, out Int32, JResult>
			GetCreatedVirtualMachines;
	}

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>JNI_GetDefaultJavaVMInitArgs</c> exported function.
		/// Returns a default configuration for the Java VM.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<ref VirtualMachineInitArgumentValue, JResult>
			GetDefaultVirtualMachineInitArgs;
		/// <summary>
		/// Pointer to <c>JNI_CreateJavaVM</c> exported function.
		/// Loads and initializes a Java VM. The current thread becomes the main thread.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<out JVirtualMachineRef, out JEnvironmentRef, in
			VirtualMachineInitArgumentValue, JResult> CreateVirtualMachine;
		/// <summary>
		/// Pointer to <c>JNI_GetCreatedJavaVMs</c> exported function.
		/// Returns all Java VMs that have been created.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef*, Int32, out Int32, JResult>
			GetCreatedVirtualMachines;
	}
}