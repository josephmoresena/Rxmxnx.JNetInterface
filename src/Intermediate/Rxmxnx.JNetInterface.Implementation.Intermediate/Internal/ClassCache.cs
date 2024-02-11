namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Class cache.
/// </summary>
internal class ClassCache
{
	/// <summary>
	/// Class access dictionary.
	/// </summary>
	private readonly ConcurrentDictionary<JClassLocalRef, AccessCache> _access = new();
	/// <summary>
	/// Retrieves access cache.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	public AccessCache? this[JClassLocalRef classRef] => this._access.GetValueOrDefault(classRef);

	/// <summary>
	/// Loads current class object.
	/// </summary>
	/// <param name="jClass">A <see cref="JReferenceObject"/> class object.</param>
	protected void Load(JReferenceObject jClass)
	{
		JClassLocalRef classRef = jClass switch
		{
			JLocalObject jLocal when jLocal.InternalReference != default => jLocal.InternalAs<JClassLocalRef>(),
			JGlobalBase { IsDefault: false, } => jClass.As<JClassLocalRef>(),
			_ => default,
		};
		this.Load(classRef);
	}
	/// <summary>
	/// Loads a <see cref="JClassLocalRef"/> reference.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	public void Load(JClassLocalRef classRef)
	{
		if (!classRef.IsDefault) this._access[classRef] = new(classRef);
	}
	/// <summary>
	/// Unloads current class.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	public void Unload(JClassLocalRef classRef) => this._access.TryRemove(classRef, out _);
	/// <summary>
	/// Unloads current class.
	/// </summary>
	/// <param name="references">A <see cref="JClassLocalRef"/> collection.</param>
	public void Unload(IReadOnlyDictionary<String, JClassLocalRef> references)
	{
		foreach (String hash in references.Keys)
		{
			this.Unload(references[hash]);
			this.SetAsUnloaded(hash, references[hash]);
		}
	}
	/// <summary>
	/// Clears current cache.
	/// </summary>
	public void Clear() => this._access.Clear();

	/// <summary>
	/// Set as unloaded a class with given hash.
	/// </summary>
	/// <param name="hash">A class hash.</param>
	/// <param name="classRef">Unloaded <see cref="JClassLocalRef"/> reference.</param>
	protected virtual void SetAsUnloaded(String hash, JClassLocalRef classRef) { }
}

/// <summary>
/// Class cache.
/// </summary>
/// <typeparam name="TClass">A <see cref="JReferenceObject"/> class type.</typeparam>
internal sealed class ClassCache<TClass> : ClassCache where TClass : JReferenceObject
{
	/// <summary>
	/// Class dictionary.
	/// </summary>
	private readonly ConcurrentDictionary<String, TClass> _classes = new();

	/// <summary>
	/// Cached class.
	/// </summary>
	/// <param name="hash">Class hash.</param>
	public TClass this[String hash]
	{
		get => this._classes[hash];
		set
		{
			this._classes[hash] = value;
			this.Load(value);
		}
	}

	/// <summary>
	/// Attempts to get the value associated with the specified hash from the cache.
	/// </summary>
	/// <param name="hash">The hash class to get.</param>
	/// <param name="jClass">
	/// When this method returns, contains the class from the cache that has the specified hash,
	/// or the <see langword="default"/> value if the operation failed.
	/// </param>
	/// <returns>
	/// <see langword="true"/> if the hash was found in the cache; otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean TryGetValue(String hash, [NotNullWhen(true)] out TClass? jClass)
		=> this._classes.TryGetValue(hash, out jClass);

	/// <inheritdoc/>
	protected override void SetAsUnloaded(String hash, JClassLocalRef classRef)
	{
		if (!this._classes.TryGetValue(hash, out TClass? jClass)) return;
		if ((jClass as JLocalObject)?.InternalReference == classRef.Value)
			jClass.ClearValue();
	}
}