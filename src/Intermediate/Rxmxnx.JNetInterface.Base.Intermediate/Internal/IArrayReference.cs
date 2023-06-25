namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="IArrayReference{TSelf}"/></typeparam>
internal interface IArrayReference<TSelf> : IObjectReference<TSelf>, IEquatable<JArrayLocalRef>
	where TSelf : unmanaged, IArrayReference<TSelf> { }