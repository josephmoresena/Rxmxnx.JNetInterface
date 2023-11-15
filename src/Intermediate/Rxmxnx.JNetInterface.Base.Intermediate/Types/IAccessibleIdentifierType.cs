namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
internal interface IAccessibleObjectIdentifierType : IFixedPointer, INativeType { }

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
/// <typeparam name="TAccessible">Type of <see cref="IAccessibleObjectIdentifierType{TAccessible}"/>.</typeparam>
internal interface IAccessibleObjectIdentifierType<TAccessible> : IAccessibleObjectIdentifierType, INativeType<TAccessible>
	where TAccessible : unmanaged, IAccessibleObjectIdentifierType<TAccessible> { }