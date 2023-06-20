namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JNIEnv</c> pointer. Represents a pointer to a <c>JNIEnv</c> object.
/// </summary>
/// <remarks>This references is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JEnvironmentRef : IFixedPointer, INative<JEnvironmentRef>,
    IReadOnlyReferenceable<JEnvironmentValue>, IEquatable<JEnvironmentRef>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JEnvironmentRef;

#pragma warning disable CS0649
    /// <summary>
    /// Internal pointer value.
    /// </summary>
    private readonly IntPtr _value;
#pragma warning restore CS0649

    /// <inheritdoc/>
    public IntPtr Pointer => this._value;
    /// <summary>
    /// <see langword="readonly ref"/> <see cref="JEnvironmentValue"/> from this pointer.
    /// </summary>
    public ref readonly JEnvironmentValue Reference => ref this._value.GetUnsafeReadOnlyReference<JEnvironmentValue>();

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JEnvironmentRef other) => this._value.Equals(other._value);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => obj is JEnvironmentRef other && this.Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._value.GetHashCode();
    #endregion
}