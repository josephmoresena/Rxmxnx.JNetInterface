namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Ensure local capacity to <paramref name="capacity"/>.
		/// </summary>
		/// <param name="capacity">Top of local references.</param>
		/// <exception cref="JniException"/>
		public unsafe void EnsureLocalCapacity(Int32 capacity)
		{
			if (capacity <= 0) return;
			ImplementationValidationUtilities.ThrowIfDifferentThread(this.Reference, this.Thread);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.EnsureLocalCapacityInfo);
			JniException? jniException =
				nativeInterface.ReferenceFunctions.EnsureLocalCapacity(this.Reference, capacity);
			if (jniException is not null) throw jniException;
			this._objects.Capacity = capacity;
		}
		/// <inheritdoc cref="JEnvironment.SetObjectCache(LocalCache?)"/>
		public void SetObjectCache(LocalCache localCache) => this._objects = localCache;
		/// <summary>
		/// Retrieves local cache.
		/// </summary>
		public LocalCache GetLocalCache() => this._objects;
		/// <summary>
		/// Release all references.
		/// </summary>
		public void FreeReferences()
		{
			this._objects.ClearCache(this._env, true);
			this._cancellation.Cancel();
		}
		/// <summary>
		/// Creates a new local reference for <paramref name="result"/>.
		/// </summary>
		/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
		/// <param name="result">A <see cref="JLocalObject"/> instance.</param>
		public void CreateLocalRef<TObjectRef>(TObjectRef globalRef, JLocalObject? result)
			where TObjectRef : unmanaged, INativeType<TObjectRef>, IWrapper<JObjectLocalRef>,
			IEqualityOperators<TObjectRef, TObjectRef, Boolean>
		{
			if (globalRef == default || result is not null) return;
			JTrace.CreateLocalRef(globalRef);
			JObjectLocalRef localRef = this._env.CreateLocalRef(globalRef);
			JLocalObject jLocal = this.Register(result)!;
			jLocal.SetValue(localRef);
		}
		/// <summary>
		/// Creates a global reference from <paramref name="localRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
		public unsafe JGlobalRef CreateGlobalRef(JObjectLocalRef localRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewGlobalRefInfo);
			JGlobalRef globalRef = nativeInterface.ReferenceFunctions.NewGlobalRef.NewRef(this.Reference, localRef);
			if (globalRef == default) this.CheckJniError();
			return globalRef;
		}
		/// <summary>
		/// Removes <paramref name="jLocal"/> from current thread.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		public void Remove(JLocalObject? jLocal)
		{
			if (jLocal is null) return;
			JObjectLocalRef localRef = jLocal.LocalReference;
			this._objects.Remove(localRef);
		}
	}
}