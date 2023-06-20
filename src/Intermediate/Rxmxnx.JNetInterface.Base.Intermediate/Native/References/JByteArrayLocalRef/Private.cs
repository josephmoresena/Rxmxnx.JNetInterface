namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JByteArrayLocalRef
{
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
    private JByteArrayLocalRef(JObjectLocalRef objRef) => this._value = new(objRef);
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="arrayRef"><see cref="JArrayLocalRef"/> value.</param>
    private JByteArrayLocalRef(JArrayLocalRef arrayRef) => this._value = arrayRef;
}