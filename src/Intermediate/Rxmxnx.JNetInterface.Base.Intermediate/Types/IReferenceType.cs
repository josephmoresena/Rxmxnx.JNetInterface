namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReferenceType : IObject, IDataType, IDisposable
{
	/// <summary>
	/// Retrieves the metadata for given reference type.
	/// </summary>
	/// <typeparam name="TReference">Type of current java reference datatype.</typeparam>
	/// <returns>The <see cref="JReferenceTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
	public new static JReferenceTypeMetadata GetMetadata<TReference>()
		where TReference : JReferenceObject, IReferenceType<TReference>
		=> (JReferenceTypeMetadata)IDataType.GetMetadata<TReference>();
}

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
/// <typeparam name="TReference">Type of java reference type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface
	IReferenceType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] out TReference> :
		IReferenceType,
		IDataType<TReference> where TReference : JReferenceObject, IReferenceType<TReference>
{
	/// <summary>
	/// Retrieves the base types from current type.
	/// </summary>
	/// <returns>Enumerable of types.</returns>
	internal static IEnumerable<Type> GetBaseTypes()
	{
		Type? currentType = typeof(TReference).BaseType;
		while (currentType is not null && currentType != typeof(JReferenceObject))
		{
			yield return currentType;
			currentType = currentType.BaseType;
		}
	}
	/// <summary>
	/// Retrieves the interfaces types from current type.
	/// </summary>
	/// <returns>Enumerable of types.</returns>
	internal static IEnumerable<Type> GetInterfaceTypes()
	{
		Type[] interfaceTypes = typeof(TReference).GetInterfaces();
		foreach (Type interfaceType in interfaceTypes)
		{
			if (TReference.ExcludingTypes.Contains(interfaceType))
				continue;
			if (TReference.ExcludingGenericTypes.Contains(interfaceType))
				continue;
			yield return interfaceType;
		}
	}
}