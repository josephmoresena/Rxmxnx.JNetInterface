namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a native java reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface INativeReferenceType : IFixedPointer, INativeType;