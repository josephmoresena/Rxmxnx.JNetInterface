namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Core object for a <see cref="IVirtualMachine"/> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
                 Justification = CommonConstants.NoMethodOverloadingJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal abstract partial class VirtualMachineCore : GlobalMainClasses
{
	/// <summary>
	/// <see cref="NativeCache"/> instance.
	/// </summary>
	public readonly NativeCache NativesCache = new();
	/// <inheritdoc cref="IVirtualMachine.Reference"/>
	public readonly JVirtualMachineRef Reference;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	protected VirtualMachineCore(IVirtualMachineHost host, JVirtualMachineRef vmRef) : base(host)
	{
		this.VirtualMachine = host.Value;
		this.Reference = vmRef;
	}

	/// <summary>
	/// Retrieves managed <see cref="InvokeInterface"/> reference from current instance.
	/// </summary>
	/// <returns>A managed <see cref="InvokeInterface"/> reference from current instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe ref readonly InvokeInterface GetInvokeInterface()
		=> ref *(InvokeInterface*)this.Reference.InterfacePointer;

	/// <inheritdoc cref="IVirtualMachineHost.GetEnv(out JEnvironmentRef, Int32)"/>
	public JResult GetEnv(out JEnvironmentRef envRef, Int32 jniVersion)
	{
		ref readonly InvokeInterface invoke = ref this.GetInvokeInterface();
		return invoke.GetEnv(this.Reference, out envRef, (Int32)JRuntimeVersion.SEd2);
	}
	/// <inheritdoc cref="IVirtualMachineHost.AttachThread(Boolean, VirtualMachineArgumentValue, out JEnvironmentRef)"/>
	public JResult AttachThread(Boolean isDaemon, VirtualMachineArgumentValue arg, out JEnvironmentRef envRef)
	{
		ref readonly InvokeInterface invoke = ref this.GetInvokeInterface();
		return !isDaemon ?
			invoke.AttachCurrentThread(this.Reference, out envRef, in arg) :
			invoke.AttachCurrentThreadAsDaemon(this.Reference, out envRef, in arg);
	}
	/// <inheritdoc cref="ITypeManager.ReloadAccess(String)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public void ReloadAccess(String classHash)
	{
		if (!this.GlobalClassCache.TryGetValue(classHash, out JGlobal? jGlobal) || jGlobal.IsDefault) return;
		this.GlobalClassCache.Load(jGlobal.As<JClassLocalRef>());
	}
	/// <inheritdoc cref="ITypeManager.GetAccess(JClassLocalRef)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public AccessCache? GetAccess(JClassLocalRef classRef)
		=> this.GlobalClassCache[classRef] ?? this.WeakClassCache[classRef];
	/// <inheritdoc cref="ITypeManager.LoadMetadataGlobal(JGlobalBase)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public ClassObjectMetadata? LoadMetadataGlobal(JGlobalBase jGlobal)
	{
		ClassObjectMetadata? result = jGlobal.ObjectMetadata as ClassObjectMetadata;
		if (result is null || this.GlobalClassCache.ContainsHash(result.Hash)) return result;
		JTrace.LoadClassMetadata(result);
		this.CreateGlobalClass(result);
		return result;
	}
	/// <inheritdoc cref="ITypeManager.LoadGlobal(JClassObject)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JGlobal LoadGlobal(JClassObject jClass)
	{
		ObjectLifetime lifetime = jClass.Lifetime;
		Boolean found = true;
		if (!this.GlobalClassCache.TryGetValue(jClass.Hash, out JGlobal? jGlobal))
		{
			WellKnownRuntimeTypeInformation typeMetadata = MetadataHelper.GetExactMetadata(jClass.Hash);
			JTypeKind kind = jClass switch
			{
				{ IsPrimitive: true, } => JTypeKind.Primitive,
				{ ArrayDimension: > 0, } => JTypeKind.Array,
				_ => typeMetadata.Kind ?? JTypeKind.Undefined,
			};
			ClassObjectMetadata metadata = new(jClass, kind, typeMetadata.IsFinal);
			jGlobal = new(this.VirtualMachine, metadata, default);
			found = false;
			this.GlobalClassCache[jClass.Hash] = jGlobal;
		}
		lifetime.SetGlobal(jGlobal);
		JTrace.LoadGlobalClass(jClass, found, jGlobal.Reference);
		return jGlobal;
	}
	/// <inheritdoc cref="ITypeManager.GetTypeInformation(String)"/>
	public ITypeInformation? GetTypeInformation(String classHash)
	{
		ITypeInformation? result = default;
		if (this.GlobalClassCache.TryGetValue(classHash, out JGlobal? jGlobal))
			result = jGlobal.ObjectMetadata as ITypeInformation;
		JTrace.GetTypeInformation(classHash, result);
		return result;
	}
}

