namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a non-instantiable java class type instance.
/// </summary>
public interface IUninstantiableType : IClassType
{
	/// <summary>
	/// Throws an exception when trying to instantiate an object of type
	/// <typeparamref name="TUninstantiable"/>.
	/// </summary>
	/// <typeparam name="TUninstantiable">Type of java reference type.</typeparam>
	protected static TUninstantiable ThrowInstantiation<TUninstantiable>()
		where TUninstantiable : JLocalObject, IUninstantiableType, IUninstantiableType<TUninstantiable>
		=> CommonValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>();
}

/// <summary>
/// This interface exposes an object that represents a non-instantiable java class type instance.
/// </summary>
/// <typeparam name="TUninstantiable">Type of java reference type.</typeparam>
public interface
	IUninstantiableType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TUninstantiable> :
	IUninstantiableType,
	IClassType<TUninstantiable> where TUninstantiable : JLocalObject, IUninstantiableType<TUninstantiable>
{
	static TUninstantiable IClassType<TUninstantiable>.Create(ClassInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TUninstantiable>();
	static TUninstantiable IClassType<TUninstantiable>.Create(ObjectInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TUninstantiable>();
	static TUninstantiable IClassType<TUninstantiable>.Create(GlobalInitializer initializer)
		=> IUninstantiableType.ThrowInstantiation<TUninstantiable>();
}