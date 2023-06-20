namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a <c>java.lang.Object</c> instance.
/// </summary>
public partial interface IObject
{
    /// <summary>
    /// Class name of current instance.
    /// </summary>
    CString ClassName { get; }
    /// <summary>
    /// Class signature of current instance.
    /// </summary>
    CString Signature { get; }

    /// <summary>
    /// Indicates whether current instance is default value.
    /// </summary>
    internal Boolean IsDefault { get; }

    /// <summary>
    /// Copy the sequence of bytes of current instance to <paramref name="span"/> at specified 
    /// <paramref name="offset"/>.
    /// </summary>
    /// <param name="span">Binary span.</param>
    /// <param name="offset">Offset in <paramref name="offset"/> to begin copy.</param>
    /// <returns>Number of bytes copied.</returns>
    internal void CopyTo(Span<Byte> span, ref Int32 offset);
    /// <summary>
    /// Copy the sequence of bytes of current instance to <paramref name="span"/> at specified 
    /// <paramref name="index"/>.
    /// </summary>
    /// <param name="span">Binary span.</param>
    /// <param name="index">Index to copy current value.</param>
    internal void CopyTo(Span<JValue> span, Int32 index);
}