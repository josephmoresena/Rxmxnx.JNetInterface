namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java object global reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IObjectGlobalReferenceType : IFixedPointer, IWrapper<JObjectLocalRef>, INativeType;