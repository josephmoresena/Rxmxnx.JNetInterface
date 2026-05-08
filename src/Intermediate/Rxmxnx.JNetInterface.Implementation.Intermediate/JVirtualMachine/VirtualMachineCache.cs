namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This class stores cache for a <see cref="JVirtualMachine"/> instance.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
#endif
	private sealed partial class VirtualMachineCache : GlobalMainClasses
	{
		/// <summary>
		/// <see cref="NativeCache"/> instance.
		/// </summary>
		public readonly NativeCache NativesCache = new();
		/// <inheritdoc cref="JVirtualMachine.Reference"/>
		public readonly JVirtualMachineRef Reference;
		/// <summary>
		/// Thread cache.
		/// </summary>
		public readonly ThreadCache<JEnvironment> ThreadCache;
		/// <summary>
		/// Weak cache.
		/// </summary>
		public readonly ClassCache WeakClassCache = new(JReferenceType.WeakGlobalRefType);

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
		public VirtualMachineCache(JVirtualMachine vm, JVirtualMachineRef vmRef) : base(vm)
		{
			this._vm = vm;
			this.Reference = vmRef;
			this.ThreadCache = new(vm);
		}

		/// <summary>
		/// Retrieves managed <see cref="InvokeInterface"/> reference from current instance.
		/// </summary>
		/// <returns>A managed <see cref="InvokeInterface"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref readonly InvokeInterface GetInvokeInterface()
			=> ref *(InvokeInterface*)this.Reference.InterfacePointer;
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

			using IThread thread = this._vm.CreateThread(ThreadPurpose.RemoveGlobalReference);
			if (thread is not JEnvironment env) return;

#pragma warning disable CA1816
			foreach (KeyValuePair<JWeakRef, WeakReference<JWeak>> kvp in this._weakObjects)
			{
				if (!this._weakObjects.TryRemove(kvp.Key, out WeakReference<JWeak>? weak)) continue;
				if (!weak.TryGetTarget(out JWeak? jWeak) || jWeak.IsDefault) continue;
				env.DeleteWeakGlobalRef(jWeak.Reference);
				jWeak.ClearValue();
				GC.SuppressFinalize(jWeak);
			}
			foreach (KeyValuePair<JGlobalRef, WeakReference<JGlobal>> kvp in this._globalObjects)
			{
				if (!this._globalObjects.TryRemove(kvp.Key, out WeakReference<JGlobal>? weak)) continue;
				if (!weak.TryGetTarget(out JGlobal? jGlobal) || jGlobal.IsDefault) continue;
				env.DeleteGlobalRef(jGlobal.Reference);
				jGlobal.ClearValue();
				GC.SuppressFinalize(jGlobal);
			}
#pragma warning restore CA1816
		}
	}
}