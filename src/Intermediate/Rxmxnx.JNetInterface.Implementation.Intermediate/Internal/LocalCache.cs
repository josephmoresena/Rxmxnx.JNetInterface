namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Local object cache.
/// </summary>
internal record LocalCache
{
	/// <summary>
	/// Object dictionary.
	/// </summary>
	private readonly Dictionary<JObjectLocalRef, ObjectLifetime> _objects;
	/// <summary>
	/// Previous cache.
	/// </summary>
	private readonly LocalCache? _previous;

	/// <summary>
	/// Lifetime for <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	public virtual ObjectLifetime this[JObjectLocalRef localRef]
	{
		set => this._objects[localRef] = value;
	}
	/// <see cref="IEnvironment.LocalCapacity"/>
	public virtual Int32? Capacity { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public LocalCache() => this._objects = new();
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="previous">Previous instance.</param>
	public LocalCache(LocalCache previous)
	{
		this._previous = previous;
		this._objects = new();
	}

	/// <summary>
	/// Clear current cache.
	/// </summary>
	/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
	/// <param name="recursive">Indicates whether current clear must done recursively.</param>
	public void ClearCache(JEnvironment env, Boolean recursive)
	{
		JObjectLocalRef[] keys = this._objects.Keys.ToArray();
		foreach (JObjectLocalRef key in keys)
			this._objects[key].Dispose();

		if (this._previous is null || !Object.ReferenceEquals(env.LocalCache, this)) return;
		if (!recursive)
			env.SetObjectCache(this._previous);
		else
			this._previous.ClearCache(env, recursive);
	}
	/// <summary>
	/// Removes current local reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	public virtual void Remove(JObjectLocalRef localRef)
	{
		if (!this._objects.Remove(localRef))
			this._previous?.Remove(localRef);
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
	/// Retrieves class reference according with <paramref name="hash"/>.
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
}