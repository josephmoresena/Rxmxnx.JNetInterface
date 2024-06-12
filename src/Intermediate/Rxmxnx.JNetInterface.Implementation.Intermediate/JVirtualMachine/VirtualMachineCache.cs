namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This class stores cache for a <see cref="JVirtualMachine"/> instance.
	/// </summary>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	private sealed partial class VirtualMachineCache : GlobalMainClasses
	{
		/// <summary>
		/// Global cache.
		/// </summary>
		public readonly ClassCache<JGlobal> GlobalClassCache = new(JReferenceType.GlobalRefType);
		/// <summary>
		/// <see cref="NativeCache"/> instance.
		/// </summary>
		public readonly NativeCache NativesCache = new();
		/// <inheritdoc cref="JVirtualMachine.Reference"/>
		public readonly JVirtualMachineRef Reference;
		/// <summary>
		/// Thread cache.
		/// </summary>
		public readonly ThreadCache ThreadCache;
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
		{
			ref readonly JVirtualMachineValue refValue = ref this.Reference.Reference;
			return ref Unsafe.AsRef<InvokeInterface>(refValue.Pointer.ToPointer());
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
			if (CommonNames.ClassObject.SequenceEqual(jGlobal.ObjectMetadata.ObjectClassName))
				this.GlobalClassCache.Load(jGlobal.As<JClassLocalRef>());
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
			if (CommonNames.ClassObject.SequenceEqual(jWeak.ObjectMetadata.ObjectClassName))
				this.WeakClassCache.Load(jWeak.As<JClassLocalRef>());
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
			this.GlobalClassCache.Unload(JClassLocalRef.FromReference(in globalRef));
		}
		/// <summary>
		/// Removes <see cref="JWeakRef"/> from current cache.
		/// </summary>
		/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
		public void Remove(JWeakRef weakRef)
		{
			if (weakRef == default) return;
			this._weakObjects.Remove(weakRef, out _);
			this.WeakClassCache.Unload(JClassLocalRef.FromReference(in weakRef));
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