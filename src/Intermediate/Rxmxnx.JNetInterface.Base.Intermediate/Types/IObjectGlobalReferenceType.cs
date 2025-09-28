namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java object global reference.
/// </summary>
internal interface IObjectGlobalReferenceType : INativeReferenceType, IWrapper<JObjectLocalRef>;