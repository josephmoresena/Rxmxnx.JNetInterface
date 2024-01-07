namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a <see cref="JObject"/> instance which may remain valid across different
/// threads.
/// </summary>
public abstract partial class JGlobalBase : JReferenceObject, IDisposable
{
	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	public IVirtualMachine VirtualMachine => this._vm;
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	public Type MetadataType => this._objectMetadata.GetType();
	/// <inheritdoc/>
	public override CString ObjectClassName => this._objectMetadata.ObjectClassName;
	/// <inheritdoc/>
	public override CString ObjectSignature => this._objectMetadata.ObjectSignature;

	/// <inheritdoc/>
	public void Dispose()
	{
		using IThread thread = this._vm.CreateThread(ThreadPurpose.RemoveGlobalReference);
		this.Unload(thread);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Destructor.
	/// </summary>
	~JGlobalBase()
	{
		using IThread thread = this._vm.CreateThread(ThreadPurpose.RemoveGlobalReference);
		this.Dispose(false, thread);
	}

	/// <summary>
	/// Indicates whether the current instance is valid.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if current instance is still valid; otherwise, <see langword="false"/>.
	/// </returns>
	public virtual Boolean IsValid(IEnvironment env) => !this._isDisposed && !this.IsDefault;

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting
	/// unmanaged resources.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	public void Unload(IEnvironment env) => this.Dispose(true, env);

	/// <inheritdoc cref="IDisposable.Dispose()"/>
	/// <param name="disposing">
	/// Indicates whether this method was called from the <see cref="IDisposable.Dispose"/> method.
	/// </param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	protected virtual void Dispose(Boolean disposing, IEnvironment env)
	{
		if (this._isDisposed) return;

		if (disposing && !this.IsDisposable)
		{
			ImmutableArray<Int64> keys = this._objects.Keys.ToImmutableArray();
			foreach (Int64 key in keys)
			{
				if (this._objects.TryRemove(key, out WeakReference<ObjectLifetime>? wObj) &&
				    wObj.TryGetTarget(out ObjectLifetime? objectLifetime))
					objectLifetime.UnloadGlobal(this);
			}
		}

		if (!env.ReferenceFeature.Unload(this)) return;
		this.ClearValue();
		this._isDisposed = true;
	}

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="globalRef">A global object reference the value of current instance.</param>
	protected void SetValue(IntPtr globalRef)
	{
		if (globalRef == default) return;
		this._value.Value = globalRef;
		this._isDisposed = false;
	}
	/// <summary>
	/// Removes the association of <paramref name="jLocal"/> from this instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jLocal"/> was associated to this instance;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	protected Boolean Remove(JLocalObject jLocal)
	{
		ObjectLifetime objectLifetime = jLocal.Lifetime;
		return this._objects.TryRemove(objectLifetime.Id, out _);
	}

	/// <summary>
	/// Associates the current instance to <paramref name="objectLifetime"/>.
	/// </summary>
	/// <param name="objectLifetime"><see cref="ObjectLifetime"/> instance.</param>
	/// <returns>A valid <see cref="JGlobalBase"/> associated to <paramref name="objectLifetime"/>.</returns>
	internal JGlobalBase? Load(ObjectLifetime objectLifetime)
	{
		if (!this._objects.TryAdd(objectLifetime.Id, new(objectLifetime)))
			this._objects[objectLifetime.Id].SetTarget(objectLifetime);
		JGlobalBase? result = default;
		if (!this.IsValid(objectLifetime.Environment))
			return result;
		result = this;
		return result;
	}

	/// <summary>
	/// Indicates whether <paramref name="jGlobal"/> is valid.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if current instance is still valid; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean IsValid([NotNullWhen(true)] JGlobalBase? jGlobal, IEnvironment env)
		=> jGlobal is not null && jGlobal.IsValid(env);
}