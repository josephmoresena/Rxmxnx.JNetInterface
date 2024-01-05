namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that primitive equatable value.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IPrimitiveEquatable : IEquatable<JPrimitiveObject>, IEquatable<IPrimitiveType>;