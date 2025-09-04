namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JInvokeInterface"/> type.
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

	/// <inheritdoc cref="JInvokeInterface.DestroyJavaVmPointer"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult DestroyVirtualMachine(JVirtualMachineRef vmRef)
		=> OperatingSystem.IsWindows() ?
			this._windows.DestroyVirtualMachine(vmRef) :
			this._unix.DestroyVirtualMachine(vmRef);
	/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadPointer"/>
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
	/// <inheritdoc cref="JInvokeInterface.DetachCurrentThreadPointer"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult DetachCurrentThread(JVirtualMachineRef vmRef)
		=> OperatingSystem.IsWindows() ?
			this._windows.DetachCurrentThread(vmRef) :
			this._unix.DetachCurrentThread(vmRef);
	/// <inheritdoc cref="JInvokeInterface.GetEnvPointer"/>
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
	/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadAsDaemonPointer"/>
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
		private readonly JInvokeInterface.ComReserved _reserved;
#pragma warning restore CS0169

		/// <inheritdoc cref="JInvokeInterface.DestroyJavaVmPointer"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JResult> DestroyVirtualMachine;
		/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadPointer"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JEnvironmentRef*, VirtualMachineArgumentValue*,
			JResult > AttachCurrentThread;
		/// <inheritdoc cref="JInvokeInterface.DetachCurrentThreadPointer"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JResult> DetachCurrentThread;
		/// <inheritdoc cref="JInvokeInterface.GetEnvPointer"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JEnvironmentRef*, Int32, JResult> GetEnv;
		/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadAsDaemonPointer"/>
		public readonly delegate* unmanaged[Stdcall]<JVirtualMachineRef, JEnvironmentRef*, VirtualMachineArgumentValue*,
			JResult > AttachCurrentThreadAsDaemon;
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
		private readonly JInvokeInterface.ComReserved _reserved;
#pragma warning restore CS0169

		/// <inheritdoc cref="JInvokeInterface.DestroyJavaVmPointer"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JResult> DestroyVirtualMachine;
		/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadPointer"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JEnvironmentRef*, VirtualMachineArgumentValue*,
			JResult > AttachCurrentThread;
		/// <inheritdoc cref="JInvokeInterface.DetachCurrentThreadPointer"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JResult> DetachCurrentThread;
		/// <inheritdoc cref="JInvokeInterface.GetEnvPointer"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JEnvironmentRef*, Int32, JResult> GetEnv;
		/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadAsDaemonPointer"/>
		public readonly delegate* unmanaged[Cdecl]<JVirtualMachineRef, JEnvironmentRef*, VirtualMachineArgumentValue*,
			JResult > AttachCurrentThreadAsDaemon;
	}
}