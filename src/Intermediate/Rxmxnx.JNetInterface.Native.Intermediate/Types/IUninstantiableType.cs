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
	/// <typeparam name="TUninstantiable">A <see cref="IUninstantiableType{TReference}"/> type.</typeparam>
	/// <returns>A <typeparamref name="TUninstantiable"/> instance.</returns>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static TUninstantiable ThrowInstantiation<TUninstantiable>()
		where TUninstantiable : JReferenceObject, IUninstantiableType<TUninstantiable>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TUninstantiable>();
		throw new InvalidOperationException($"{metadata.ClassName} not is an instantiable class.");
	}
}

/// <summary>
/// This interface exposes an object that represents a non-instantiable java reference type instance.
/// </summary>
/// <typeparam name="TUninstantiable">Type of java reference type.</typeparam>
public interface IUninstantiableType<out TUninstantiable> : IUninstantiableType, IReferenceType<TUninstantiable>
	where TUninstantiable : JReferenceObject, IUninstantiableType<TUninstantiable>
{
	static TUninstantiable IReferenceType<TUninstantiable>.Create(ClassInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TUninstantiable>();
	static TUninstantiable IReferenceType<TUninstantiable>.Create(ObjectInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TUninstantiable>();
	static TUninstantiable IReferenceType<TUninstantiable>.Create(GlobalInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TUninstantiable>();
}