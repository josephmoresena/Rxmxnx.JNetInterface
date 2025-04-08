namespace Rxmxnx.JNetInterface.Native.Values;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
#endif
internal readonly partial struct JInvokeInterface
{
	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatibility.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	[InlineArray(3)]
	internal struct ComReserved
	{
#pragma warning disable IDE0051
#pragma warning disable CS0169
		private IntPtr _reserved0;
#pragma warning restore CS0169
#pragma warning restore IDE0051
	}
}