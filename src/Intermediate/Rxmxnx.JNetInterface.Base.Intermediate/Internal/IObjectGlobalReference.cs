namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java object global reference.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="IObjectGlobalReference{TSelf}"/></typeparam>
internal interface IObjectGlobalReference<TSelf> : IFixedPointer, IWrapper<JObjectLocalRef>, INative<TSelf>
	where TSelf : unmanaged, IObjectGlobalReference<TSelf> { }