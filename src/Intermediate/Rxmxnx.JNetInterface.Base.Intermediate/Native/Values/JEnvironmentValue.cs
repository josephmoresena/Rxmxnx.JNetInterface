namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNIEnv</c> struct. Contains a pointer to a <c>JNINativeInterface_</c> object.
/// </summary>
public readonly partial struct JEnvironmentValue : IFixedPointer, INative<JEnvironmentValue>,
    IReadOnlyReferenceable<JNativeInterface>, IEquatable<JEnvironmentValue>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JNativeInterface;

    /// <summary>
    /// Internal <see cref="JNativeInterface"/> pointer.
    /// </summary>
    private readonly IntPtr _functions;

    /// <summary>
    /// <see langword="readonly ref"/> <see cref="JNativeInterface"/> from this value.
    /// </summary>
    public ref readonly JNativeInterface Reference => ref this._functions.GetUnsafeReadOnlyReference<JNativeInterface>();
    /// <inheritdoc/>
    public IntPtr Pointer => this._functions;

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JEnvironmentValue other) => this._functions.Equals(other._functions);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => obj is JEnvironmentValue other && this.Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._functions.GetHashCode();
    #endregion
}