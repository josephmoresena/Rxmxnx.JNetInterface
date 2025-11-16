namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
internal interface IObjectReferenceType : INativeReferenceType, IWrapper<JObjectLocalRef>;