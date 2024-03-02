namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local <c>java.lang.Object</c> instance.
/// </summary>
public partial class JLocalObject : JReferenceObject, IClassType<JLocalObject>
{
	/// <summary>
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment => this.Lifetime.Environment;
	/// <summary>
	/// Retrieves the class object from current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JClassObject Class => this.Lifetime.GetLoadClassObject(this);
	/// <summary>
	/// Retrieves the global object from current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JGlobal Global => this.Lifetime.GetLoadGlobalObject(this);
	/// <summary>
	/// Retrieves the global object from current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JWeak Weak => this.Lifetime.GetLoadWeakObject(this);
	/// <inheritdoc/>
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <inheritdoc cref="JObject.ObjectClassName"/>
	public override CString ObjectClassName => this.Lifetime.Class?.Name ?? UnicodeClassNames.Object;
	/// <inheritdoc cref="JObject.ObjectSignature"/>
	public override CString ObjectSignature
		=> this.Lifetime.Class?.ClassSignature ?? UnicodeObjectSignatures.ObjectSignature;
	/// <inheritdoc/>
	public override String ToString() => $"{this.Class.Name} {this.As<JObjectLocalRef>()}";

	/// <inheritdoc/>
	~JLocalObject() { this.Dispose(false); }

	/// <summary>
	/// Retrieves a <typeparamref name="TReference"/> instance from current local instance.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <param name="dispose">
	/// Optional. Indicates whether current instance should be disposed after casting.
	/// </param>
	/// <returns>A <typeparamref name="TReference"/> instance from current global instance.</returns>
	public TReference CastTo<TReference>(Boolean dispose = false)
		where TReference : JReferenceObject, IReferenceType<TReference>
	{
		IEnvironment env = this.Environment;
		if (this is TReference result) return result;
		if (JLocalObject.IsClassType<TReference>())
		{
			result = (TReference)(Object)env.ClassFeature.AsClassObject(this);
		}
		else
		{
			JLocalObject.Validate<TReference>(this);
			result = TReference.Create(this);
		}
		if (dispose) this.Dispose();
		return result;
	}
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
	/// <summary>
	/// Retrieves the class and metadata from current instance for external use.
	/// </summary>
	/// <param name="jClass">Output. Loaded class from current instance.</param>
	/// <param name="metadata">Output. Metadata for current instance.</param>
	/// <returns>Current instance instance.</returns>
	public JLocalObject ForExternalUse(out JClassObject jClass, out ObjectMetadata metadata)
	{
		metadata = ILocalObject.CreateMetadata(this);
		jClass = this.Class;
		return this;
	}
}