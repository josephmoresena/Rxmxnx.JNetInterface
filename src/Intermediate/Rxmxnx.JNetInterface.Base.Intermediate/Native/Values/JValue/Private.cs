namespace Rxmxnx.JNetInterface.Native.Values;

internal partial struct JValue
{
#pragma warning disable 0169
    /// <summary>
    /// Least significant integer (4 bytes).
    /// </summary>
    private readonly Int32 _lsi;
    /// <summary>
    /// Most significant integer (4 bytes).
    /// </summary>
    private readonly Int32 _msi;
#pragma warning restore 0169
}