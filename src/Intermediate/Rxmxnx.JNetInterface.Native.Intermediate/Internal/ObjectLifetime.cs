namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This object stores the lifetime for a java object instance.
/// </summary>
internal sealed partial class ObjectLifetime : IDisposable
{
	/// <summary>
	/// Internal id.
	/// </summary>
	public Int64 Id => this._id;
	/// <summary>
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment => this._env;
	/// <summary>
	/// Indicates whether the this instance is disposed.
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
	public ObjectLifetime(IEnvironment env, JLocalObject jLocal, JObjectLocalRef localRef = default)
	{
		this._env = env;
		this._isDisposed = IMutableWrapper.Create<Boolean>();
		this._id = jLocal.Id;
		this._value = IMutableReference<JObjectLocalRef>.Create(localRef);
		this._isDisposable = jLocal is not JClassObject;
		this.Load(jLocal);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jLocal">The java object to load.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public ObjectLifetime(IEnvironment env, JLocalObject jLocal, JGlobalBase? jGlobal)
	{
		this._env = env;
		this._isDisposed = IMutableWrapper.Create<Boolean>();
		this._global = (jGlobal as JGlobal)?.Load(this);
		this._weak = (jGlobal as JWeak)?.Load(this);
		this._id = jLocal.Id;
		this._value = IMutableReference<JObjectLocalRef>.Create();
		this._isDisposable = jLocal is not JClassObject;
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
	/// <param name="localRef">A local object reference the value of current instance.</param>
	public void SetValue(JLocalObject jLocal, JObjectLocalRef localRef)
	{
		if (localRef == default) return;
		this.LoadNewValue(jLocal, localRef);
		this.Secondary?.LoadNewValue(jLocal, localRef);
	}
	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <typeparam name="TValue">Type of <see langword="IObjectReference"/> instance.</typeparam>
	/// <param name="jLocal">The java object to load.</param>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	public void SetValue<TValue>(JLocalObject jLocal, TValue localRef) where TValue : unmanaged, IObjectReferenceType
	{
		if (localRef.Equals(default)) return;
		this.LoadNewValue(jLocal, localRef.Value);
		this.Secondary?.LoadNewValue(jLocal, localRef.Value);
	}
	/// <summary>
	/// Retrieves the loaded global object for current instance.
	/// </summary>
	/// <returns>The loaded <see cref="JGlobalBase"/> object for current instance.</returns>
	public JGlobalBase? GetGlobalObject()
	{
		if (this._global is not null && this._global.IsValid(this._env))
			return this._global;
		if (this._weak is not null && this._weak.IsValid(this._env))
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
		this._class = this._env.ClassFeature.GetObjectClass(jLocal);
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
		if (!JGlobalBase.IsValid(this._global, this._env))
		{
			this._global = this._env.ReferenceFeature.Create<JGlobal>(jLocal);
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
		if (!JGlobalBase.IsValid(this._weak, this._env))
		{
			this._weak = this._env.ReferenceFeature.Create<JWeak>(jLocal);
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
	/// <param name="instanceMetadata">The object metadata for current instance.</param>
	public void SetClass(ObjectMetadata instanceMetadata)
	{
		this._class = instanceMetadata.GetClass(this._env);
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
	/// Sets the class object for a local class instance.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="classHash">A <see cref="JClassObject"/> hash.</param>
	public void SetClassClass(JClassObject jClass, String? classHash)
	{
		this._class = classHash == IDataType.GetHash<JClassObject>() ? jClass : this._env.ClassFeature.ClassObject;
		this._isRealClass = true;
	}
	/// <summary>
	/// Indicates whether a local instance is assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if local instance is assignable to <typeparamref name="TDataType"/> type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean IsAssignableTo<TDataType>(JLocalObject jLocal)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		if (JGlobalBase.IsValid(this._global, this._env))
			return this._global.IsAssignableTo<TDataType>();
		if (JGlobalBase.IsValid(this._weak, this._env))
			return this._weak.IsAssignableTo<TDataType>();
		return this.IsAssignableTo<TDataType>() ?? this._env.ClassFeature.IsAssignableTo<TDataType>(jLocal);
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
		if (this._objects.Count > this._classCounter.Value || this._env.ReferenceFeature.IsParameter(jLocal)) return;
		this._env.ReferenceFeature.Unload(jLocal);
		this.Dispose();
	}
	/// <summary>
	/// Unloads current <see cref="JGlobalBase"/> instance.
	/// </summary>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance to unload.</param>
	public void UnloadGlobal(JGlobalBase jGlobal)
	{
		if (Object.ReferenceEquals(jGlobal, this._global))
			this._global = default;
		else if (Object.ReferenceEquals(jGlobal, this._global))
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
		this._secondary.SetTarget(other);
		other._secondary.SetTarget(other);
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
	/// Indicates whether current instance has a valid <typeparamref name="TGlobal"/> instance.
	/// </summary>
	/// <typeparam name="TGlobal">A <see cref="JGlobalBase"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current instance has a valid <typeparamref name="TGlobal"/> instance.;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean HasValidGlobal<TGlobal>() where TGlobal : JGlobalBase
	{
		TGlobal? jGlobal = this._global as TGlobal ?? this._weak as TGlobal;
		return jGlobal is not null && !jGlobal.IsDefault && jGlobal.IsValid(this._env);
	}
	/// <summary>
	/// Sets current <see cref="JGlobal"/> object.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
	public void SetGlobal(JGlobal jGlobal) => this._global = jGlobal;
}