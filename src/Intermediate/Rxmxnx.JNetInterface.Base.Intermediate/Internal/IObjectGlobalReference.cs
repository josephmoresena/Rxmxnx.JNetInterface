namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java object global reference.
/// </summary>
internal interface IObjectGlobalReference : IFixedPointer, IWrapper<JObjectLocalRef>, INative { }

/// <summary>
/// This interface exposes a java object global reference.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="IObjectGlobalReference{TSelf}"/>.</typeparam>
internal interface IObjectGlobalReference<TSelf> : IObjectGlobalReference, INative<TSelf>
	where TSelf : unmanaged, IObjectGlobalReference<TSelf> { }