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

	/// <summary>
	/// Detaches current thread from <see cref="IVirtualMachine"/> referenced by <paramref name="core"/>.
	/// </summary>
	/// <param name="core">A <see cref="VirtualMachineCore"/> reference.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="thread">A <see cref="Thread"/> instance.</param>
	public static void DetachCurrentThread(VirtualMachineCore? core, JEnvironmentRef envRef, Thread thread)
	{
		ImplementationValidationUtilities.ThrowIfDifferentThread(envRef, thread);
		JResult result = core?.GetInvokeInterface().DetachCurrentThread(core.Reference) ?? JResult.DetachedThreadError;
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
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