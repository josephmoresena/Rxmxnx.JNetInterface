namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Local object cache.
/// </summary>
internal class LocalCache
{
	/// <summary>
	/// Object dictionary.
	/// </summary>
	private readonly Dictionary<JObjectLocalRef, ObjectLifetime> _objects;

	/// <summary>
	/// Internal id.
	/// </summary>
	public readonly Guid Id;
	/// <summary>
	/// Previous cache.
	/// </summary>
	private LocalCache? _previous;

	/// <summary>
	/// Lifetime for <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	public virtual ObjectLifetime this[JObjectLocalRef localRef]
	{
		set
		{
			Boolean add = !(this._previous?.IsRegistered(localRef) ?? localRef == default);
			if (add) this._objects[localRef] = value;
		}
	}
	/// <summary>
	/// Name of cache.
	/// </summary>
	public virtual String Name => "initial";
	/// <see cref="IEnvironment.LocalCapacity"/>
	public virtual Int32? Capacity { get; set; }
	/// <summary>
	/// Current class cache.
	/// </summary>
	public ClassCache ClassCache { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public LocalCache(ClassCache classCache)
	{
		this._objects = new();
		this.Id = Guid.NewGuid();
		this.ClassCache = classCache;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="previous">Previous instance.</param>
	protected LocalCache(LocalCache previous) : this(previous.ClassCache)
	{
		this._previous = previous;
		this._objects = new();
		this.Id = Guid.NewGuid();
	}

	/// <summary>
	/// Indicates whether current value is registered in the cache tree.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="localRef"/> is already registered by current cache tree; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	public Boolean IsRegistered(JObjectLocalRef localRef)
		=> this.Contains(localRef) || (this._previous?.IsRegistered(localRef) ?? localRef == default);
	/// <summary>
	/// Indicates whether current value is contained by current cache.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="localRef"/> is already registered by current cache; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	public Boolean Contains(JObjectLocalRef localRef) => this._objects.ContainsKey(localRef);
	/// <summary>
	/// Clear current cache.
	/// </summary>
	/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
	/// <param name="exclude">A <see cref="JObjectLocalRef"/> reference to exclude.</param>
	/// <param name="recursive">Indicates whether current clear must do recursively.</param>
	public void ClearCache(JEnvironment env, Boolean recursive, JObjectLocalRef exclude = default)
	{
		if (this._objects.Remove(exclude)) // Removes excluded result and unloads its class cache.
			this.ClassCache.Unload(JClassLocalRef.FromReference(in exclude));

		JObjectLocalRef[] keys = this._objects.Keys.ToArray();
		foreach (JObjectLocalRef key in keys)
			this.Remove(key); // Removes each reference in cache.

		if (this._previous is null || !Object.ReferenceEquals(env.LocalCache, this))
			return; // Current cache is initial or is not active.

		if (!recursive)
			env.SetObjectCache(this._previous); // Restores to previous cache.
		else
			this._previous.ClearCache(env, recursive, exclude);
	}
	/// <summary>
	/// Removes current local reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	public virtual void Remove(JObjectLocalRef localRef)
	{
		if (!this._objects.Remove(localRef, out ObjectLifetime? lifetime))
			this._previous?.Remove(localRef);
		if (lifetime is null) return;
		lifetime.Dispose(); // Disposes current instance.
		this.ClassCache.Unload(JClassLocalRef.FromReference(localRef)); // Unloads class cache.
	}
	/// <summary>
	/// Indicates whether <paramref name="localRef"/> is JNI method parameter reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="localRef"/> is a parameter reference;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public virtual Boolean IsParameter(JObjectLocalRef localRef)
		=> localRef != default && (this._previous?.IsParameter(localRef) ?? false);
	/// <summary>
	/// Indicates whether <paramref name="localRef"/> is from a local frame.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="localRef"/> is from a local frame;
	/// otherwise, <see langword="false"/>.
	/// </returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public virtual Boolean IsFromLocalFrame(JObjectLocalRef localRef)
		=> localRef != default && (this._previous?.IsFromLocalFrame(localRef) ?? false);
	/// <summary>
	/// Retrieves class reference according to <paramref name="hash"/>.
	/// </summary>
	/// <param name="hash">Class hash.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	public virtual JClassLocalRef FindClassParameter(String hash)
		=> this._previous?.FindClassParameter(hash) ?? default;
	/// <summary>
	/// Retrieves the value associated with <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> instance.</param>
	/// <returns>The <see cref="ObjectLifetime"/> instance associated with <paramref name="localRef"/>.</returns>
	public virtual ObjectLifetime? GetLifetime(JObjectLocalRef localRef)
		=> this._objects.GetValueOrDefault(localRef) ?? this._previous?.GetLifetime(localRef);
	/// <summary>
	/// Sets previous cache.
	/// </summary>
	protected void SetPrevious(LocalCache previous) => this._previous = previous;
}