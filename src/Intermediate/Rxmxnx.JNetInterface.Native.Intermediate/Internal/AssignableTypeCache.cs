namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This object stores the assignable types cache for a java object instance.
/// </summary>
public sealed record AssignableTypeCache
{
	/// <summary>
	/// Set of assignable types.
	/// </summary>
	private readonly ConcurrentDictionary<Type, Action<JReferenceObject>> _assignableTypes = new();

	/// <summary>
	/// Indicates whether current instance is assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current instance is assignable to <typeparamref name="TDataType"/> type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean IsAssignableTo<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>
		=> this._assignableTypes.ContainsKey(typeof(TDataType));
	/// <summary>
	/// Sets current instance as assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	internal void SetAssignableTo<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>
		=> this._assignableTypes.TryAdd(typeof(TDataType), AssignableTypeCache.SetAssignableTo<TDataType>);

	/// <summary>
	/// Sets <paramref name="jGlobal"/> as assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	private static void SetAssignableTo<TDataType>(JReferenceObject jGlobal)
		where TDataType : JReferenceObject, IDataType<TDataType>
		=> jGlobal.SetAssignableTo<TDataType>();

	/// <summary>
	/// Prepares <paramref name="jReference"/> instance to be compatible with assignable types.
	/// </summary>
	/// <typeparam name="TReferenceObject">A <see cref="JGlobalBase"/> type.</typeparam>
	/// <param name="jReference">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns><paramref name="jReference"/> instance.</returns>
	public TReferenceObject Prepare<TReferenceObject>(TReferenceObject jReference)
		where TReferenceObject : JReferenceObject
	{
		foreach (Type assignableType in this._assignableTypes.Keys)
			this._assignableTypes[assignableType](jReference);
		return jReference;
	}
}