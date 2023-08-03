namespace Rxmxnx.JNetInterface.Internal.Types;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
internal interface IAccessibleObjectIdentifier : IFixedPointer, INativeType { }

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
/// <typeparam name="TAccessible">Type of <see cref="IAccessibleObjectIdentifier{TSelf}"/>.</typeparam>
internal interface IAccessibleObjectIdentifier<TAccessible> : IAccessibleObjectIdentifier, INativeType<TAccessible>
	where TAccessible : unmanaged, IAccessibleObjectIdentifier<TAccessible> { }