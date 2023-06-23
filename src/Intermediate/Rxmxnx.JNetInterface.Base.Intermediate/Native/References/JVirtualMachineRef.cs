namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JavaVM</c> pointer. Represents a pointer to a <c>JavaVM</c> object.
/// </summary>
/// <remarks>
/// This identifier will be valid until the library is unloaded or the JVM instance is destroyed.
/// </remarks>
public readonly partial struct JVirtualMachineRef : IFixedPointer, INative<JVirtualMachineRef>,
    IReadOnlyReferenceable<JVirtualMachineValue>, IEquatable<JVirtualMachineRef>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JVirtualMachineRef;

#pragma warning disable CS0649
    /// <summary>
    /// Internal pointer value.
    /// </summary>
    private readonly IntPtr _value;
#pragma warning restore CS0649

    /// <summary>
    /// <see langword="readonly ref"/> <see cref="JVirtualMachineValue"/> from this pointer.
    /// </summary>
    public ref readonly JVirtualMachineValue Reference => ref this._value.GetUnsafeReadOnlyReference<JVirtualMachineValue>();
    /// <inheritdoc/>
    public IntPtr Pointer => this._value;

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JVirtualMachineRef other) => this._value.Equals(other._value);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => obj is JVirtualMachineRef other && this.Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._value.GetHashCode();
    #endregion
}