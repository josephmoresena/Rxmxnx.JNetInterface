namespace Rxmxnx.JNetInterface.Types.Inheritance;

/// <summary>
/// This interface defines the implementation of a <see cref="IInterfaceType"/> type in a <see cref="JLocalObject"/>
/// derivative type.
/// </summary>
/// <typeparam name="TImplementation">Type of implementing type.</typeparam>
/// <typeparam name="TInterface">Type of implemented interface.</typeparam>
public interface IInterfaceImplementation<
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TImplementation,
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface> : IDerivedType<TImplementation,
	TInterface>
	where TImplementation : JLocalObject, IReferenceType<TImplementation>,
	IInterfaceImplementation<TImplementation, TInterface>
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	static JDerivationKind IDerivedType<TImplementation, TInterface>.Type
		=> TImplementation.Kind == JTypeKind.Interface ? JDerivationKind.Extension : JDerivationKind.Implementation;

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
	static virtual explicit operator TInterface?(TImplementation? implementationInstance)
		=> TInterface.Create(implementationInstance);
}