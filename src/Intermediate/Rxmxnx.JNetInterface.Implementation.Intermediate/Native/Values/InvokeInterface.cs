namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNIInvokeInterface_</c> struct. Contains all pointers to the functions of the Invocation API.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct InvokeInterface
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
	/// Pointer to <c>DestroyJavaVM</c> function. Unloads a JVM and reclaims its resources.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult DestroyVirtualMachine(JVirtualMachineRef vmRef)
		=> OperatingSystem.IsWindows() ?
			this._windows.DestroyVirtualMachine(vmRef) :
			this._unix.DestroyVirtualMachine(vmRef);
	/// <summary>
	/// Pointer to <c>AttachCurrentThread</c> function. Attaches the current thread to a JVM.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult AttachCurrentThread(JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineArgumentValue vmArg)
	{
		fixed (JEnvironmentRef* envRefPtr = &envRef)
		fixed (VirtualMachineArgumentValue* vmArgPtr = &vmArg)
		{
			return OperatingSystem.IsWindows() ?
				this._windows.AttachCurrentThread(vmRef, envRefPtr, vmArgPtr) :
				this._unix.AttachCurrentThread(vmRef, envRefPtr, vmArgPtr);
		}
	}
	/// <summary>
	/// Pointer to <c>DetachCurrentThread</c> function. Detaches the current thread from a JVM.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult DetachCurrentThread(JVirtualMachineRef vmRef)
		=> OperatingSystem.IsWindows() ?
			this._windows.DetachCurrentThread(vmRef) :
			this._unix.DetachCurrentThread(vmRef);
	/// <summary>
	/// Pointer to <c>GetEnv</c> function. Retrieves the <c>JNIEnv</c> pointer for the current thread.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult GetEnv(JVirtualMachineRef vmRef, out JEnvironmentRef envRef, Int32 jniVersion)
	{
		fixed (JEnvironmentRef* envRefPtr = &envRef)
		{
			return OperatingSystem.IsWindows() ?
				this._windows.GetEnv(vmRef, envRefPtr, jniVersion) :
				this._unix.GetEnv(vmRef, envRefPtr, jniVersion);
		}
	}
	/// <summary>
	/// Pointer to <c>AttachCurrentThreadAsDaemon</c> function. Same as AttachCurrentThread, but the
	/// newly-created <c>java.lang.Thread</c> instance is a daemon.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult AttachCurrentThreadAsDaemon(JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineArgumentValue vmArg)
	{
		fixed (JEnvironmentRef* envRefPtr = &envRef)
		fixed (VirtualMachineArgumentValue* vmArgPtr = &vmArg)
		{
			return OperatingSystem.IsWindows() ?
				this._windows.AttachCurrentThreadAsDaemon(vmRef, envRefPtr, vmArgPtr) :
				this._unix.AttachCurrentThreadAsDaemon(vmRef, envRefPtr, vmArgPtr);
		}
	}

	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Internal reserved entries.
		/// </summary>
#pragma warning disable CS0169
		private readonly ComReserved _reserved;
#pragma warning restore CS0169

		/// <inheritdoc cref="InvokeInterface.DestroyVirtualMachine"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JResult> DestroyVirtualMachine;
		/// <inheritdoc cref="InvokeInterface.AttachCurrentThread"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JEnvironmentRef*, VirtualMachineArgumentValue*,
			JResult> AttachCurrentThread;
		/// <inheritdoc cref="InvokeInterface.DetachCurrentThread"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JResult> DetachCurrentThread;
		/// <inheritdoc cref="InvokeInterface.GetEnv"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JEnvironmentRef*, Int32, JResult> GetEnv;
		/// <inheritdoc cref="InvokeInterface.AttachCurrentThreadAsDaemon"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JEnvironmentRef*, VirtualMachineArgumentValue*,
			JResult> AttachCurrentThreadAsDaemon;
	}

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Internal reserved entries.
		/// </summary>
#pragma warning disable CS0169
		private readonly ComReserved _reserved;
#pragma warning restore CS0169

		/// <inheritdoc cref="InvokeInterface.DestroyVirtualMachine"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JResult> DestroyVirtualMachine;
		/// <inheritdoc cref="InvokeInterface.AttachCurrentThread"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JEnvironmentRef*, VirtualMachineArgumentValue*,
			JResult> AttachCurrentThread;
		/// <inheritdoc cref="InvokeInterface.DetachCurrentThread"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JResult> DetachCurrentThread;
		/// <inheritdoc cref="InvokeInterface.GetEnv"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JEnvironmentRef*, Int32, JResult> GetEnv;
		/// <inheritdoc cref="InvokeInterface.AttachCurrentThreadAsDaemon"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JEnvironmentRef*, VirtualMachineArgumentValue*,
			JResult> AttachCurrentThreadAsDaemon;
	}

	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatibility.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct ComReserved
	{
#pragma warning disable CS0169
		private IntPtr _reserved0;
		private IntPtr _reserved1;
		private IntPtr _reserved2;
#pragma warning restore CS0169
	}
}