namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a native java reference.
/// </summary>
internal interface INativeReference : IFixedPointer, INative { }

/// <summary>
/// This interface exposes a native java reference.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="INativeReference{TSelf}"/>.</typeparam>
internal interface INativeReference<TSelf> : INativeReference, INative<TSelf>
	where TSelf : unmanaged, INativeReference<TSelf> { }

/// <summary>
/// This interface exposes a native java reference.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="INativeReference{TSelf, TValue}"/>.</typeparam>
/// <typeparam name="TValue">Type of referenced value.</typeparam>
internal interface INativeReference<TSelf, TValue> : INativeReference<TSelf>, IReadOnlyReferenceable<TValue>
	where TSelf : unmanaged, INativeReference<TSelf, TValue> where TValue : unmanaged { }