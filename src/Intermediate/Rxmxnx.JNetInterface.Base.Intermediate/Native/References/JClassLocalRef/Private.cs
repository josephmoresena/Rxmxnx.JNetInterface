namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JClassLocalRef
{
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
    private JClassLocalRef(JObjectLocalRef objRef) => this._value = objRef;
}