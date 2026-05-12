namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Alien (external) local object cache.
/// </summary>
internal abstract class AlienLocalCache : LocalCache, IDisposable
{
	/// <summary>
	/// Parameters dictionary.
	/// </summary>
	private readonly Dictionary<JObjectLocalRef, ILocalObject> _aliens = [];
	/// <summary>
	/// Class dictionary.
	/// </summary>
	private readonly Dictionary<String, JClassLocalRef> _classes = new();
	/// <summary>
	/// <see cref="INativeThread"/> instance.
	/// </summary>
	public INativeThread Environment { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="classes">A <see cref="ClassCache"/> instance.</param>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	protected AlienLocalCache(ClassCache classes, INativeThread env) : base(classes) => this.Environment = env;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	protected AlienLocalCache(INativeThread env) : base(env.LocalCache)
	{
		this.Environment = env;
		env.CheckJniError();
	}
	/// <inheritdoc/>
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Registers an object in current alien local object frame.
	/// </summary>
	/// <param name="localRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	public void RegisterAlien(JObjectLocalRef localRef, ILocalObject jLocal)
	{
		this._aliens.TryAdd(localRef, jLocal);
		this[localRef] = jLocal.Lifetime.GetCacheable();
	}
	/// <summary>
	/// Registers a class in current alien local object frame.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public void RegisterAlien(JClassLocalRef classRef, JClassObject jClass)
	{
		this.RegisterAlien(classRef.Value, jClass);
		this._classes.TryAdd(jClass.Hash, classRef);
		this.Environment.LoadClass(jClass);
	}
	/// <summary>
	/// Registers a class in current alien local object frame.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="classView">A <see cref="JLocalObject.View{JClassObject}"/> instance.</param>
	public void RegisterAlien(JClassLocalRef classRef, JLocalObject.View<JClassObject> classView)
	{
		this._aliens.TryAdd(classRef.Value, classView);
		this[classRef.Value] = (classView as ILocalViewObject).Lifetime;
		this._classes.TryAdd(classView.Object.Hash, classRef);
		this.Environment.ClassCache.Load(classRef);
	}

	/// <inheritdoc/>
	public override Boolean IsParameter(JObjectLocalRef localRef)
		=> this._aliens.ContainsKey(localRef) || base.IsParameter(localRef);
	/// <inheritdoc/>
	public override JClassLocalRef FindClassParameter(String hash)
		=> this._classes.TryGetValue(hash, out JClassLocalRef result) ? result : base.FindClassParameter(hash);
	/// <inheritdoc/>
	public override ObjectLifetime? GetLifetime(JObjectLocalRef localRef)
		=> this._aliens.TryGetValue(localRef, out ILocalObject? jLocal) ? jLocal.Lifetime : base.GetLifetime(localRef);

	/// <inheritdoc cref="IDisposable.Dispose()"/>
	/// <param name="disposing">
	/// Indicates whether current calls is performed by <see cref="IDisposable.Dispose()"/>.
	/// </param>
	protected virtual void Dispose(Boolean disposing)
	{
		this.Clear();
		if (disposing)
			this.ClearCache(this.Environment, false);
	}
	/// <summary>
	/// Clears current instance.
	/// </summary>
	protected void Clear()
	{
		this.Environment.ClassCache.Unload(this._classes);
		this._aliens.Clear();
		this._classes.Clear();
	}

	~AlienLocalCache() { this.Dispose(false); }
}