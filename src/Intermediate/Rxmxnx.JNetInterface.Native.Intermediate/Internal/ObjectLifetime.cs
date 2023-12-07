namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This object stores the lifetime for a java object instance.
/// </summary>
internal sealed record ObjectLifetime
{
	/// <summary>
	/// Cache of assignable types.
	/// </summary>
	private readonly AssignableTypeCache _assignableTypes = new();
	/// <summary>
	/// Internal <see cref="IEnvironment"/> instance.
	/// </summary>
	private readonly IEnvironment _env;
	/// <summary>
	/// Internal id.
	/// </summary>
	private readonly Int64 _id;
	/// <summary>
	/// Indicates whether the this instance is disposed.
	/// </summary>
	private readonly IMutableWrapper<Boolean> _isDisposed;
	/// <summary>
	/// Indicates whether the java object comes from a JNI parameter.
	/// </summary>
	private readonly Boolean _isNativeParameter;
	/// <summary>
	/// Internal hash set.
	/// </summary>
	private readonly Dictionary<Int64, WeakReference<JLocalObject>> _objects = new();

	/// <inheritdoc cref="ObjectLifetime.Class"/>
	private JClassObject? _class;
	/// <summary>
	/// Current <see cref="JGlobal"/> instance.
	/// </summary>
	private JGlobal? _global;
	/// <inheritdoc cref="ObjectLifetime.IsRealClass"/>
	private Boolean _isRealClass;
	/// <summary>
	/// Current <see cref="JWeak"/> instance.
	/// </summary>
	private JWeak? _weak;

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
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	/// <param name="jLocal">The java object to load.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public ObjectLifetime(Boolean isNativeParameter, IEnvironment env, JLocalObject jLocal,
		JGlobalBase? jGlobal = default)
	{
		this._env = env;
		this._isNativeParameter = isNativeParameter;
		this._isDisposed = IMutableWrapper.Create<Boolean>();
		this._global = (jGlobal as JGlobal)?.Load(this);
		this._weak = (jGlobal as JWeak)?.Load(this);
		this._id = jLocal.Id;
		this.Load(jLocal);
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
		this._class = this._env.ClassProvider.GetObjectClass(jLocal);
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
			this._global = this._env.ReferenceProvider.Create<JGlobal>(jLocal);
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
			this._weak = this._env.ReferenceProvider.Create<JWeak>(jLocal);
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
	public void SetClass(JObjectMetadata instanceMetadata)
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
		this._class = classHash == IDataType.GetHash<JClassObject>() ? jClass : this._env.ClassProvider.ClassObject;
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
		return this._env.ClassProvider.IsAssignableTo<TDataType>(jLocal);
	}

	/// <summary>
	/// Loads the given object in the current instance.
	/// </summary>
	/// <param name="jLocal">The java object to load.</param>
	public void Load(JLocalObject jLocal) => this._objects.Add(jLocal.Id, new(jLocal));
	/// <summary>
	/// Unloads the given object from the current instance.
	/// </summary>
	/// <param name="jLocal">The java object to unload.</param>
	/// <returns>
	/// <see langword="true"/> if the given instance was the only instance in the lifetime;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean Unload(JLocalObject jLocal)
	{
		if (!this._objects.Remove(jLocal.Id))
			return false;
		this.CleanObjects();
		Boolean result = !this._isNativeParameter && this._objects.Count == 0;
		return result;
	}
	/// <summary>
	/// Unloads current <see cref="JGlobalBase"/> instance.
	/// </summary>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance to unload.</param>
	public void UnloadGlobal(JGlobalBase jGlobal)
	{
		if (jGlobal.Equals(jGlobal))
			this._global = default;
		else if (jGlobal.Equals(jGlobal))
			this._weak = default;
	}

	/// <summary>
	/// Sets current instance as disposed.
	/// </summary>
	public void SetDisposed() => this._isDisposed.Value = true;
	/// <summary>
	/// Indicates whether current instance is assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current instance is assignable to <typeparamref name="TDataType"/> type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean IsAssignableTo<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>
		=> this._assignableTypes.IsAssignableTo<TDataType>().HasValue;
	/// <summary>
	/// Sets current instance as assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	public void SetAssignableTo<TDataType>(Boolean isAssignable)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		this._assignableTypes.SetAssignableTo<TDataType>(isAssignable);
		this._global?.AssignationCache.SetAssignableTo<TDataType>(isAssignable);
		this._weak?.AssignationCache.SetAssignableTo<TDataType>(isAssignable);
	}

	/// <summary>
	/// Clears current object dictionary.
	/// </summary>
	private void CleanObjects()
	{
		Int64[] keys = this._objects.Keys.ToArray();
		foreach (Int64 objId in keys)
		{
			if (!this._objects[objId].TryGetTarget(out _))
				this._objects.Remove(objId);
		}
	}
}