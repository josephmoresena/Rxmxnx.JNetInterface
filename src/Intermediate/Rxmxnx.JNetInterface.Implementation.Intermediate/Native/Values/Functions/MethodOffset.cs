namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// This struct represent the reserved pointer for Call and CallV functions.
/// </summary>
[InlineArray(2)]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
internal struct MethodOffset
{
#pragma warning disable CS0169
	private IntPtr _reserved0;
#pragma warning restore CS0169
}