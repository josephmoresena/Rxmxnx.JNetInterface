namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
internal interface IAccessibleObjectIdentifier : IFixedPointer, INative { }

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="IAccessibleObjectIdentifier{TSelf}"/>.</typeparam>
internal interface IAccessibleObjectIdentifier<TSelf> : IAccessibleObjectIdentifier, INative<TSelf>
	where TSelf : unmanaged, IAccessibleObjectIdentifier<TSelf> { }