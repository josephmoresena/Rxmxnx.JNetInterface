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
		if (this._isDisposed)
			return;

		if (disposing)
		{
			ImmutableArray<Int64> keys = this._objects.Keys.ToImmutableArray();
			foreach (Int64 key in keys)
			{
				if (this._objects.TryRemove(key, out WeakReference<JLocalObject>? wObj) &&
				    wObj.TryGetTarget(out JLocalObject? jLocal))
					this.Unload(jLocal);
			}
		}

		env.ReferenceProvider.Unload(this);
		this.ClearValue();
		this._isDisposed = true;
	}

	/// <summary>
	/// Associates the current instance to <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <returns>A valid <see cref="JGlobalBase"/> associated to <paramref name="jLocal"/>.</returns>
	protected JGlobalBase? Load(JLocalObject jLocal)
	{
		if (!this._objects.TryAdd(jLocal.Id, new(jLocal)))
			this._objects[jLocal.Id].SetTarget(jLocal);
		JGlobalBase? result = default;
		if (!this.IsValid(jLocal.Environment))
			return result;
		result = this;
		return result;
	}
	/// <summary>
	/// Removes the association of <paramref name="jLocal"/> from this instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jLocal"/> was associated to this instance;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	protected Boolean Remove(JLocalObject jLocal) => this._objects.TryRemove(jLocal.Id, out _);

	/// <summary>
	/// Disassociates the current current instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	protected abstract void Unload(in JLocalObject jLocal);
}