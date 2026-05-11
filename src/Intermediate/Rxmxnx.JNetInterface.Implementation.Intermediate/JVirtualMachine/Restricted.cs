namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine : IVirtualMachineHost, ITypeManager
{
	IEnumerable<ITypeInformation> ITypeManager.ClassesInformation => JVirtualMachine.MainClassesInformation;
	Boolean ITypeManager.Contains(String classHash) => JVirtualMachine.userMainClasses.ContainsKey(classHash);

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	AccessCache? ITypeManager.GetAccess(JClassLocalRef classRef)
		=> this._core.GlobalClassCache[classRef] ?? this._core.WeakClassCache[classRef];
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	void ITypeManager.ReloadAccess(String classHash)
	{
		if (!this._core.GlobalClassCache.TryGetValue(classHash, out JGlobal? jGlobal) || jGlobal.IsDefault) return;
		this._core.GlobalClassCache.Load(jGlobal.As<JClassLocalRef>());
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	ClassObjectMetadata? ITypeManager.LoadMetadataGlobal(JGlobalBase jGlobal)
	{
		ClassObjectMetadata? result = jGlobal.ObjectMetadata as ClassObjectMetadata;
		if (result is null || this._core.GlobalClassCache.ContainsHash(result.Hash)) return result;
		JTrace.LoadClassMetadata(result);
		this._core.CreateGlobalClass(result);
		return result;
	}
	JGlobal ITypeManager.LoadGlobal(JClassObject jClass)
	{
		ObjectLifetime lifetime = jClass.Lifetime;
		Boolean found = true;
		if (!this._core.GlobalClassCache.TryGetValue(jClass.Hash, out JGlobal? jGlobal))
		{
			WellKnownRuntimeTypeInformation typeMetadata = MetadataHelper.GetExactMetadata(jClass.Hash);
			JTypeKind kind = jClass switch
			{
				{ IsPrimitive: true, } => JTypeKind.Primitive,
				{ ArrayDimension: > 0, } => JTypeKind.Array,
				_ => typeMetadata.Kind ?? JTypeKind.Undefined,
			};
			ClassObjectMetadata metadata = new(jClass, kind, typeMetadata.IsFinal);
			jGlobal = new(this, metadata, default);
			found = false;
			this._core.GlobalClassCache[jClass.Hash] = jGlobal;
		}
		lifetime.SetGlobal(jGlobal);
		JTrace.LoadGlobalClass(jClass, found, jGlobal.Reference);
		return jGlobal;
	}
	ITypeInformation? ITypeManager.GetTypeInformation(String classHash)
	{
		ITypeInformation? result = default;
		if (this._core.GlobalClassCache.TryGetValue(classHash, out JGlobal? jGlobal))
			result = jGlobal.ObjectMetadata as ITypeInformation;
		JTrace.GetTypeInformation(classHash, result);
		return result;
	}
	void ITypeManager.RegisterNatives(String classHash, IReadOnlyList<JNativeCallEntry> calls)
		=> this._core.NativesCache[classHash] = calls;
	void ITypeManager.UnregisterNatives(String classHash) => this._core.NativesCache.Clear(classHash);

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
	void IVirtualMachineHost.FinalizeThread(JEnvironmentRef envRef, ILocalCacheOwner owner, Thread thread)
	{
		this._core.ThreadCache.Remove(envRef);
		owner.FreeReferences();
		VirtualMachineCore.DetachCurrentThread(this._core, envRef, thread);
	}
}