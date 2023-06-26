namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
internal interface IArrayReference : IObjectReference, IEquatable<JArrayLocalRef> { }

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
/// <typeparam name="TArray">Type of <see cref="IArrayReference{TSelf}"/>.</typeparam>
internal interface IArrayReference<TArray> : IArrayReference, IObjectReference<TArray>
	where TArray : unmanaged, IArrayReference<TArray> { }