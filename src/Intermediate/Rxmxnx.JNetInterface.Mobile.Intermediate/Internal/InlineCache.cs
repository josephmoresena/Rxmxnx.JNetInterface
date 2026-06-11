namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Inline local cache.
/// </summary>
internal sealed class InlineCache : LocalCache, IDisposable
{
	/// <summary>
	/// A <see cref="ILocalCacheOwner"/> instance.
	/// </summary>
	private readonly INativeThread _env;
	/// <summary>
	/// Internal reference queue.
	/// </summary>
	private readonly HashSet<JObjectLocalRef> _references;

	/// <inheritdoc/>
	public override ObjectLifetime this[JObjectLocalRef localRef]
	{
		set
		{
			if (!this.IsRegistered(localRef))
				this._references.Add(localRef);
			base[localRef] = value;
		}
	}
	/// <inheritdoc/>
	public override String Name => "inline";

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	public InlineCache(INativeThread env) : base(env.LocalCache)
	{
		this._env = env;
		this._references = [];
		this._env.LocalCache = this;
	}
	/// <inheritdoc/>
	public void Dispose()
	{
		foreach (JObjectLocalRef localRef in this._references)
			this._env.MemoryManager.DeleteLocalRef(localRef);
		this.ClearCache(this._env, false);
	}

	/// <inheritdoc/>
	public override void Remove(JObjectLocalRef localRef)
	{
		if (this.Contains(localRef))
			this._references.Remove(localRef);
		base.Remove(localRef);
	}
}