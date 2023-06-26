namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
internal interface IObjectReference : IFixedPointer, IWrapper<JObjectLocalRef>, INative { }

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
/// <typeparam name="TObject">Type of <see cref="IArrayReference{TSelf}"/>.</typeparam>
internal interface IObjectReference<TObject> : IObjectReference, INative<TObject>
	where TObject : unmanaged, IObjectReference<TObject> { }