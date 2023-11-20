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
	private readonly HashSet<JLocalObject> _objects = new();

	/// <summary>
	/// Indicates whether the this instance is disposed.
	/// </summary>
	public Boolean IsDisposed => this._isDisposed.Value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	/// <param name="jLocal">The java object to load.</param>
	public ObjectLifetime(Boolean isNativeParameter, JLocalObject jLocal)
	{
		this._isNativeParameter = isNativeParameter;
		this._isDisposed = IMutableWrapper.Create<Boolean>();
		this.Load(jLocal);
	}

	/// <summary>
	/// Loads the given object in the current instance.
	/// </summary>
	/// <param name="jLocal">The java object to load.</param>
	public void Load(JLocalObject jLocal) => this._objects.Add(jLocal);
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
		if (!this._objects.Remove(jLocal))
			return false;

		Boolean result = !this._isNativeParameter && this._objects.Count == 0;
		return result;
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
		=> this._assignableTypes.IsAssignableTo<TDataType>();
	/// <summary>
	/// Sets current instance as assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	internal void SetAssignableTo<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>
		=> this._assignableTypes.SetAssignableTo<TDataType>();

	/// <summary>
	/// Prepares <paramref name="jGlobal"/> instance to be compatible with assignable types.
	/// </summary>
	/// <typeparam name="TGlobal">A <see cref="JGlobalBase"/> type.</typeparam>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns><paramref name="jGlobal"/> instance.</returns>
	public TGlobal Prepare<TGlobal>(TGlobal jGlobal) where TGlobal : JGlobalBase
		=> this._assignableTypes.Prepare(jGlobal);
}