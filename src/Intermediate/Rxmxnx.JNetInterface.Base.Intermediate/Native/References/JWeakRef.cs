namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI weak global handle for fully-qualified-class objects (<c>jweak</c>). 
/// Represents a native signed integer which serves as opaque identifier for any 
/// object. 
/// </summary>
/// <remarks>This identifier may be valid until it is explicitly unloaded.</remarks>
internal readonly partial struct JWeakRef : INative<JWeakRef>, IWrapper<JObjectLocalRef>, IEquatable<JWeakRef>, IFixedPointer
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JWeak;

    /// <summary>
    /// Internal <see cref="JObjectLocalRef"/> reference.
    /// </summary>
    private readonly JObjectLocalRef _value;

    /// <summary>
    /// JNI value as local reference.
    /// </summary>
    public JObjectLocalRef Value => this._value;
    /// <inheritdoc/>
    public IntPtr Pointer => this._value.Pointer;

    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public JWeakRef() => this._value = default;

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JWeakRef other) => this._value.Equals(other._value);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => obj is JWeakRef other && Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._value.GetHashCode();
    #endregion
}