namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a non-instantiable java reference type instance.
/// </summary>
/// <typeparam name="TUninstantiable">Type of java reference type.</typeparam>
public interface IUninstantiableType<out TUninstantiable> : IReferenceType<TUninstantiable>
	where TUninstantiable : JReferenceObject, IUninstantiableType<TUninstantiable>
{
	static TUninstantiable IReferenceType<TUninstantiable>.Create(ClassInitializer initializer)
		=> ValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>();
	static TUninstantiable IReferenceType<TUninstantiable>.Create(ObjectInitializer initializer)
		=> ValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>();
	static TUninstantiable IReferenceType<TUninstantiable>.Create(GlobalInitializer initializer)
		=> ValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>();
}