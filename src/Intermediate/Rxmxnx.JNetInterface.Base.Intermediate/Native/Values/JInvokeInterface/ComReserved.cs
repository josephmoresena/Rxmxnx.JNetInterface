namespace Rxmxnx.JNetInterface.Native.Values;

[SuppressMessage(CommonConstants.CodeQuality, CommonConstants.CheckId0051,
                 Justification = CommonConstants.BinaryStructJustification)]
internal readonly partial struct JInvokeInterface
{
	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatiblity.
	/// </summary>
	private readonly struct ComReserved
	{
#pragma warning disable CS0169
		private readonly IntPtr _reserved0;
		private readonly IntPtr _reserved1;
		private readonly IntPtr _reserved2;
#pragma warning restore CS0169
	}
}