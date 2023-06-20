namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JFloatArrayLocalRef
{
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
    private JFloatArrayLocalRef(JObjectLocalRef objRef) => this._value = new(objRef);
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="arrayRef"><see cref="JArrayLocalRef"/> value.</param>
    private JFloatArrayLocalRef(JArrayLocalRef arrayRef) => this._value = arrayRef;
}