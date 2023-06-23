namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for fully-qualified-class objects (<c>jobject</c>). 
/// Represents a native signed integer which serves as opaque identifier for any 
/// object. 
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JObjectLocalRef : IFixedPointer, INative<JObjectLocalRef>, IEquatable<JObjectLocalRef>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JObject;

    /// <summary>
    /// Internal native signed integer
    /// </summary>
    private readonly IntPtr _value;

    /// <inheritdoc/>
    public IntPtr Pointer => this._value;

    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public JObjectLocalRef() => this._value = IntPtr.Zero;

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JObjectLocalRef other) => this._value.Equals(other._value);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => obj is JObjectLocalRef other && this.Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._value.GetHashCode();
    #endregion
}