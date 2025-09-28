namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// This struct represent the reserved pointer for Call and CallV functions.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
#endif
internal struct MethodOffset
{
#pragma warning disable CS0169
	private IntPtr _reserved0;
	private IntPtr _reserved1;
#pragma warning restore CS0169
}