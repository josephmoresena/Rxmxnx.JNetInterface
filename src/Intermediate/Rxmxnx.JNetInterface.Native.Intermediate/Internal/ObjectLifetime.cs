namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This object stores the lifetime for a java object instance.
/// </summary>
/// <remarks>
/// Constructor.
/// </remarks>
/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
/// <param name="localRef">Local object reference.</param>
/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
/// <param name="isDisposable">Indicates whether the current instance is disposable.</param>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2292,
                 Justification = CommonConstants.PublicInitPrivateSetJustification)]
internal sealed partial class ObjectLifetime(
	IEnvironment env,
	JObjectLocalRef localRef,
	JReferenceObject jObject,
	Boolean isDisposable) : IDisposable
{
	/// <summary>
	/// Internal id.
	/// </summary>
	public Int64 Id { get; } = jObject.Id;
	/// <summary>
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment { get; } = env;
	/// <summary>
	/// Indicates whether this instance is disposed.
	/// </summary>
	public Boolean IsDisposed => this._isDisposed.Value;

	/// <summary>
	/// Class object.
	/// </summary>
	public JClassObject? Class
	{
		get => this._class;
		init => this._class = value;
	}
	/// <summary>
	/// Indicates whether the current class is the real object class.
	/// </summary>
	public Boolean IsRealClass
	{
		get => this._isRealClass;
		init => this._isRealClass = value;
	}
	/// <summary>
	/// Retrieves current value as bytes.
	/// </summary>
	public ReadOnlySpan<Byte> Span => this._value.Reference.AsBytes();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jLocal">The java object to load.</param>
	/// <param name="localRef">Local object reference.</param>
	public ObjectLifetime(IEnvironment env, JLocalObject jLocal, JObjectLocalRef localRef = default) : this(
		env, localRef, jLocal, jLocal is not JClassObject)
	{
		this.Load(jLocal);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jLocal">The java object to load.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public ObjectLifetime(IEnvironment env, JLocalObject jLocal, JGlobalBase? jGlobal) : this(
		env, default, jLocal, jLocal is not JClassObject)
	{
		this._global = (jGlobal as JGlobal)?.Load(this);
		this._weak = (jGlobal as JWeak)?.Load(this);
		this.Load(jLocal);
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		if (this._isDisposed.Value) return;
		this._value.Value = default;
		this._isDisposed.Value = true;
		try
		{
			this.Secondary?.Dispose();
		}
		finally
		{
			if (!this._isDisposable) this._isDisposed.Value = true;
		}
	}
	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="jLocal">The java object to load.</param>
	/// <param name="localRef">A local object reference the value of the current instance.</param>
	public void SetValue(JLocalObject jLocal, JObjectLocalRef localRef)
	{
		this.LoadNewValue(jLocal, localRef);
		this.Secondary?.LoadNewValue(jLocal, localRef);
	}
	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <typeparam name="TValue">Type of <see langword="IObjectReference"/> instance.</typeparam>
	/// <param name="jLocal">The java object to load.</param>
	/// <param name="localRef">A local object reference the value of the current instance.</param>
	public void SetValue<TValue>(JLocalObject jLocal, TValue localRef) where TValue : unmanaged, IObjectReferenceType
	{
		this.LoadNewValue(jLocal, localRef.Value);
		this.Secondary?.LoadNewValue(jLocal, localRef.Value);
	}
	/// <summary>
	/// Retrieves the loaded global object for the current instance.
	/// </summary>
	/// <returns>The loaded <see cref="JGlobalBase"/> object for the current instance.</returns>
	public JGlobalBase? GetGlobalObject()
	{
		if (this._global is not null && this._global.IsMinimalValid(this.Environment))
			return this._global;
		if (this._weak is not null && this._weak.IsMinimalValid(this.Environment))
			return this._weak;
		return default;
	}
	/// <summary>
	/// Gets or loads the class object in the <paramref name="jLocal"/> instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns><see cref="JClassObject"/> for <paramref name="jLocal"/>.</returns>
	public JClassObject GetLoadClassObject(JLocalObject jLocal)
	{
		if (this._class is not null && this._isRealClass) return this._class;
		this._class = this.Environment.ClassFeature.GetObjectClass(jLocal);
		this._isRealClass = true;
		return this._class;
	}
	/// <summary>
	/// Gets or loads the <see cref="JGlobal"/> object in the <paramref name="jLocal"/> instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns><see cref="JGlobal"/> for <paramref name="jLocal"/>.</returns>
	public JGlobal GetLoadGlobalObject(JLocalObject jLocal)
	{
		if (!JGlobalBase.IsValid(this._global, this.Environment))
		{
			this._global = this.Environment.ReferenceFeature.Create<JGlobal>(jLocal);
			this._global.AssignationCache.Merge(this._assignableTypes);
		}
		else
		{
			this._global.RefreshMetadata(jLocal);
		}
		return this._global;
	}
	/// <summary>
	/// Gets or loads the <see cref="JWeak"/> object in the <paramref name="jLocal"/> instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns><see cref="JGlobal"/> for <paramref name="jLocal"/>.</returns>
	public JWeak GetLoadWeakObject(JLocalObject jLocal)
	{
		if (!JGlobalBase.IsValid(this._weak, this.Environment))
		{
			this._weak = this.Environment.ReferenceFeature.Create<JWeak>(jLocal);
			this._weak.AssignationCache.Merge(this._assignableTypes);
		}
		else
		{
			this._weak.RefreshMetadata(jLocal);
		}
		return this._weak;
	}

	/// <summary>
	/// Sets instance class.
	/// </summary>
	/// <param name="instanceMetadata">The object metadata for the current instance.</param>
	public void SetClass(ObjectMetadata instanceMetadata)
	{
		if (!instanceMetadata.ObjectClassName.AsSpan().SequenceEqual(this._class?.Name))
			this._class = instanceMetadata.GetClass(this.Environment);
		this._isRealClass = true;
	}
	/// <summary>
	/// Sets instance class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public void SetClass(JClassObject? jClass)
	{
		this._class = jClass ?? this._class;
		if (this._class?.IsFinal == true) this._isRealClass = true;
	}
	/// <summary>
	/// Indicates whether a local instance is assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the local instance is assignable to <typeparamref name="TDataType"/> type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean InstanceOf<TDataType>(JLocalObject jLocal) where TDataType : JReferenceObject, IDataType<TDataType>
	{
		Boolean? result = this.InstanceOf<TDataType>();

		if (result.HasValue)
			return result.Value;
		if (JGlobalBase.IsValid(this._global, this.Environment))
			return this._global.InstanceOf<TDataType>();
		return JGlobalBase.IsValid(this._weak, this.Environment) ?
			this._weak.InstanceOf<TDataType>() :
			this.Environment.ClassFeature.IsInstanceOf<TDataType>(jLocal);
	}

	/// <summary>
	/// Loads the given object in the current instance.
	/// </summary>
	/// <param name="jLocal">The java object to load.</param>
	public void Load(JLocalObject jLocal)
	{
		Boolean isClass = jLocal is JClassObject;
		ObjectLifetime.Load(this, isClass, jLocal);
		ObjectLifetime.Load(this.Secondary, isClass, jLocal);
	}
	/// <summary>
	/// Unloads the given object from the current instance.
	/// </summary>
	/// <param name="jLocal">The java object to unload.</param>
	/// <returns>
	/// <see langword="true"/> if the given instance was the only instance in the lifetime;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public void Unload(JLocalObject jLocal)
	{
		if (!this._objects.ContainsKey(jLocal.Id)) return;
		Boolean isClass = jLocal is JClassObject;
		this.Unload(jLocal.Id, isClass);
		this.Secondary?.Unload(jLocal.Id, isClass);
		if (this._objects.Count > this._classCounter.Value ||
		    this.Environment.ReferenceFeature.IsParameter(jLocal)) return;
		if (this.Environment.ReferenceFeature.Unload(jLocal)) this.Dispose();
	}
	/// <summary>
	/// Unloads current <see cref="JGlobalBase"/> instance.
	/// </summary>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance to unload.</param>
	public void UnloadGlobal(JGlobalBase jGlobal)
	{
		if (Object.ReferenceEquals(jGlobal, this._global))
			this._global = default;
		else if (Object.ReferenceEquals(jGlobal, this._weak))
			this._weak = default;
		if (Object.ReferenceEquals(jGlobal, this.Secondary?._global))
			this.Secondary._global = default;
		if (Object.ReferenceEquals(jGlobal, this.Secondary?._weak))
			this.Secondary._weak = default;
	}
	/// <summary>
	/// Sets current instance as assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	public void SetAssignableTo<TDataType>(Boolean isAssignable)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		this.LoadAssignation<TDataType>(isAssignable);
		this.Secondary?.LoadAssignation<TDataType>(isAssignable);
	}
	/// <summary>
	/// Synchronizes current instance with <paramref name="other"/>.
	/// </summary>
	/// <param name="other">A <see cref="ObjectLifetime"/> instance.</param>
	public void Synchronize(ObjectLifetime other)
	{
		if (Object.ReferenceEquals(this, other)) return;
		this._secondary.SetTarget(other);
		other._secondary.SetTarget(this);
		this._assignableTypes.Merge(other._assignableTypes);
		this.SynchronizeGlobal(other);
		this.SynchronizeObjects(other._objects);
		other.SynchronizeObjects(this._objects);
	}
	/// <summary>
	/// Retrieves cacheable instance.
	/// </summary>
	/// <returns>A <see cref="ObjectLifetime"/> cacheable instance.</returns>
	public ObjectLifetime GetCacheable()
	{
		if (!this._isDisposable || this.Secondary is null || this.Secondary._isDisposable) return this;
		return this.Secondary;
	}
	/// <summary>
	/// Indicates whether the current instance has a valid <typeparamref name="TGlobal"/> instance.
	/// </summary>
	/// <typeparam name="TGlobal">A <see cref="JGlobalBase"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if the current instance has a valid <typeparamref name="TGlobal"/> instance.;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean HasValidGlobal<TGlobal>() where TGlobal : JGlobalBase
	{
		TGlobal? jGlobal = this._global as TGlobal ?? this._weak as TGlobal;
		return jGlobal is not null && !jGlobal.IsDefault && jGlobal.IsValid(this.Environment);
	}
	/// <summary>
	/// Sets current <see cref="JGlobal"/> object.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	public void SetGlobal(JGlobalBase jGlobal)
	{
		switch (jGlobal)
		{
			case JGlobal jGlobalGlobal:
				this._global = jGlobalGlobal;
				break;
			case JWeak jWeak:
				this._weak = jWeak;
				break;
		}
	}
}