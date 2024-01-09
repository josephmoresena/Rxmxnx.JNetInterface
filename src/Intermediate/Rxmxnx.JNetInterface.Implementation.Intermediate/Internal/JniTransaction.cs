namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a JNI call transaction.
/// </summary>
internal sealed record JniTransaction : INativeTransaction
{
	/// <summary>
	/// Lock object.
	/// </summary>
	private readonly Object _lock = new();
	/// <summary>
	/// Set of references.
	/// </summary>
	private readonly HashSet<IntPtr> _references = [];

	/// <summary>
	/// Indicates current instance is disposed.
	/// </summary>
	private Boolean _disposed;
	/// <summary>
	/// Current transaction handle.
	/// </summary>
	private JniTransactionHandle _handle;

	ref JniTransactionHandle IReferenceable<JniTransactionHandle>.Reference => ref this._handle;
	ref readonly JniTransactionHandle IReadOnlyReferenceable<JniTransactionHandle>.Reference => ref this._handle;

	/// <inheritdoc/>
	public void Dispose()
	{
		if (this._disposed) return;
		this._disposed = true;
		this._handle.Dispose();
	}

	/// <inheritdoc/>
	public JObjectLocalRef Add(JObjectLocalRef localRef)
	{
		if (localRef == default) return default;
		lock (this._lock)
			this._references.Add(localRef.Pointer);
		return localRef;
	}
	/// <inheritdoc/>
	public JObjectLocalRef Add(JReferenceObject? jReferenceObject)
	{
		if (jReferenceObject is null) return default;
		JObjectLocalRef localRef = jReferenceObject.As<JObjectLocalRef>();
		if (localRef == default) return default;
		lock (this._lock)
			this._references.Add(localRef.Pointer);
		return localRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jReferenceObject"/> instance reference.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IObjectReferenceType"/> type.</typeparam>
	/// <param name="jReferenceObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> reference.</returns>
	public TReference Add<TReference>(JReferenceObject? jReferenceObject)
		where TReference : unmanaged, IObjectReferenceType<TReference>
	{
		if (jReferenceObject is null) return default;
		TReference nativeRef = jReferenceObject.As<TReference>();
		if (nativeRef.Value == default) return default;
		lock (this._lock)
			this._references.Add(nativeRef.Pointer);
		return nativeRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jGlobal"/> instance reference.
	/// </summary>
	/// <typeparam name="TGlobalReference">A <see cref="IObjectGlobalReferenceType"/> type.</typeparam>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <typeparamref name="TGlobalReference"/> reference.</returns>
	public TGlobalReference Add<TGlobalReference>(JGlobalBase? jGlobal)
		where TGlobalReference : unmanaged, IObjectGlobalReferenceType<TGlobalReference>
	{
		if (jGlobal is null) return default;
		TGlobalReference nativeRef = jGlobal.As<TGlobalReference>();
		if (nativeRef.Value == default) return default;
		lock (this._lock)
			this._references.Add(nativeRef.Pointer);
		return nativeRef;
	}
	/// <summary>
	/// Indicates whether reference is used by current transaction.
	/// </summary>
	/// <param name="reference">A <see cref="IntPtr"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if reference is used by current transaction;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean Contains(IntPtr reference)
	{
		if (reference == IntPtr.Zero) return false;
		lock (this._lock)
			return this._references.Contains(reference);
	}
}