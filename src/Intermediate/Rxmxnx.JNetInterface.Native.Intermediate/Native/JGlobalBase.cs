namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a <see cref="JObject"/> instance which may remain valid across different
/// threads.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.JniThreadRequiredJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2953,
                 Justification = CommonConstants.JniThreadRequiredJustification)]
public abstract partial class JGlobalBase : JReferenceObject, IDisposable
{
	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	public IVirtualMachine VirtualMachine { get; }
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	public Type MetadataType => this.ObjectMetadata.GetType();
	/// <inheritdoc/>
	public override CString ObjectClassName => this.ObjectMetadata.ObjectClassName;
	/// <inheritdoc/>
	public override CString ObjectSignature => this.ObjectMetadata.ObjectSignature;

	/// <inheritdoc/>
	public void Dispose()
	{
		using IThread thread = this.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		this.Dispose(true, thread);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Retrieves a <typeparamref name="TReference"/> instance from current global instance.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> instance from current global instance.</returns>
	public TReference AsLocal<TReference>(IEnvironment env)
		where TReference : JReferenceObject, IReferenceType<TReference>
	{
		JReferenceTypeMetadata metadata = IReferenceType.GetMetadata<TReference>();
		JReferenceTypeMetadata typeMetadata =
			this.ObjectMetadata.TypeMetadata ?? IClassType.GetMetadata<JLocalObject>();
		JLocalObject jLocal = typeMetadata.ParseInstance(env, this);
		return (TReference)metadata.ParseInstance(jLocal, true);
	}

	/// <summary>
	/// Destructor.
	/// </summary>
	~JGlobalBase()
	{
		using IThread thread = this.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		this.Dispose(false, thread);
	}

	/// <summary>
	/// Indicates whether the current instance is valid.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if current instance is still valid; otherwise, <see langword="false"/>.
	/// </returns>
	public virtual Boolean IsValid(IEnvironment env) => !this._isDisposed && !this.IsDefault;

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
		IEnvironment env = jClass.Environment;
		return env.ClassFeature.IsInstanceOf(this, jClass);
	}

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="globalRef">A global object reference the value of current instance.</param>
	protected void SetValue(IntPtr globalRef)
	{
		if (globalRef == default) return;
		this._value.Value = globalRef;
		this._isDisposed = false;
	}
	/// <summary>
	/// Removes the association of <paramref name="jLocal"/> from this instance.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jLocal"/> was associated to this instance;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	protected Boolean Remove(JLocalObject jLocal)
	{
		ObjectLifetime objectLifetime = jLocal.Lifetime;
		return this._objects.TryRemove(objectLifetime.Id, out _);
	}

	/// <summary>
	/// Associates the current instance to <paramref name="objectLifetime"/>.
	/// </summary>
	/// <param name="objectLifetime"><see cref="ObjectLifetime"/> instance.</param>
	/// <returns>A valid <see cref="JGlobalBase"/> associated to <paramref name="objectLifetime"/>.</returns>
	internal JGlobalBase? Load(ObjectLifetime objectLifetime)
	{
		if (!this._objects.TryAdd(objectLifetime.Id, new(objectLifetime)))
			this._objects[objectLifetime.Id].SetTarget(objectLifetime);
		JGlobalBase? result = default;
		if (!this.IsValid(objectLifetime.Environment))
			return result;
		result = this;
		return result;
	}

	/// <summary>
	/// Indicates whether <paramref name="jGlobal"/> is valid.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if current instance is still valid; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean IsValid([NotNullWhen(true)] JGlobalBase? jGlobal, IEnvironment env)
		=> jGlobal is not null && jGlobal.IsValid(env);
}