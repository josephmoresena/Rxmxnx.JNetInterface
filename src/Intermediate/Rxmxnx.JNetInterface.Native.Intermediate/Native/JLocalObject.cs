namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local <c>java.lang.Object</c> instance.
/// </summary>
public class JLocalObject : JReferenceObject, IReferenceType<JLocalObject>
{
	/// <summary>
	/// Instance class object.
	/// </summary>
	private readonly IClass? _class;
	/// <summary>
	/// Internal <see cref="IEnvironment"/> instance.
	/// </summary>
	private readonly IEnvironment _env;

	/// <summary>
	/// Current <see cref="JGlobal"/> instance.
	/// </summary>
	private readonly JGlobal? _global;
	/// <summary>
	/// Internal <see cref="ObjectLifetime"/> instance.
	/// </summary>
	private readonly ObjectLifetime _lifetime;
	/// <summary>
	/// Current <see cref="JWeak"/> instance.
	/// </summary>
	private readonly JWeak? _weak;
	/// <summary>
	/// Indicates whether the this instance is disposed.
	/// </summary>
	private Boolean _isDisposed;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	protected JLocalObject(JLocalObject jLocal, IClass? jClass = default) : base(jLocal)
	{
		jLocal._lifetime.Load(this);

		this._env = jLocal.Environment;
		this._lifetime = jLocal._lifetime;
		this._isDisposed = jLocal._isDisposed;
		this._class = jClass ?? jLocal._class;
		this._global = jLocal._global;
		this._weak = jLocal._weak;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="refObj"><see cref="JReferenceObject"/> instance.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	internal JLocalObject(IEnvironment env, JReferenceObject refObj, Boolean isNativeParameter, IClass? jClass) :
		base(refObj)
	{
		this._env = env;
		this._lifetime = new(isNativeParameter, this);
		this._isDisposed = false;
		this._class = jClass;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JLocalObject(IEnvironment env, JGlobalBase jGlobal) : base(jGlobal)
	{
		this._env = env;
		this._lifetime = new(false, this);
		this._class = jGlobal.GetObjectClass(env);
		this._global = jGlobal as JGlobal;
		this._weak = jGlobal as JWeak;
	}

	/// <summary>
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment => this._env;

	/// <summary>
	/// Internal reference value.
	/// </summary>
	internal JObjectLocalRef InternalReference => base.To<JObjectLocalRef>();
	/// <summary>
	/// Internal value.
	/// </summary>
	internal JValue InternalValue => base.Value;

	/// <inheritdoc/>
	internal override JValue Value => this.GetGlobalObject()?.Value ?? base.Value;
	/// <inheritdoc cref="JObject.ObjectClassName"/>
	public override CString ObjectClassName => this._class?.Name ?? JObject.JObjectClassName;
	/// <inheritdoc cref="JObject.ObjectSignature"/>
	public override CString ObjectSignature => this._class?.ClassSignature ?? JObject.JObjectSignature;

	/// <inheritdoc/>
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	static JLocalObject? IDataType<JLocalObject>.Create(JObject? jObject)
	{
		if (jObject is JLocalObject { Value.IsDefault: false, } jLocal)
			return new(jLocal);
		return null;
	}

	/// <inheritdoc/>
	~JLocalObject() { this.Dispose(false); }

	/// <inheritdoc cref="IDisposable.Dispose()"/>
	/// <param name="disposing">
	/// Indicates whether this method was called from the <see cref="IDisposable.Dispose"/> method.
	/// </param>
	protected virtual void Dispose(Boolean disposing)
	{
		if (this._isDisposed)
			return;
		if (this._lifetime.Unload(this))
			//TODO: Call Unload local object.
			this._isDisposed = this is not IClassType;
	}

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	internal void SetValue(JObjectLocalRef localRef)
	{
		if (localRef == default)
			return;
		base.SetValue(localRef);
		this._lifetime.Load(this);
	}
	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <typeparam name="TValue">Type of <see langword="IObjectReference"/> instance.</typeparam>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	internal void SetValue<TValue>(TValue localRef) where TValue : unmanaged, IObjectReference
	{
		if (localRef.Equals(default))
			return;
		base.SetValue(localRef);
		this._lifetime.Load(this);
	}

	/// <inheritdoc/>
	internal override ref readonly TValue As<TValue>()
	{
		JGlobalBase? jGlobal = this.GetGlobalObject();
		if (jGlobal is not null)
			return ref jGlobal.As<TValue>();
		return ref base.As<TValue>();
	}
	/// <inheritdoc/>
	internal override TValue To<TValue>() => this.GetGlobalObject()?.To<TValue>() ?? base.To<TValue>();

	/// <summary>
	/// Retrieves the loaded global object for current instance.
	/// </summary>
	/// <returns>The loaded <see cref="JGlobalBase"/> object for current instance.</returns>
	private JGlobalBase? GetGlobalObject()
	{
		if (this._global is not null && this._global.IsValid(this._env))
			return this._global;
		if (this._weak is not null && this._weak.IsValid(this._env))
			return this._weak;
		return default;
	}
}