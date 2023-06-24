﻿namespace Rxmxnx.JNetInterface.Native.Identifiers;

/// <summary>
/// JNI handle for fields (<c>fieldID</c>). Represents a native signed integer which serves 
/// as opaque identifier for a declared field in a <c>class</c>.
/// </summary>
/// <remarks>This handle will be valid until the associated <c>class</c> is unloaded.</remarks>
internal readonly partial struct JFieldId : IFixedPointer, INative<JFieldId>
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
    public JFieldId() => this._value = IntPtr.Zero;
}