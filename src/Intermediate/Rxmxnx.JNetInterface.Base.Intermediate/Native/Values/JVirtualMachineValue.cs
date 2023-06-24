namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JavaVM</c> struct. Contains a pointer to a <c>JNIInvokeInterface_</c> object.
/// </summary>
public readonly partial struct JVirtualMachineValue : IFixedPointer, INative<JVirtualMachineValue>,
    IReadOnlyReferenceable<JInvokeInterface>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JVirtualMachine;

    /// <summary>
    /// Internal <see cref="JInvokeInterface"/> pointer.
    /// </summary>
    private readonly IntPtr _functions;

    /// <summary>
    /// <see langword="readonly ref"/> <see cref="JInvokeInterface"/> from this value.
    /// </summary>
    public ref readonly JInvokeInterface Reference => ref this._functions.GetUnsafeReadOnlyReference<JInvokeInterface>();
    /// <inheritdoc/>
    public IntPtr Pointer => this._functions;
}