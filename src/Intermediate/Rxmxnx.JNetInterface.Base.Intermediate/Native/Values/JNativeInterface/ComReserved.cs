namespace Rxmxnx.JNetInterface.Native.Values;

[SuppressMessage(CommonConstants.CodeQuality, CommonConstants.CheckId0051, Justification = CommonConstants.BinaryStructJustification)]
public readonly partial struct JNativeInterface
{
    /// <summary>
    /// This struct represent the reserved pointer for Microsoft COM compatiblity.
    /// </summary>
    private readonly struct ComReserved
    {
        private readonly IntPtr _reserved0;
        private readonly IntPtr _reserved1;
        private readonly IntPtr _reserved2;
        private readonly IntPtr _reserved3;
    }
}