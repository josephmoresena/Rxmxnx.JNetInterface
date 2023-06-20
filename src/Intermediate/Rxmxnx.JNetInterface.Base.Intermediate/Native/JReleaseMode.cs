namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Modes of primitive array releasing.
/// </summary>
public enum JReleaseMode : Int32
{
    /// <summary>
    /// Copy back the content and free the elems buffer.
    /// </summary>
    Free = 0,
    /// <summary>
    /// Copy back the content but do not free the elems buffer (<c>JNI_COMMIT</c>).
    /// </summary>
    Commit = 1,
    /// <summary>
    /// Free the buffer without copying back the possible changes (<c>JNI_ABORT</c>).
    /// </summary>
    Abort = 2,
}