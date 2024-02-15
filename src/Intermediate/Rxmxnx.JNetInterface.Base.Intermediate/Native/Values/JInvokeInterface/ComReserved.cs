namespace Rxmxnx.JNetInterface.Native.Values;

[SuppressMessage(CommonConstants.CodeQuality, CommonConstants.CheckId0051,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
internal readonly partial struct JInvokeInterface
{
	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatibility.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	[InlineArray(3)]
	private record struct ComReserved
	{
#pragma warning disable CS0169
		private IntPtr _reserved0;
#pragma warning restore CS0169
	}
}