namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java object global reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IObjectGlobalReferenceType : IFixedPointer, IWrapper<JObjectLocalRef>, INativeType;

/// <summary>
/// This interface exposes a java object global reference.
/// </summary>
/// <typeparam name="TGlobal">Type of <see cref="IObjectGlobalReferenceType{TGlobal}"/>.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IObjectGlobalReferenceType<TGlobal> : IObjectGlobalReferenceType, INativeType<TGlobal>
	where TGlobal : unmanaged, IObjectGlobalReferenceType<TGlobal>;