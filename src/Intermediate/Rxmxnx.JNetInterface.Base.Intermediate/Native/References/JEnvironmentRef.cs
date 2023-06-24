namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JNIEnv</c> pointer. Represents a pointer to a <c>JNIEnv</c> object.
/// </summary>
/// <remarks>This references is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JEnvironmentRef : IFixedPointer, INative<JEnvironmentRef>,
    IReadOnlyReferenceable<JEnvironmentValue>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JEnvironmentRef;

    /// <summary>
    /// Internal pointer value.
    /// </summary>
    private readonly IntPtr _value;

    /// <inheritdoc/>
    public IntPtr Pointer => this._value;
    /// <summary>
    /// <see langword="readonly ref"/> <see cref="JEnvironmentValue"/> from this pointer.
    /// </summary>
    public ref readonly JEnvironmentValue Reference => ref this._value.GetUnsafeReadOnlyReference<JEnvironmentValue>();
    
    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public JEnvironmentRef() => this._value = IntPtr.Zero;
    
    /// <inheritdoc/>
    public override Int32 GetHashCode() => HashCode.Combine(this._value);
    /// <inheritdoc/>
    public override Boolean Equals([NotNullWhen(true)] Object? obj)
        => obj is JEnvironmentRef jEnvRef && this._value.Equals(jEnvRef._value);
}