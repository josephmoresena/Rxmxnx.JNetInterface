namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JInvokeInterface"/> type.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct InvokeInterface
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
#pragma warning disable CS0169
	private readonly JInvokeInterface.ComReserved _reserved;
#pragma warning restore CS0169

	/// <inheritdoc cref="JInvokeInterface.DestroyJavaVmPointer"/>
	private readonly delegate* unmanaged<IntPtr, Int32> _destroyVirtualMachine;
	/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadPointer"/>
	private readonly delegate* unmanaged<IntPtr, void*, void*, Int32 > _attachCurrentThread;
	/// <inheritdoc cref="JInvokeInterface.DetachCurrentThreadPointer"/>
	private readonly delegate* unmanaged<IntPtr, Int32> _detachCurrentThread;
	/// <inheritdoc cref="JInvokeInterface.GetEnvPointer"/>
	private readonly delegate* unmanaged<IntPtr, void*, Int32, Int32> _getEnv;
	/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadAsDaemonPointer"/>
	private readonly delegate* unmanaged<IntPtr, void*, void*, Int32 > _attachCurrentThreadAsDaemon;

	/// <inheritdoc cref="JInvokeInterface.DestroyJavaVmPointer"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult DestroyVirtualMachine(JVirtualMachineRef vmRef)
		=> (JResult)this._destroyVirtualMachine(vmRef.Pointer);
	/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadPointer"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult AttachCurrentThread(JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineArgumentValue vmArg)
	{
		fixed (void* envRefPtr = &envRef)
		fixed (void* vmArgPtr = &vmArg)
			return (JResult)this._attachCurrentThread(vmRef.Pointer, envRefPtr, vmArgPtr);
	}
	/// <inheritdoc cref="JInvokeInterface.DetachCurrentThreadPointer"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult DetachCurrentThread(JVirtualMachineRef vmRef) => (JResult)this._destroyVirtualMachine(vmRef.Pointer);
	/// <inheritdoc cref="JInvokeInterface.GetEnvPointer"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult GetEnv(JVirtualMachineRef vmRef, out JEnvironmentRef envRef, Int32 count)
	{
		fixed (void* envRefPtr = &envRef)
			return (JResult)this._getEnv(vmRef.Pointer, envRefPtr, count);
	}
	/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadAsDaemonPointer"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult AttachCurrentThreadAsDaemon(JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineArgumentValue vmArg)
	{
		fixed (void* envRefPtr = &envRef)
		fixed (void* vmArgPtr = &vmArg)
			return (JResult)this._attachCurrentThreadAsDaemon(vmRef.Pointer, envRefPtr, vmArgPtr);
	}
}