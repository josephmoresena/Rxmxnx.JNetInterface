namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
public abstract record JPrimitiveMetadata
{
    /// <summary>
    /// JNI signature for an array of current primitive type.
    /// </summary>
    public abstract CString ArraySignature { get; }
    /// <summary>
    /// Size of current primitive type in bytes.
    /// </summary>
    public abstract Int32 SizeOf { get; }
    /// <summary>
    /// Managed type of internal value of <see cref="IPrimitive"/>.
    /// </summary>
    public abstract Type Type { get; }
}
