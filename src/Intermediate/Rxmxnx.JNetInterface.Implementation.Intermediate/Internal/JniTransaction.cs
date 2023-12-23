namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a JNI call transaction.
/// </summary>
internal sealed record JniTransaction : IDisposable
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
	/// Dictionary of transactions.
	/// </summary>
	private readonly IDictionary<Guid, JniTransaction> _transactions;
	/// <summary>
	/// Indicates current instance is disposed.
	/// </summary>
	private Boolean _disposed;

	/// <summary>
	/// Current transaction identifier.
	/// </summary>
	private Guid _id;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="transactions">Dictionary of transactions.</param>
	private JniTransaction(IDictionary<Guid, JniTransaction> transactions) => this._transactions = transactions;
	/// <inheritdoc/>
	public void Dispose()
	{
		if (this._disposed) return;
		this._disposed = true;
		this._transactions.Remove(this._id);
	}

	/// <summary>
	/// Adds to current transaction <paramref name="localRef"/> reference.
	/// </summary>
	/// <param name="localRef">A <see cref="localRef"/> reference.</param>
	/// <returns><paramref name="localRef"/>.</returns>
	public JObjectLocalRef Add(JObjectLocalRef localRef)
	{
		if (localRef == default) return default;
		lock (this._lock)
			this._references.Add(localRef.Pointer);
		return localRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="nativeRef"/> reference.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="INativeReferenceType"/> type.</typeparam>
	/// <param name="nativeRef">A <see cref="nativeRef"/> reference.</param>
	/// <returns><paramref name="nativeRef"/>.</returns>
	public TReference Add<TReference>(TReference nativeRef)
		where TReference : unmanaged, IObjectReferenceType<TReference>
	{
		if (nativeRef.Value == default) return default;
		lock (this._lock)
			this._references.Add(nativeRef.Pointer);
		return nativeRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jReferenceObject"/> instance reference.
	/// </summary>
	/// <param name="jReferenceObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
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
	/// Adds to current transaction <paramref name="jLocal"/> instance reference.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
	public JObjectLocalRef Add(JLocalObject? jLocal)
	{
		if (jLocal is null) return default;
		JObjectLocalRef localRef = jLocal.As<JObjectLocalRef>();
		if (localRef == default) return default;
		lock (this._lock)
			this._references.Add(localRef.Pointer);
		return localRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jClass"/> instance reference.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	public JClassLocalRef Add(JClassObject? jClass)
	{
		if (jClass is null) return default;
		JClassLocalRef classRef = jClass.Reference;
		if (classRef.Value == default) return default;
		lock (this._lock)
			this._references.Add(classRef.Pointer);
		return classRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jString"/> instance reference.
	/// </summary>
	/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
	/// <returns>A <see cref="JStringLocalRef"/> reference.</returns>
	public JStringLocalRef Add(JStringObject? jString)
	{
		if (jString is null) return default;
		JStringLocalRef stringRef = jString.Reference;
		if (stringRef.Value == default) return default;
		lock (this._lock)
			this._references.Add(stringRef.Pointer);
		return stringRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jArray"/> instance reference.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <returns>A <see cref="JArrayLocalRef"/> reference.</returns>
	public JArrayLocalRef Add(JArrayObject? jArray)
	{
		if (jArray is null) return default;
		JArrayLocalRef arrayRef = jArray.Reference;
		if (arrayRef.Value == default) return default;
		lock (this._lock)
			this._references.Add(arrayRef.Pointer);
		return arrayRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jThrowable"/> instance reference.
	/// </summary>
	/// <param name="jThrowable">A <see cref="JThrowableObject"/> instance.</param>
	/// <returns>A <see cref="JArrayLocalRef"/> reference.</returns>
	public JThrowableLocalRef Add(JThrowableObject? jThrowable)
	{
		if (jThrowable is null) return default;
		JThrowableLocalRef throwableRef = jThrowable.As<JThrowableLocalRef>();
		if (throwableRef.Value == default) return default;
		lock (this._lock)
			this._references.Add(throwableRef.Pointer);
		return throwableRef;
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
	/// Adds to current transaction <paramref name="jArray"/> instance reference.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IArrayReferenceType"/> type.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> reference.</returns>
	public TReference Add<TReference>(JArrayObject? jArray)
		where TReference : unmanaged, IArrayReferenceType<TReference>
	{
		if (jArray is null) return default;
		TReference arrayRef = jArray.As<TReference>();
		if (arrayRef.Value == default) return default;
		lock (this._lock)
			this._references.Add(arrayRef.Pointer);
		return arrayRef;
	}
	/// <summary>
	/// Indicates whether handle is used by current transaction.
	/// </summary>
	/// <param name="handle">A <see cref="IntPtr"/> handle.</param>
	/// <returns>
	/// <see langword="true"/> if handle is used by current transaction;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean Contains(IntPtr handle)
	{
		if (handle == IntPtr.Zero) return false;
		lock (this._lock)
			return this._references.Contains(handle);
	}

	/// <summary>
	/// Creates an empty <see cref="JniTransaction"/> instance.
	/// </summary>
	/// <param name="transactions">Dictionary of transactions.</param>
	/// <returns>A new <see cref="JniTransaction"/> instance.</returns>
	public static JniTransaction Create(IDictionary<Guid, JniTransaction> transactions)
	{
		JniTransaction result = new(transactions);
		do
			result._id = Guid.NewGuid();
		while (!transactions.TryAdd(result._id, result));
		return result;
	}
}