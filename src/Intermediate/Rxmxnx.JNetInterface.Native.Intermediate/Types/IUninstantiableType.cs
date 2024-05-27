namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a non-instantiable java class type instance.
/// </summary>
/// <typeparam name="TUninstantiable">Type of java reference type.</typeparam>
public interface
	IUninstantiableType<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TUninstantiable> : IClassType<
	TUninstantiable> where TUninstantiable : JReferenceObject, IUninstantiableType<TUninstantiable>
{
	static TUninstantiable IClassType<TUninstantiable>.Create(ClassInitializer initializer)
		=> CommonValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>();
	static TUninstantiable IClassType<TUninstantiable>.Create(ObjectInitializer initializer)
		=> CommonValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>();
	static TUninstantiable IClassType<TUninstantiable>.Create(GlobalInitializer initializer)
		=> CommonValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>();
}