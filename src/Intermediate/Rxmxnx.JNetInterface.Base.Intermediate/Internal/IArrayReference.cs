namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
internal interface IArrayReference : IObjectReference, IEquatable<JArrayLocalRef> { }

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="IArrayReference{TSelf}"/>.</typeparam>
internal interface IArrayReference<TSelf> : IArrayReference, IObjectReference<TSelf>
	where TSelf : unmanaged, IArrayReference<TSelf> { }