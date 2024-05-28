namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JInvokeInterface"/> type.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal unsafe struct InvokeInterface
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
#pragma warning disable CS0169
	private readonly JInvokeInterface.ComReserved _reserved;
#pragma warning restore CS0169

	/// <inheritdoc cref="JInvokeInterface.DestroyJavaVmPointer"/>
	public delegate* unmanaged<JVirtualMachineRef, JResult> DestroyVirtualMachine;
	/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadPointer"/>
	public delegate* unmanaged<JVirtualMachineRef, out JEnvironmentRef, in VirtualMachineArgumentValue, JResult>
		AttachCurrentThread;
	/// <inheritdoc cref="JInvokeInterface.DetachCurrentThreadPointer"/>
	public delegate* unmanaged<JVirtualMachineRef, JResult> DetachCurrentThread;
	/// <inheritdoc cref="JInvokeInterface.GetEnvPointer"/>
	public delegate* unmanaged<JVirtualMachineRef, out JEnvironmentRef, Int32, JResult> GetEnv;
	/// <inheritdoc cref="JInvokeInterface.AttachCurrentThreadAsDaemonPointer"/>
	public delegate* unmanaged<JVirtualMachineRef, out JEnvironmentRef, in VirtualMachineArgumentValue, JResult>
		AttachCurrentThreadAsDaemon;
}