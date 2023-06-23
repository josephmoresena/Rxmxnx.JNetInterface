namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jarray</c>). Represents a native signed integer 
/// which serves as opaque identifier for an array object (<c>[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JArrayLocalRef : IFixedPointer, INative<JBooleanArrayLocalRef>,
    IWrapper<JObjectLocalRef>, IEquatable<JArrayLocalRef>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JArray;

    /// <summary>
    /// Internal <see cref="JObjectLocalRef"/> reference.
    /// </summary>
    private readonly JObjectLocalRef _value;

    /// <summary>
    /// JNI local reference.
    /// </summary>
    public JObjectLocalRef Value => this._value;
    /// <inheritdoc/>
    public IntPtr Pointer => this._value.Pointer;

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JArrayLocalRef other) => this._value.Equals(other._value);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => obj is JArrayLocalRef other && this.Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._value.GetHashCode();
    #endregion
}