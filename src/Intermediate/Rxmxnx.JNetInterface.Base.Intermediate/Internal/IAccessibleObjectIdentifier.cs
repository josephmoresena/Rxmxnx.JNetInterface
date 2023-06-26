namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
internal interface IAccessibleObjectIdentifier : IFixedPointer, INative { }

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
/// <typeparam name="TAccessible">Type of <see cref="IAccessibleObjectIdentifier{TSelf}"/>.</typeparam>
internal interface IAccessibleObjectIdentifier<TAccessible> : IAccessibleObjectIdentifier, INative<TAccessible>
	where TAccessible : unmanaged, IAccessibleObjectIdentifier<TAccessible> { }