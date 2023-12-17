namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This record stores cache for a <see cref="JVirtualMachine"/> instance.
	/// </summary>
	private sealed record JVirtualMachineCache
	{
		/// <summary>
		/// Delegates dictionary.
		/// </summary>
		private static readonly Dictionary<Type, Func<JVirtualMachineRef, IntPtr>> getPointer = new()
		{
			{ typeof(DestroyVirtualMachineDelegate), r => r.Reference.Reference.DestroyJavaVmPointer },
			{ typeof(AttachCurrentThreadDelegate), r => r.Reference.Reference.AttachCurrentThreadPointer },
			{ typeof(DetachCurrentThreadDelegate), r => r.Reference.Reference.DetachCurrentThreadPointer },
			{ typeof(GetEnvDelegate), r => r.Reference.Reference.GetEnvPointer },
			{
				typeof(AttachCurrentThreadAsDaemonDelegate),
				r => r.Reference.Reference.AttachCurrentThreadAsDaemonPointer
			},
		};

		/// <summary>
		/// Global object dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<JGlobalRef, WeakReference<JGlobal>> _globalObjects = new();
		/// <summary>
		/// Weak global object dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<JWeakRef, WeakReference<JWeak>> _weakObjects = new();

		/// <inheritdoc cref="JVirtualMachine.Reference"/>
		public JVirtualMachineRef Reference { get; }
		/// <summary>
		/// Delegate cache.
		/// </summary>
		public DelegateHelperCache DelegateCache { get; }
		/// <summary>
		/// Thread cache.
		/// </summary>
		public ThreadCache ThreadCache { get; }
		/// <summary>
		/// Global cache.
		/// </summary>
		public ClassCache<JGlobal> GlobalClassCache { get; } = new();
		/// <summary>
		/// Weak cache.
		/// </summary>
		public ClassCache WeakClassCache { get; } = new();
		/// <summary>
		/// <see cref="GlobalMainClasses"/> instance.
		/// </summary>
		public GlobalMainClasses MainClasses { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
		/// <param name="mainClasses">A <see cref="GlobalMainClasses"/> instance.</param>
		public JVirtualMachineCache(JVirtualMachineRef vmRef, GlobalMainClasses mainClasses)
		{
			this.Reference = vmRef;
			this.DelegateCache = new();
			this.ThreadCache = new(vmRef);
			this.MainClasses = this.Register(mainClasses);
		}

		/// <summary>
		/// Retrieves a <typeparamref name="TDelegate"/> instance for <typeparamref name="TDelegate"/>.
		/// </summary>
		/// <typeparam name="TDelegate">Type of method delegate.</typeparam>
		/// <returns>A <typeparamref name="TDelegate"/> instance.</returns>
		public TDelegate GetDelegate<TDelegate>() where TDelegate : Delegate
		{
			Type typeOfT = typeof(TDelegate);
			IntPtr ptr = JVirtualMachineCache.getPointer[typeOfT](this.Reference);
			return this.DelegateCache.GetDelegate<TDelegate>(ptr);
		}

		/// <summary>
		/// Registers a <see cref="GlobalMainClasses"/> in current <see cref="IVirtualMachine"/> instance.
		/// </summary>
		/// <param name="mainClasses">A <see cref="GlobalMainClasses"/> instance.</param>
		/// <returns>A <see cref="GlobalMainClasses"/> instance.</returns>
		private GlobalMainClasses Register(GlobalMainClasses mainClasses)
		{
			this.GlobalClassCache[mainClasses.ClassMetadata.Hash] = mainClasses.ClassObject;
			this.GlobalClassCache[mainClasses.ThrowableMetadata.Hash] = mainClasses.ThrowableObject;
			this.GlobalClassCache[mainClasses.StackTraceElementMetadata.Hash] = mainClasses.StackTraceElementObject;
			return mainClasses;
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
			using IThread thread = JVirtualMachine.GetVirtualMachine(this.Reference)
			                                      .CreateThread(ThreadPurpose.CheckGlobalReference);
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
			using IThread thread = JVirtualMachine.GetVirtualMachine(this.Reference)
			                                      .CreateThread(ThreadPurpose.RemoveGlobalReference);
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