namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local <c>java.lang.Object</c> instance.
/// </summary>
public partial class JLocalObject : JReferenceObject, IReferenceType<JLocalObject>
{
	/// <summary>
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment => this._env;

	/// <inheritdoc cref="JObject.ObjectClassName"/>
	public override CString ObjectClassName => this._class?.Name ?? JObject.JObjectClassName;
	/// <inheritdoc cref="JObject.ObjectSignature"/>
	public override CString ObjectSignature => this._class?.ClassSignature ?? JObject.JObjectSignature;
	/// <summary>
	/// Retrieves the class object from current instance.
	/// </summary>
	public JClassObject Class
	{
		get
		{
			if (this._class is null)
				this.LoadClassObject();
			return this._class!;
		}
	}
	/// <summary>
	/// Retrieves the global object from current instance.
	/// </summary>
	public JGlobal Global
	{
		get
		{
			if (this._global is null || this._global.IsValid(this._env))
				this._global = this._env.ReferenceProvider.Create<JGlobal>(this);
			return this._global;
		}
	}
	/// <summary>
	/// Retrieves the global object from current instance.
	/// </summary>
	public JWeak Weak
	{
		get
		{
			if (this._weak is null || this._weak.IsValid(this._env))
				this._weak = this._env.ReferenceProvider.Create<JWeak>(this);
			return this._weak;
		}
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	protected JLocalObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal)
	{
		jLocal._lifetime.Load(this);

		this._env = jLocal.Environment;
		this._lifetime = jLocal._lifetime;
		this._isDisposed = jLocal._isDisposed;
		this._class = jClass ?? jLocal._class;
		this._global = jLocal._global;
		this._weak = jLocal._weak;
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Loads the class object in the current instance.
	/// </summary>
	public void LoadClassObject()
	{
		if (this._class is not null && this._isRealClass) 
			return;
		this._class = this._env.ClassProvider.GetObjectClass(this);
		this._isRealClass = true;
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
			this._isDisposed = this._env.ReferenceProvider.Unload(this);
	}
}