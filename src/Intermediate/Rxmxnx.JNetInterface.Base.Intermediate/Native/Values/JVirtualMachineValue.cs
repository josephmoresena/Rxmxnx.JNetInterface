namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JavaVM</c> struct. Contains a pointer to a <c>JNIInvokeInterface_</c> object.
/// </summary>
public readonly partial struct JVirtualMachineValue : IFixedPointer, INative<JVirtualMachineValue>,
    IReadOnlyReferenceable<JInvokeInterface>, IEquatable<JVirtualMachineValue>
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

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JVirtualMachineValue other) => this._functions.Equals(other._functions);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => obj is JVirtualMachineValue other && this.Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._functions.GetHashCode();
    #endregion
}