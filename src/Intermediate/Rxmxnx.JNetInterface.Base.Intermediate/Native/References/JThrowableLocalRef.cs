﻿namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for throwable objects (<c>jthrowable</c>). Represents a native signed integer 
/// which serves as opaque identifier for an throwable object (<c>java.lang.Throwable</c>). 
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JThrowableLocalRef : IFixedPointer, INative<JThrowableLocalRef>,
	IWrapper<JObjectLocalRef>, IEquatable<JThrowableLocalRef>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JThrowable;

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
    public Boolean Equals(JThrowableLocalRef other) => this._value.Equals(other._value);
	#endregion

	#region Overrided Methods
	/// <inheritdoc/>
	public override String ToString() => INative.ToString(this);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => obj is JThrowableLocalRef other && Equals(other);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.GetHashCode();
	#endregion
}