namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This object stores the lifetime for a java object instance.
/// </summary>
internal sealed record ObjectLifetime
{
	/// <summary>
	/// Indicates whether the java object comes from a JNI parameter.
	/// </summary>
	private readonly Boolean _isNativeParameter;
	/// <summary>
	/// Internal hash set.
	/// </summary>
	private readonly HashSet<JLocalObject> _objects = new();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	/// <param name="jLocal">The java object to load.</param>
	public ObjectLifetime(Boolean isNativeParameter, JLocalObject jLocal)
	{
		this._isNativeParameter = isNativeParameter;
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
}