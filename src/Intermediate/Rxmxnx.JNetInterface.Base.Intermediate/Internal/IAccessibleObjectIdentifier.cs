namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="IAccessibleObjectIdentifier{TSelf}"/></typeparam>
internal interface IAccessibleObjectIdentifier<TSelf> : IFixedPointer, INative<TSelf>
	where TSelf : unmanaged, IAccessibleObjectIdentifier<TSelf> { }