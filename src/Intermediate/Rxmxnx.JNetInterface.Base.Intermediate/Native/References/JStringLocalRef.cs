namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for string objects (<c>jstring</c>). Represents a native signed integer 
/// which serves as opaque identifier for an string object (<c>java.lang.String</c>). 
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JStringLocalRef : IFixedPointer, INative<JStringLocalRef>,
    IWrapper<JObjectLocalRef>
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
}