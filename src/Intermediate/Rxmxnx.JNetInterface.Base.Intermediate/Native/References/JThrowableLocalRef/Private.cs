namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JThrowableLocalRef
{
    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
    private JThrowableLocalRef(JObjectLocalRef objRef) => this._value = objRef;
}