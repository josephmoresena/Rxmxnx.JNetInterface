namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for throwable objects (<c>jthrowable</c>). Represents a native signed integer 
/// which serves as opaque identifier for an throwable object (<c>java.lang.Throwable</c>). 
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JThrowableLocalRef : IFixedPointer, INative<JThrowableLocalRef>,
    IWrapper<JObjectLocalRef> 
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

    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
    private JThrowableLocalRef(JObjectLocalRef objRef) => this._value = objRef;

    /// <inheritdoc/>
    public override Boolean Equals(Object? obj) => JObjectLocalRef.ObjectEquals(this, obj);
}