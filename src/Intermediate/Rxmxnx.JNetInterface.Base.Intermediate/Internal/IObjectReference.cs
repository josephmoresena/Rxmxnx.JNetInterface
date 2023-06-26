namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
internal interface IObjectReference : IFixedPointer, IWrapper<JObjectLocalRef>, INative { }

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="IArrayReference{TSelf}"/>.</typeparam>
internal interface IObjectReference<TSelf> : IObjectReference, INative<TSelf>
	where TSelf : unmanaged, IObjectReference<TSelf> { }