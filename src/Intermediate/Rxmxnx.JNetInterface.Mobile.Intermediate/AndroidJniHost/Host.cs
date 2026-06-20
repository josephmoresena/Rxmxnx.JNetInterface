namespace Rxmxnx.JNetInterface;

public sealed partial class AndroidJniHost : IVirtualMachineHost
{
	Boolean IVirtualMachineHost.IsOwned() => JniEnvironment.EnvironmentPointer == IntPtr.Zero;

	IVirtualMachine IWrapper<IVirtualMachine>.Value => this;
	Boolean IVirtualMachineHost.IsRunning => JniRuntime.GetRegisteredRuntime(this._core.Reference.Pointer) is not null;
	IGlobalObjectManager IVirtualMachineHost.GlobalManager => this._core;
	INativeMemoryManager IVirtualMachineHost.MemoryManager => this._core;
	ITypeManager IVirtualMachineHost.TypeManager => this;
	GlobalMainClasses IVirtualMachineHost.MainClasses => this._core;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JResult IVirtualMachineHost.GetEnv(out JEnvironmentRef envRef, Int32 jniVersion)
		=> this._core.GetEnv(out envRef, jniVersion);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JResult IVirtualMachineHost.AttachThread(Boolean isDaemon, VirtualMachineArgumentValue arg,
		out JEnvironmentRef envRef)
		=> this._core.AttachThread(isDaemon, arg, out envRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IVirtualMachineHost.FinalizeThread(JEnvironmentRef envRef, ILocalCacheOwner owner, Thread? thread)
		=> this._core.FinalizeThread(envRef, owner, thread);
}