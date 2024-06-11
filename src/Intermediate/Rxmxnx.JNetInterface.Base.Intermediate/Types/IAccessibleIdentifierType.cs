namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IAccessibleIdentifierType : IFixedPointer, INativeType;