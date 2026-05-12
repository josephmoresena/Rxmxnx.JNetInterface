namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine : IVirtualMachineHost, ITypeManager
{
	IEnumerable<ITypeInformation> ITypeManager.ClassesInformation => JVirtualMachine.MainClassesInformation;
	Boolean ITypeManager.Contains(String classHash) => JVirtualMachine.userMainClasses.ContainsKey(classHash);

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	AccessCache? ITypeManager.GetAccess(JClassLocalRef classRef) => this._core.GetAccess(classRef);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ITypeManager.ReloadAccess(String classHash) => this._core.ReloadAccess(classHash);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	ClassObjectMetadata? ITypeManager.LoadMetadataGlobal(JGlobalBase jGlobal) => this._core.LoadMetadataGlobal(jGlobal);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JGlobal ITypeManager.LoadGlobal(JClassObject jClass) => this._core.LoadGlobal(jClass);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	ITypeInformation? ITypeManager.GetTypeInformation(String classHash) => this._core.GetTypeInformation(classHash);
	void ITypeManager.RegisterNatives(String classHash, IReadOnlyList<JNativeCallEntry> calls)
		=> this._core.NativesCache[classHash] = calls;
	void ITypeManager.UnregisterNatives(String classHash) => this._core.NativesCache.Clear(classHash);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	LocalCache? IVirtualMachineHost.GetInitialCache(INativeThread _, ClassCache __) => default;

	IVirtualMachine IWrapper<IVirtualMachine>.Value => this;
	Boolean IVirtualMachineHost.IsRunning => this.IsAlive;
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