namespace Rxmxnx.JNetInterface;

public sealed partial class AndroidJniHost : IInvokedVirtualMachine
{
	Boolean IVirtualMachine.NoProxy => true;
	JRuntimeVersion IVirtualMachine.Version => JRuntimeVersion.J6;
	Int32 IVirtualMachine.AndroidApiLevel
	{
		get
		{
			if (AndroidFeature.ApiLevel is { } apiLevel) return apiLevel;
			return AndroidJniHost.ApiLevel;
		}
	}
	JVirtualMachineRef IVirtualMachine.Reference => this._core.Reference;
	IEnvironment? IVirtualMachine.GetEnvironment() => this._core.ThreadCache.GetAttachedThread();
	IThread IVirtualMachine.CreateThread(ThreadPurpose purpose)
		=> this._core.CreateThread((this as IVirtualMachineHost).IsRunning, purpose);
	IThread IVirtualMachine.InitializeThread(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this._core.AttachThread(new() { Name = threadName, ThreadGroup = threadGroup, Version = version, });
	IThread IVirtualMachine.InitializeDaemon(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this._core.AttachThread(new()
		{
			Name = threadName,
			ThreadGroup = threadGroup,
			Version = version,
			IsDaemon = true,
		});
	void IVirtualMachine.FatalError(CString? message) => JniRuntime.CurrentRuntime.FailFast(message?.ToString());
	void IVirtualMachine.FatalError(String? message) => JniRuntime.CurrentRuntime.FailFast(message);
	void IDisposable.Dispose() => JniRuntime.CurrentRuntime.DestroyRuntime();
}