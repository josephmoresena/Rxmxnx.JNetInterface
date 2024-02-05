namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This record stores cache for a <see cref="JVirtualMachine"/> instance.
	/// </summary>
	private sealed partial record VirtualMachineCache : GlobalMainClasses
	{
		/// <inheritdoc cref="JVirtualMachine.Reference"/>
		public readonly JVirtualMachineRef Reference;
		/// <summary>
		/// Thread cache.
		/// </summary>
		public readonly ThreadCache ThreadCache;
		/// <summary>
		/// Global cache.
		/// </summary>
		public readonly ClassCache<JGlobal> GlobalClassCache = new();
		/// <summary>
		/// Weak cache.
		/// </summary>
		public readonly ClassCache WeakClassCache = new();
		/// <summary>
		/// <see cref="NativeCache"/> instance.
		/// </summary>
		public readonly NativeCache NativesCache = new();

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
		public VirtualMachineCache(JVirtualMachine vm, JVirtualMachineRef vmRef) : base(vm)
		{
			this._delegateCache = new();

			this._vm = vm;
			this.Reference = vmRef;
			this.ThreadCache = new(vm);
		}

		/// <summary>
		/// Retrieves a <typeparamref name="TDelegate"/> instance for <typeparamref name="TDelegate"/>.
		/// </summary>
		/// <typeparam name="TDelegate">Type of method delegate.</typeparam>
		/// <returns>A <typeparamref name="TDelegate"/> instance.</returns>
		public TDelegate GetDelegate<TDelegate>() where TDelegate : Delegate
		{
			Type typeOfT = typeof(TDelegate);
			IntPtr ptr = VirtualMachineCache.getPointer[typeOfT](this.Reference);
			return this._delegateCache.GetDelegate<TDelegate>(ptr);
		}
		/// <summary>
		/// Register a <see cref="JGlobal"/> instance.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
		/// <returns>Registered <see cref="JGlobal"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jGlobal))]
		public JGlobal? Register(JGlobal? jGlobal)
		{
			if (jGlobal is null || jGlobal.IsDefault) return jGlobal;
			using IThread thread = this._vm.CreateThread(ThreadPurpose.CheckGlobalReference);
			this._globalObjects[jGlobal.Reference] = new(jGlobal.GetCacheable(thread));
			return jGlobal;
		}
		/// <summary>
		/// Register a <see cref="JWeak"/> instance.
		/// </summary>
		/// <param name="jWeak">A <see cref="JWeak"/> instance.</param>
		/// <returns>Registered <see cref="JWeak"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jWeak))]
		public JWeak? Register(JWeak? jWeak)
		{
			if (jWeak is null || jWeak.IsDefault) return jWeak;
			this._weakObjects[jWeak.Reference] = new(jWeak);
			return jWeak;
		}
		/// <summary>
		/// Removes <see cref="JGlobalRef"/> from current cache.
		/// </summary>
		/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
		public void Remove(JGlobalRef globalRef)
		{
			if (globalRef == default) return;
			this._globalObjects.Remove(globalRef, out _);
			this.GlobalClassCache.Unload(NativeUtilities.Transform<JGlobalRef, JClassLocalRef>(in globalRef));
		}
		/// <summary>
		/// Removes <see cref="JWeakRef"/> from current cache.
		/// </summary>
		/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
		public void Remove(JWeakRef weakRef)
		{
			if (weakRef == default) return;
			this._weakObjects.Remove(weakRef, out _);
			this.GlobalClassCache.Unload(NativeUtilities.Transform<JWeakRef, JClassLocalRef>(in weakRef));
		}
		/// <summary>
		/// Clears cache.
		/// </summary>
		public void ClearCache()
		{
			JGlobalRef[] globalKeys = this._globalObjects.Keys.ToArray();
			JWeakRef[] weakKeys = this._weakObjects.Keys.ToArray();
			this.GlobalClassCache.Clear();
			this.WeakClassCache.Clear();
			if (globalKeys.Length == 0 && weakKeys.Length == 0) return;
			using IThread thread = this._vm.CreateThread(ThreadPurpose.RemoveGlobalReference);
			JEnvironment env = (JEnvironment)thread;
			foreach (JWeakRef key in weakKeys)
			{
				if (this._weakObjects.Remove(key, out WeakReference<JWeak>? weak) &&
				    weak.TryGetTarget(out JWeak? jWeak) && !jWeak.IsDefault)
					env.DeleteWeakGlobalRef(jWeak.Reference);
			}
			foreach (JGlobalRef key in globalKeys)
			{
				if (this._globalObjects.Remove(key, out WeakReference<JGlobal>? weak) &&
				    weak.TryGetTarget(out JGlobal? jGlobal) && !jGlobal.IsDefault)
					env.DeleteGlobalRef(jGlobal.Reference);
			}
		}
	}
}