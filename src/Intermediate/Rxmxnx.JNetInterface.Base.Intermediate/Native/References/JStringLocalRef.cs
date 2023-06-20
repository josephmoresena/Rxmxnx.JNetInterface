namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for string objects (<c>jstring</c>). Represents a native signed integer 
/// which serves as opaque identifier for an string object (<c>java.lang.String</c>). 
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JStringLocalRef : IFixedPointer, INative<JStringLocalRef>,
	IWrapper<JObjectLocalRef>, IEquatable<JStringLocalRef>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JString;

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
    public Boolean Equals(JStringLocalRef other) => this._value.Equals(other._value);
	#endregion

	#region Overrided Methods
	/// <inheritdoc/>
	public override String ToString() => INative.ToString(this);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => obj is JStringLocalRef other && this.Equals(other);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.GetHashCode();
	#endregion
}