namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a non-instantiable java reference type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IUninstantiableType : IReferenceType
{
	/// <summary>
	/// Always throws an exception.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IUninstantiableType{TReference}"/> type.</typeparam>
	/// <returns>A <see cref="TReference"/> instance.</returns>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static TReference ThrowInstantiation<TReference>()
		where TReference : JReferenceObject, IUninstantiableType<TReference>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TReference>();
		throw new InvalidOperationException($"{metadata.ClassName} not is an instantiable class.");
	}
}

/// <summary>
/// This interface exposes an object that represents a non-instantiable java reference type instance.
/// </summary>
/// <typeparam name="TReference">Type of java reference type.</typeparam>
public interface IUninstantiableType<out TReference> : IUninstantiableType, IReferenceType<TReference>
	where TReference : JReferenceObject, IUninstantiableType<TReference>
{
	static TReference IReferenceType<TReference>.Create(ClassInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TReference>();
	static TReference IReferenceType<TReference>.Create(ObjectInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TReference>();
	static TReference IReferenceType<TReference>.Create(GlobalInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TReference>();
}