namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jbooleanArray</c>). Represents a native signed integer 
/// which serves as opaque identifier for a primitive array object (<c>boolean[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JBooleanArrayLocalRef : IFixedPointer, INative<JBooleanArrayLocalRef>, IWrapper<JObjectLocalRef>,
    IEquatable<JBooleanArrayLocalRef>, IEquatable<JArrayLocalRef>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JBooleanArray;

    /// <summary>
    /// Internal <see cref="JArrayLocalRef"/> reference.
    /// </summary>
    private readonly JArrayLocalRef _value;

    /// <summary>
    /// JNI local reference.
    /// </summary>
    public JObjectLocalRef Value => this._value.Value;
    /// <summary>
    /// JNI array local reference.
    /// </summary>
    public JArrayLocalRef ArrayValue => this._value;
    /// <inheritdoc/>
    public IntPtr Pointer => this._value.Value.Pointer;

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JBooleanArrayLocalRef other) => this._value.Equals(other._value);
    /// <inheritdoc/>
    public Boolean Equals(JArrayLocalRef other) => this._value.Equals(other);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => obj is JBooleanArrayLocalRef other && this.Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._value.GetHashCode();
    #endregion
}