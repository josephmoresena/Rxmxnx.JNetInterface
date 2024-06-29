namespace Rxmxnx.JNetInterface.Tests.Internal;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal unsafe struct InternalInvokeInterface
{
#pragma warning disable CS0169
	private readonly JInvokeInterface.ComReserved _reserved;
#pragma warning restore CS0169

	public delegate* unmanaged<JVirtualMachineRef, JResult> DestroyVirtualMachine;
	public delegate* unmanaged<JVirtualMachineRef, ValPtr<JEnvironmentRef>,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>, JResult> AttachCurrentThread;
	public delegate* unmanaged<JVirtualMachineRef, JResult> DetachCurrentThread;
	public delegate* unmanaged<JVirtualMachineRef, ValPtr<JEnvironmentRef>, Int32, JResult> GetEnv;
	public delegate* unmanaged<JVirtualMachineRef, ValPtr<JEnvironmentRef>,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>, JResult> AttachCurrentThreadAsDaemon;
}