/// <summary>
/// This class stores cache for a <see cref="IVirtualMachine"/> instance.
/// </summary>
/// <typeparam name="TThread">A CLR <see cref="INativeThread{TThread}"/> type.</typeparam>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
                 Justification = CommonConstants.NoMethodOverloadingJustification)]
#endif
internal sealed class VirtualMachineCore<TThread> : VirtualMachineCore where TThread : class, INativeThread<TThread>
{
	/// <summary>
	/// Thread cache.
	/// </summary>
	public readonly ThreadCache<TThread> ThreadCache;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	public VirtualMachineCore(IVirtualMachineHost host, JVirtualMachineRef vmRef) : base(host, vmRef)
		=> this.ThreadCache = new(host);

	/// <summary>
	/// Clears cache.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3971,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	public void ClearCache()
	{
		this.GlobalClassCache.Clear();
		this.WeakClassCache.Clear();

		using IThread thread = this.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		if (thread is not INativeThread env) return;

#pragma warning disable CA1816
		foreach (KeyValuePair<JWeakRef, WeakReference<JWeak>> kvp in this.WeakObjects)
		{
			if (!this.WeakObjects.TryRemove(kvp.Key, out WeakReference<JWeak>? weak)) continue;
			if (!weak.TryGetTarget(out JWeak? jWeak) || jWeak.IsDefault) continue;
			env.MemoryManager.DeleteWeakGlobalRef(jWeak.Reference);
			jWeak.ClearValue();
			GC.SuppressFinalize(jWeak);
		}
		foreach (KeyValuePair<JGlobalRef, WeakReference<JGlobal>> kvp in this.GlobalObjects)
		{
			if (!this.GlobalObjects.TryRemove(kvp.Key, out WeakReference<JGlobal>? weak)) continue;
			if (!weak.TryGetTarget(out JGlobal? jGlobal) || jGlobal.IsDefault) continue;
			env.MemoryManager.DeleteGlobalRef(jGlobal.Reference);
			jGlobal.ClearValue();
			GC.SuppressFinalize(jGlobal);
		}
#pragma warning restore CA1816
	}
	/// <summary>
	/// Initialize main classes.
	/// </summary>
	public void InitializeClasses()
	{
		using IThread thread = this.ThreadCache.AttachThread(ThreadPurpose.CreateGlobalReference);
		if (thread is IMainClassLoader mainClassLoader)
			this.LoadMainClasses(mainClassLoader);
	}
	/// <summary>
	/// Attaches current thread to VM.
	/// </summary>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns>A <see cref="IThread"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IThread AttachThread(ThreadCreationArgs args) => this.ThreadCache.AttachThread(args);
	/// <summary>
	/// Attaches the current thread to the virtual machine for <paramref name="purpose"/>.
	/// </summary>
	/// <param name="isAlive">Indicates whether the current virtual machine is alive.</param>
	/// <param name="purpose">The purpose of requested thread.</param>
	/// <returns>A <see cref="IThread"/> instance for given purpose.</returns>
	public IThread CreateThread(Boolean isAlive, ThreadPurpose purpose)
	{
		if (!isAlive) return new DeadThread(this.VirtualMachine);
		ThreadCreationArgs args = ThreadCreationArgs.Create(purpose);
		try
		{
			return this.AttachThread(args);
		}
		catch (Exception)
		{
			switch (purpose)
			{
				case ThreadPurpose.ReleaseSequence:
				case ThreadPurpose.RemoveGlobalReference:
				case ThreadPurpose.CheckGlobalReference:
				case ThreadPurpose.CheckAssignability:
				case ThreadPurpose.SynchronizeGlobalReference:
					return new DeadThread(this.VirtualMachine);
				case ThreadPurpose.ExceptionExecution:
				case ThreadPurpose.CreateGlobalReference:
				case ThreadPurpose.FatalError:
				case ThreadPurpose.GetRuntimeVersion:
				default:
					throw;
			}
		}
	}
	/// <inheritdoc cref="IVirtualMachineHost.FinalizeThread(JEnvironmentRef, ILocalCacheOwner, Thread)"/>
	public void FinalizeThread(JEnvironmentRef envRef, ILocalCacheOwner owner, Thread? thread)
	{
		this.ThreadCache.Remove(envRef);
		owner.FreeReferences();
		if (thread is not null && thread.IsAlive)
			VirtualMachineCore.DetachCurrentThread(this, envRef, thread);
	}
}