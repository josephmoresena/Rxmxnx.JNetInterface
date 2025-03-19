namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial class EnvironmentCache
	{
		/// <summary>
		/// Ensure local capacity to <paramref name="capacity"/>.
		/// </summary>
		/// <param name="capacity">Top of local references.</param>
		/// <exception cref="JniException"/>
		public unsafe void EnsureLocalCapacity(Int32 capacity)
		{
			if (capacity <= this._objects.Capacity.GetValueOrDefault()) return;
			ImplementationValidationUtilities.ThrowIfDifferentThread(this.Reference, this.Thread);
			if (this._objects is not LocalFrame)
			{
				ref readonly NativeInterface nativeInterface =
					ref this.GetNativeInterface<NativeInterface>(NativeInterface.EnsureLocalCapacityInfo);
				JniException? jniException =
					nativeInterface.ReferenceFunctions.EnsureLocalCapacity(this.Reference, capacity);
				this.CheckJniError();
				if (jniException is not null) throw jniException;
			}
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
		public void FreeReferences() => this._objects.ClearCache(this._env, true);
		/// <summary>
		/// Creates a global reference from <paramref name="localRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="withNoCheckError">Indicates whether <see cref="CheckJniError"/> should not be called.</param>
		/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
		public JGlobalRef CreateGlobalRef(JObjectLocalRef localRef, Boolean withNoCheckError = false)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewGlobalRefInfo);
			JGlobalRef globalRef = nativeInterface.ReferenceFunctions.NewGlobalRef.NewRef(this.Reference, localRef);
			JTrace.CreateGlobalRef(localRef, globalRef);
			if (globalRef == default && !withNoCheckError) this.CheckJniError();
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