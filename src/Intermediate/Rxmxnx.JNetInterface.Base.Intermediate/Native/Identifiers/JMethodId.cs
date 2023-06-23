﻿namespace Rxmxnx.JNetInterface.Native.Identifiers;

/// <summary>
/// JNI handle for methods (<c>methodID</c>). Represents a native signed integer which serves 
/// as opaque identifier for a declared method in a <c>class</c>.
/// </summary>
/// <remarks>This handle will be valid until the associated <c>class</c> is unloaded.</remarks>
internal readonly struct JMethodId : IFixedPointer, INative<JMethodId>, IEquatable<JMethodId>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JMethod;

    /// <summary>
    /// Internal native signed integer
    /// </summary>
    private readonly IntPtr _value;

    /// <inheritdoc/>
    public IntPtr Pointer => this._value;

    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public JMethodId() => this._value = IntPtr.Zero;

    #region Public Methods
    /// <inheritdoc/>
    public Boolean Equals(JMethodId other) => this._value.Equals(other._value);
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    /// <inheritdoc/>
    public override Boolean Equals([NotNullWhen(true)] Object? obj) => obj is JMethodId other && this.Equals(other);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._value.GetHashCode();
    #endregion

    #region Operators
    /// <inheritdoc/>
    public static Boolean operator ==(JMethodId left, JMethodId right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JMethodId left, JMethodId right) => !left.Equals(right);
    #endregion
}