namespace Rxmxnx.JNetInterface.Tests;

public abstract class InvokeInterfaceProxy
{
	public abstract JResult DestroyVirtualMachine();
	public abstract JResult AttachCurrentThread(ValPtr<JEnvironmentRef> envRef,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper> arg);
	public abstract JResult DetachCurrentThread();
	public abstract JResult GetEnv(ValPtr<JEnvironmentRef> envRef, Int32 version);
	public abstract JResult AttachCurrentThreadAsDaemon(ValPtr<JEnvironmentRef> envRef,
		ReadOnlyValPtr<VirtualMachineArgumentValueWrapper> arg);
}