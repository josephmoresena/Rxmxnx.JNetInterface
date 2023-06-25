namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a native java reference.
/// </summary>
internal interface INativeReference<TSelf, TValue> : IFixedPointer, INative<TSelf>, IReadOnlyReferenceable<TValue>
	where TSelf : unmanaged, INativeReference<TSelf, TValue> where TValue : unmanaged { }