namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JObjectArrayLocalRef
{
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
    private JObjectArrayLocalRef(JObjectLocalRef objRef) => this._value = new(objRef);
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="arrayRef"><see cref="JArrayLocalRef"/> value.</param>
    private JObjectArrayLocalRef(JArrayLocalRef arrayRef) => this._value = arrayRef;
}