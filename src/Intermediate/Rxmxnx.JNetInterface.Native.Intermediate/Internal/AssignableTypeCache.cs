namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This object stores the assignable types cache for a java object instance.
/// </summary>
internal sealed class AssignableTypeCache
{
	/// <summary>
	/// Set of assignable types.
	/// </summary>
	private readonly ConcurrentDictionary<Type, Boolean> _assignableTypes = new();

	/// <summary>
	/// Merges two caches.
	/// </summary>
	/// <param name="other">A <see cref="AssignableTypeCache"/> instance.</param>
	public void Merge(AssignableTypeCache other)
	{
		foreach (KeyValuePair<Type, Boolean> pair in other._assignableTypes)
			this._assignableTypes.TryAdd(pair.Key, pair.Value);
	}

	/// <summary>
	/// Indicates whether current instance is assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current instance is assignable to <typeparamref name="TDataType"/> type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean? IsAssignableTo<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>
	{
		if (JLocalObject.IsObjectType<TDataType>()) return true;
		JDataTypeMetadata metadata = IDataType.GetMetadata<TDataType>();
		return this._assignableTypes.TryGetValue(metadata.Type, out Boolean value) ? value : default(Boolean?);
	}
	/// <summary>
	/// Sets current instance as assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="isAssignable">Indicates whether current instance is assignable to <typeparamref name="TDataType"/> type.</param>
	internal void SetAssignableTo<TDataType>(Boolean isAssignable)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)IDataType.GetMetadata<TDataType>();
		this._assignableTypes.TryAdd(metadata.Type, isAssignable);
		if (!isAssignable) return;
		// If assignable should be assignable to interfaces and base types.
		foreach (JInterfaceTypeMetadata interfaceMetadata in metadata.Interfaces)
			this._assignableTypes.TryAdd(interfaceMetadata.Type, isAssignable);
		JClassTypeMetadata? baseMetadata = metadata.BaseMetadata;
		while (baseMetadata is not null && baseMetadata.Type != typeof(JLocalObject))
		{
			this._assignableTypes.TryAdd(baseMetadata.Type, isAssignable);
			baseMetadata = baseMetadata.BaseMetadata;
		}
	}
}