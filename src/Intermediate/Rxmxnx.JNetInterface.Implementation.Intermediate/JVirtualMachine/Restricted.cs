namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine : IVirtualMachineHost, ITypeManager
{
	IEnumerable<ITypeInformation> ITypeManager.ClassesInformation => JVirtualMachine.MainClassesInformation;
	Boolean ITypeManager.Contains(String classHash) => JVirtualMachine.userMainClasses.ContainsKey(classHash);

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	AccessCache? ITypeManager.GetAccess(JClassLocalRef classRef)
		=> this._cache.GlobalClassCache[classRef] ?? this._cache.WeakClassCache[classRef];
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	void ITypeManager.ReloadAccess(String classHash)
	{
		if (!this._cache.GlobalClassCache.TryGetValue(classHash, out JGlobal? jGlobal) || jGlobal.IsDefault) return;
		this._cache.GlobalClassCache.Load(jGlobal.As<JClassLocalRef>());
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	ClassObjectMetadata? ITypeManager.LoadMetadataGlobal(JGlobalBase jGlobal)
	{
		ClassObjectMetadata? result = jGlobal.ObjectMetadata as ClassObjectMetadata;
		if (result is null || this._cache.GlobalClassCache.ContainsHash(result.Hash)) return result;
		JTrace.LoadClassMetadata(result);
		this.CreateGlobalClass(result);
		return result;
	}
	JGlobal ITypeManager.LoadGlobal(JClassObject jClass)
	{
		ObjectLifetime lifetime = jClass.Lifetime;
		Boolean found = true;
		if (!this._cache.GlobalClassCache.TryGetValue(jClass.Hash, out JGlobal? jGlobal))
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
			this._cache.GlobalClassCache[jClass.Hash] = jGlobal;
		}
		lifetime.SetGlobal(jGlobal);
		JTrace.LoadGlobalClass(jClass, found, jGlobal.Reference);
		return jGlobal;
	}
	ITypeInformation? ITypeManager.GetTypeInformation(String classHash)
	{
		ITypeInformation? result = default;
		if (this._cache.GlobalClassCache.TryGetValue(classHash, out JGlobal? jGlobal))
			result = jGlobal.ObjectMetadata as ITypeInformation;
		JTrace.GetTypeInformation(classHash, result);
		return result;
	}
	void ITypeManager.RegisterNatives(String classHash, IReadOnlyList<JNativeCallEntry> calls)
		=> this._cache.NativesCache[classHash] = calls;
	void ITypeManager.UnregisterNatives(String classHash) => this._cache.NativesCache.Clear(classHash);

	IVirtualMachine IWrapper<IVirtualMachine>.Value => this;
	Boolean IVirtualMachineHost.IsRunning => this.IsAlive;
	IGlobalObjectManager IVirtualMachineHost.GlobalManager => this._cache;
	INativeMemoryManager IVirtualMachineHost.MemoryManager => this._cache;
	ITypeManager IVirtualMachineHost.TypeManager => this;
	GlobalMainClasses IVirtualMachineHost.MainClasses => this._cache;

	JResult IVirtualMachineHost.GetEnv(out JEnvironmentRef envRef, Int32 jniVersion)
	{
		ref readonly InvokeInterface invoke = ref this.GetInvokeInterface();
		return invoke.GetEnv(this.Reference, out envRef, (Int32)JRuntimeVersion.SEd2);
	}
	JResult IVirtualMachineHost.AttachThread(Boolean isDaemon, VirtualMachineArgumentValue arg,
		out JEnvironmentRef envRef)
	{
		ref readonly InvokeInterface invoke = ref this.GetInvokeInterface();
		return !isDaemon ?
			invoke.AttachCurrentThread(this._cache.Reference, out envRef, in arg) :
			invoke.AttachCurrentThreadAsDaemon(this._cache.Reference, out envRef, in arg);
	}
}