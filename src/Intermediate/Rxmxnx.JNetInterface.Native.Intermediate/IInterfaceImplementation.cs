namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface defines the implementation of a <see cref="IInterfaceType"/> type in a <see cref="JLocalObject"/>
/// derivative type.
/// </summary>
/// <typeparam name="TImplementation">Type of implementing type.</typeparam>
/// <typeparam name="TInterface">Type of implemented interface.</typeparam>
public interface
	IInterfaceImplementation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TImplementation,
		TInterface> : IDerivedType<TImplementation, TInterface>
	where TImplementation : JLocalObject, IInterfaceImplementation<TImplementation, TInterface>
	where TInterface : JInterfaceObject, IInterfaceType<TInterface>
{
	static DerivationKind IDerivedType<TImplementation, TInterface>.Type => default;
	/*=> IReferenceType<TImplementation>.IsDerivedFrom(typeof(JInterfaceObject)) ?
		DerivationKind.Extension :
		DerivationKind.Implementation;*/

	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TInterface"/> to <typeparamref name="TImplementation"/>.
	/// </summary>
	/// <param name="interfaceInstance">A <typeparamref name="TInterface"/> to explicit convert.</param>
	static virtual explicit operator TImplementation?(TInterface? interfaceInstance)
		=> TImplementation.Create(interfaceInstance);
	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TImplementation"/> to <typeparamref name="TInterface"/>.
	/// </summary>
	/// <param name="implementationInstance">A <typeparamref name="TImplementation"/> to implicitly convert.</param>
	static virtual implicit operator TInterface?(TImplementation? implementationInstance)
		=> TInterface.Create(implementationInstance);
}