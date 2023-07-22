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
}