namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local <c>java.lang.Object</c> instance.
/// </summary>
public partial class JLocalObject : JReferenceObject, IBaseClassType<JLocalObject>
{
	/// <summary>
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment => this._lifetime.Environment;
	/// <summary>
	/// Retrieves the class object from current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JClassObject Class => this._lifetime.GetLoadClassObject(this);
	/// <summary>
	/// Retrieves the global object from current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JGlobal Global => this._lifetime.GetLoadGlobalObject(this);
	/// <summary>
	/// Retrieves the global object from current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JWeak Weak => this._lifetime.GetLoadWeakObject(this);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ClassInitializer"/> initializer.</param>
	protected JLocalObject(IReferenceType.ClassInitializer initializer)
		=> this._lifetime = initializer.Class.Environment.ReferenceFeature.GetLifetime(this, initializer.ToInternal());
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.GlobalInitializer"/> initializer.</param>
	protected JLocalObject(IReferenceType.GlobalInitializer initializer) : base(initializer.Global)
	{
		this._lifetime = new(initializer.Environment, this, initializer.Global);
		JLocalObject.ProcessMetadata(this, initializer.Global.ObjectMetadata);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ObjectInitializer"/> initializer.</param>
	protected JLocalObject(IReferenceType.ObjectInitializer initializer) : base(initializer.Instance)
	{
		JLocalObject jLocal = initializer.Instance;
		jLocal._lifetime.Load(this);
		this._lifetime = jLocal._lifetime;
		this._lifetime.SetClass(initializer.Class);
		if (jLocal is JInterfaceObject jInterface)
			JLocalObject.ProcessMetadata(this, jInterface.ObjectMetadata);
	}
	/// <inheritdoc/>
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <inheritdoc cref="JObject.ObjectClassName"/>
	public override CString ObjectClassName => this._lifetime.Class?.Name ?? JObject.JObjectClassName;
	/// <inheritdoc cref="JObject.ObjectSignature"/>
	public override CString ObjectSignature => this._lifetime.Class?.ClassSignature ?? JObject.JObjectSignature;

	/// <summary>
	/// Indicates whether current instance is an instance of <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if current instance is an instance of
	/// <paramref name="jClass"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean InstanceOf(JClassObject jClass)
	{
		IEnvironment env = this.Environment;
		return env.ClassFeature.IsInstanceOf(this, jClass);
	}

	/// <inheritdoc/>
	~JLocalObject() { this.Dispose(false); }

	/// <inheritdoc cref="JObject.ObjectClassName"/>
	internal override Boolean IsInstanceOf<TDataType>()
	{
		Boolean result = this.Environment.ClassFeature.IsInstanceOf<TDataType>(this);
		this.Environment.ClassFeature.SetAssignableTo<TDataType>(this, result);
		return result;
	}

	/// <inheritdoc cref="IDisposable.Dispose()"/>
	/// <param name="disposing">
	/// Indicates whether this method was called from the <see cref="IDisposable.Dispose"/> method.
	/// </param>
	protected virtual void Dispose(Boolean disposing)
	{
		if (this._lifetime.IsDisposed) return;
		this._lifetime.Unload(this);
	}
	/// <summary>
	/// Creates the object metadata for current instance.
	/// </summary>
	/// <returns>The object metadata for current instance.</returns>
	protected virtual ObjectMetadata CreateMetadata() => new(this._lifetime.GetLoadClassObject(this));
	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="instanceMetadata">The object metadata for current instance.</param>
	protected virtual void ProcessMetadata(ObjectMetadata instanceMetadata)
		=> this._lifetime.SetClass(instanceMetadata);

	/// <summary>
	/// Retrieves the class and metadata from current instance for external use.
	/// </summary>
	/// <param name="jClass">Output. Loaded class from current instance.</param>
	/// <param name="metadata">Output. Metadata for current instance.</param>
	/// <returns>Current instance instance.</returns>
	protected internal JLocalObject ForExternalUse(out JClassObject jClass, out ObjectMetadata metadata)
	{
		metadata = ILocalObject.CreateMetadata(this);
		jClass = this.Class;
		return this;
	}
}