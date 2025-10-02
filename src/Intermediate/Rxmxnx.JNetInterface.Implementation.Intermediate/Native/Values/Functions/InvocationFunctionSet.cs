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
	{
		fixed (VirtualMachineInitArgumentValue* initArgPtr = &initArg)
		{
			return OperatingSystem.IsWindows() ?
				this._windows.GetDefaultVirtualMachineInitArgs(initArgPtr) :
				this._unix.GetDefaultVirtualMachineInitArgs(initArgPtr);
		}
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
		{
			//TODO: Remove this.
			Console.WriteLine("Calling CreateVirtualMachine");
			JResult result = OperatingSystem.IsWindows() ?
				this._windows.CreateVirtualMachine(vmRefPtr, envRefPtr, initArgPtr) :
				this._unix.CreateVirtualMachine(vmRefPtr, envRefPtr, initArgPtr);
			Console.WriteLine($"Result CreateVirtualMachine: {result}, {vmRef}, {envRef}");
			return result;
		}
	}
	/// <summary>
	/// <c>JNI_GetCreatedJavaVMs</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult GetCreatedVirtualMachines(JVirtualMachineRef* arr, Int32 arrSize, out Int32 count)
	{
		fixed (Int32* countPtr = &count)
		{
			return OperatingSystem.IsWindows() ?
				this._windows.GetCreatedVirtualMachines(arr, arrSize, countPtr) :
				this._unix.GetCreatedVirtualMachines(arr, arrSize, countPtr);
		}
	}

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
		public readonly delegate* unmanaged[Stdcall]< VirtualMachineInitArgumentValue*, JResult>
			GetDefaultVirtualMachineInitArgs;
		/// <summary>
		/// Pointer to <c>JNI_CreateJavaVM</c> exported function.
		/// Loads and initializes a Java VM. The current thread becomes the main thread.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]< JVirtualMachineRef*, JEnvironmentRef*,
			VirtualMachineInitArgumentValue*, JResult> CreateVirtualMachine;
		/// <summary>
		/// Pointer to <c>JNI_GetCreatedJavaVMs</c> exported function.
		/// Returns all Java VMs that have been created.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef*, Int32, Int32*, JResult>
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
		public readonly delegate* unmanaged[Cdecl]<VirtualMachineInitArgumentValue*, JResult>
			GetDefaultVirtualMachineInitArgs;
		/// <summary>
		/// Pointer to <c>JNI_CreateJavaVM</c> exported function.
		/// Loads and initializes a Java VM. The current thread becomes the main thread.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef*, JEnvironmentRef*,
			VirtualMachineInitArgumentValue*, JResult> CreateVirtualMachine;
		/// <summary>
		/// Pointer to <c>JNI_GetCreatedJavaVMs</c> exported function.
		/// Returns all Java VMs that have been created.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef*, Int32, Int32*, JResult>
			GetCreatedVirtualMachines;
	}
}