namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Call reference frame
/// </summary>
internal sealed class CallFrame : LocalCache, IDisposable
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
	private readonly Dictionary<JObjectLocalRef, ILocalObject> _parameters = [];

	/// <inheritdoc/>
	public override String Name => "call";

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
	public CallFrame(JEnvironment env) : base(env.LocalCache)
	{
		this._env = env;
		env.CheckJniError();
	}
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
	public void RegisterParameter(JObjectLocalRef localRef, ILocalObject jLocal)
	{
		this._parameters.TryAdd(localRef, jLocal);
		this[localRef] = jLocal.Lifetime.GetCacheable();
	}
	/// <summary>
	/// Registers a class in current call frame.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public void RegisterParameter(JClassLocalRef classRef, JClassObject jClass)
	{
		this.RegisterParameter(classRef.Value, jClass);
		this._classes.TryAdd(jClass.Hash, classRef);
		this._env.LoadClass(jClass);
	}
	/// <summary>
	/// Registers a class in current call frame.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="classView">A <see cref="JLocalObject.View{JClassObject}"/> instance.</param>
	public void RegisterParameter(JClassLocalRef classRef, JLocalObject.View<JClassObject> classView)
	{
		this._parameters.TryAdd(classRef.Value, classView);
		this[classRef.Value] = (classView as ILocalViewObject).Lifetime;
		this._classes.TryAdd(classView.Object.Hash, classRef);
		this._env.ClassCache.Load(classRef);
	}

	/// <summary>
	/// Sets current instance as current object cache.
	/// </summary>
	/// <param name="previous"></param>
	public void Activate(out LocalCache previous)
	{
		previous = this._env.LocalCache;
		this.SetPrevious(previous);
		this._env.SetObjectCache(this);
	}

	/// <inheritdoc/>
	public override Boolean IsParameter(JObjectLocalRef localRef)
		=> this._parameters.ContainsKey(localRef) || base.IsParameter(localRef);
	/// <inheritdoc/>
	public override JClassLocalRef FindClassParameter(String hash)
		=> this._classes.TryGetValue(hash, out JClassLocalRef result) ? result : base.FindClassParameter(hash);
	/// <inheritdoc/>
	public override ObjectLifetime? GetLifetime(JObjectLocalRef localRef)
		=> this._parameters.TryGetValue(localRef, out ILocalObject? jLocal) ?
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