namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class helper stores the <typeparamref name="TObject"/> instance cache.
/// </summary>
/// <typeparam name="TObject">Type of object.</typeparam>
/// <typeparam name="TReference">Type of reference.</typeparam>
/// <typeparam name="TCreationArg">Type of creation argument.</typeparam>
internal abstract record ReferenceHelperCache<TObject, TReference, TCreationArg>
	where TObject : class, IWrapper<TReference> where TReference : unmanaged, INativeReferenceType<TReference>
{
	/// <summary>
	/// Dictionary of objects.
	/// </summary>
	private readonly ConcurrentDictionary<IntPtr, WeakReference<TObject>> _objects = new();

	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance from <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A reference pointer to create a <typeparamref name="TObject"/> instance.</param>
	/// <param name="arg">Creation argument.</param>
	/// <returns>A <typeparamref name="TObject"/> instance.</returns>
	protected abstract TObject Create(TReference reference, TCreationArg? arg);
	/// <summary>
	/// Destroys <paramref name="instance"/>.
	/// </summary>
	/// <param name="instance">A <typeparamref name="TObject"/> to destroy.</param>
	protected abstract void Destroy(TObject instance);

	/// <summary>
	/// Retrieves a <typeparamref name="TObject"/> instance from <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A reference pointer to <typeparamref name="TObject"/> instance.</param>
	/// <param name="arg">Creation argument.</param>
	/// <param name="isNew">Indicates whether current object is new in cache.</param>
	/// <returns>A <typeparamref name="TObject"/> instance.</returns>
	public TObject Get(TReference reference, out Boolean isNew, TCreationArg? arg = default)
	{
		isNew = false;
		if (this._objects.TryGetValue(reference.Pointer, out WeakReference<TObject>? weak) &&
		    weak.TryGetTarget(out TObject? result)) return result;

		isNew = true;
		result = this.Create(reference, arg);
		if (weak is null)
			this._objects.TryAdd(reference.Pointer, new(result));
		else
			weak.SetTarget(result);
		return result;
	}

	/// <summary>
	/// Removes the <typeparamref name="TObject"/> instance of <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A reference pointer to <typeparamref name="TObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <typeparamref name="TObject"/> instance was destroyed; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	public Boolean Remove(TReference reference)
	{
		if (!this._objects.TryRemove(reference.Pointer, out WeakReference<TObject>? weak) ||
		    !weak.TryGetTarget(out TObject? result)) return false;
		this.Destroy(result);
		return true;
	}
}