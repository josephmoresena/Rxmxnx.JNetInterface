namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JDoubleArrayLocalRef
{
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
    private JDoubleArrayLocalRef(JObjectLocalRef objRef) => this._value = new(objRef);
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="arrayRef"><see cref="JArrayLocalRef"/> value.</param>
    private JDoubleArrayLocalRef(JArrayLocalRef arrayRef) => this._value = arrayRef;
}