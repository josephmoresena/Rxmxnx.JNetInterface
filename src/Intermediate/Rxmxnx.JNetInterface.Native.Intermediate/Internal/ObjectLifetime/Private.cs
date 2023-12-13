namespace Rxmxnx.JNetInterface.Internal;

internal partial class ObjectLifetime
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
	/// Internal hash set.
	/// </summary>
	private readonly Dictionary<Int64, WeakReference<JLocalObject>> _objects = new();
	/// <summary>
	/// Weak reference to <see cref="ObjectLifetime"/> instance.
	/// </summary>
	private readonly WeakReference<ObjectLifetime?> _secondary = new(default);
	/// <summary>
	/// Internal value.
	/// </summary>
	private readonly IMutableReference<JObjectLocalRef> _value;

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
	/// Indicates whether current instance is not disposable.
	/// </summary>
	private readonly Boolean _isDisposable;

	/// <summary>
	/// Secondary <see cref="ObjectLifetime"/>
	/// </summary>
	private ObjectLifetime? Secondary => this._secondary.TryGetTarget(out ObjectLifetime? result) ? result : default;

	/// <summary>
	/// Synchronizes global objects.
	/// </summary>
	/// <param name="other">A <see cref="ObjectLifetime"/> instance.</param>
	private void SynchronizeGlobal(ObjectLifetime other)
	{
		if (this._global is null) this._global = other._global;
		else if (other._global is null) other._global = this._global;
		else this._global.Synchronize(other._global);

		if (this._weak is null) this._weak = other._weak;
		else if (other._weak is null) other._weak = this._weak;
		else this._weak.Synchronize(other._weak);
	}
	/// <summary>
	/// Indicates whether current instance is assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current instance is assignable to <typeparamref name="TDataType"/> type,
	/// <see langword="null"/> if unknown; otherwise, <see langword="false"/>.
	/// </returns>
	private Boolean? IsAssignableTo<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>
		=> this._assignableTypes.IsAssignableTo<TDataType>() ??
			this.Secondary?._assignableTypes.IsAssignableTo<TDataType>();

	/// <summary>
	/// Synchronizes object dictionary.
	/// </summary>
	/// <param name="objects">External object dictionary.</param>
	private void SynchronizeObjects(IReadOnlyDictionary<Int64, WeakReference<JLocalObject>> objects)
	{
		foreach (Int32 id in objects.Keys)
			this._objects[id] = objects[id];
	}
	/// <inheritdoc cref="SetAssignableTo{TDataType}(Boolean)"/>
	private void LoadAssignation<TDataType>(Boolean isAssignable)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		this._assignableTypes.SetAssignableTo<TDataType>(isAssignable);
		this._global?.AssignationCache.SetAssignableTo<TDataType>(isAssignable);
		this._weak?.AssignationCache.SetAssignableTo<TDataType>(isAssignable);
	}
	/// <inheritdoc cref="SetValue(JLocalObject, JObjectLocalRef)"/>
	private void LoadNewValue(JLocalObject jLocal, JObjectLocalRef localRef)
	{
		this._value.Value = localRef;
		this._isDisposed.Value = false;
		this.Load(jLocal);
	}
	/// <summary>
	/// Unloads the given object from the current instance.
	/// </summary>
	/// <param name="id">Object identifier.</param>
	/// <param name="isClass">Indicates whether object instance is a class.</param>
	/// <returns>
	/// <see langword="true"/> if the given instance was the only instance in the lifetime;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	private void Unload(Int64 id, Boolean isClass)
	{
		if (!isClass) this._objects.Remove(id);
		this.CleanObjects();
	}
	/// <summary>
	/// Clears current object dictionary.
	/// </summary>
	private void CleanObjects()
	{
		Int64[] keys = this._objects.Keys.ToArray();
		foreach (Int64 objId in keys)
		{
			if (!this._objects[objId].TryGetTarget(out JLocalObject? jLocal) && jLocal is not JClassObject)
				this._objects.Remove(objId);
		}
	}
}