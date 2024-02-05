namespace Rxmxnx.JNetInterface;

/// <summary>
/// Call reference frame
/// </summary>
internal sealed record CallFrame : LocalCache, IDisposable
{
	/// <summary>
	/// Class dictionary.
	/// </summary>
	private readonly Dictionary<String, JClassLocalRef> _classes = new();
	/// <summary>
	/// <see cref="JEnvironment"/> instance.
	/// </summary>
	private readonly JEnvironment _env;
	/// <summary>
	/// Parameters dictionary.
	/// </summary>
	private readonly Dictionary<JObjectLocalRef, JLocalObject> _parameters = [];

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"></param>
	public CallFrame(JEnvironment env) : base(env.LocalCache) => this._env = env;
	/// <inheritdoc/>
	public void Dispose()
	{
		this.ClearParameters();
		this.ClearCache(this._env, false);
		GC.SuppressFinalize(this);
	}

	~CallFrame() { this.ClearParameters(); }

	/// <summary>
	/// Registers an object in current call frame.
	/// </summary>
	/// <param name="localRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	public void RegisterParameter(JObjectLocalRef localRef, JLocalObject jLocal)
		=> this._parameters.Add(localRef, jLocal);
	/// <summary>
	/// Registers a class in current call frame.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public void RegisterParameter(JClassLocalRef classRef, JClassObject jClass)
	{
		this.RegisterParameter(classRef.Value, jClass);
		this._classes.TryAdd(jClass.Hash, classRef);
		this._env.ClassCache.Load(classRef);
		this._env.LoadClass(jClass);
	}

	/// <summary>
	/// Sets current instance as current object cache.
	/// </summary>
	public void Activate() => this._env.SetObjectCache(this);

	/// <inheritdoc/>
	public override Boolean IsParameter(JObjectLocalRef localRef)
		=> this._parameters.ContainsKey(localRef) || base.IsParameter(localRef);
	/// <inheritdoc/>
	public override JClassLocalRef FindClassParameter(String hash)
		=> this._classes.TryGetValue(hash, out JClassLocalRef result) ? result : base.FindClassParameter(hash);
	/// <inheritdoc/>
	public override ObjectLifetime? GetLifetime(JObjectLocalRef localRef)
		=> this._parameters.TryGetValue(localRef, out JLocalObject? jLocal) ?
			jLocal.Lifetime :
			base.GetLifetime(localRef);

	/// <summary>
	/// Clear call parameters.
	/// </summary>
	private void ClearParameters()
	{
		this._env.ClassCache.Unload(this._classes);
		this._parameters.Clear();
		this._classes.Clear();
	}
}