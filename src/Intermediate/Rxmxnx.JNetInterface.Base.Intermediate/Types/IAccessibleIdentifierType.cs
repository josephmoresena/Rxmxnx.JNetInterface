namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
internal interface IAccessibleIdentifierType : IFixedPointer, INativeType;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
/// <typeparam name="TAccessible">Type of <see cref="IAccessibleIdentifierType{TAccessible}"/>.</typeparam>
internal interface IAccessibleIdentifierType<TAccessible> : IAccessibleIdentifierType, INativeType<TAccessible>
	where TAccessible : unmanaged, IAccessibleIdentifierType<TAccessible>;