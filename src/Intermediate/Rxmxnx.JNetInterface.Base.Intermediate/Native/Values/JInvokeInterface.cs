namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
///     <c>JNIInvokeInterface_</c> struct. Contains all pointers to the functions of the Invocation API.
/// </summary>
public readonly partial struct JInvokeInterface : INative<JNativeInterface>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JNativeInterface;

    /// <summary>
    ///     Internal reserved entries.
    /// </summary>
    private readonly ComReserved _reserved;

    /// <summary>
    ///     Pointer to <c>DestroyJavaVM</c> function. Unloads a JVM and reclaims its resources.
    /// </summary>
    internal readonly IntPtr DestroyJavaVMPointer { get; init; }
    /// <summary>
    ///     Pointer to <c>AttachCurrentThread</c> function. Attaches the current thread to a JVM.
    /// </summary>
    internal readonly IntPtr AttachCurrentThreadPointer { get; init; }
    /// <summary>
    ///     Pointer to <c>DetachCurrentThread</c> function. Detaches the current thread from a JVM.
    /// </summary>
    internal readonly IntPtr DetachCurrentThreadPointer { get; init; }
    /// <summary>
    ///     Pointer to <c>GetEnv</c> function. Retrieves the <c>JNIEnv</c> pointer for current thread.
    /// </summary>
    internal readonly IntPtr GetEnvPointer { get; init; }
    /// <summary>
    ///     Pointer to <c>AttachCurrentThreadAsDaemon</c> function. Same as AttachCurrentThread, but the
    ///     newly-created <c>java.lang.Thread</c> instance is a daemon.
    /// </summary>
    internal readonly IntPtr AttachCurrentThreadAsDaemonPointer { get; init; }
}