namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jbyteArray</c>). Represents a native signed integer 
/// which serves as opaque identifier for a primitive array object (<c>byte[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JByteArrayLocalRef : IFixedPointer, INative<JByteArrayLocalRef>, IWrapper<JObjectLocalRef>,
	IEquatable<JByteArrayLocalRef>, IEquatable<JArrayLocalRef>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JByteArray;

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
    public Boolean Equals(JByteArrayLocalRef other) => this._value.Equals(other._value);
	/// <inheritdoc/>
	public Boolean Equals(JArrayLocalRef other) => this._value.Equals(other);
	#endregion

	#region Overrided Methods
	/// <inheritdoc/>
	public override String ToString() => INative.ToString(this);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => obj is JByteArrayLocalRef other && this.Equals(other);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.GetHashCode();
	#endregion
}