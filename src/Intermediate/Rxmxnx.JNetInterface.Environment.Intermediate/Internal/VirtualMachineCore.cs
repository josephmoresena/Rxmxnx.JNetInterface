namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Core object for a <see cref="IVirtualMachine"/> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
                 Justification = CommonConstants.NoMethodOverloadingJustification)]
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
	/// Weak cache.
	/// </summary>
	public readonly ClassCache WeakClassCache = new(JReferenceType.WeakGlobalRefType);

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
}