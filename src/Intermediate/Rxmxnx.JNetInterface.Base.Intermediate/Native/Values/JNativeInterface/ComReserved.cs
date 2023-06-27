namespace Rxmxnx.JNetInterface.Native.Values;

[SuppressMessage(CommonConstants.CodeQuality, CommonConstants.CheckId0051,
                 Justification = CommonConstants.BinaryStructJustification)]
internal readonly partial struct JNativeInterface
{
	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatibility.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly record struct ComReserved
	{
#pragma warning disable CS0169
		private readonly IntPtr _reserved0;
		private readonly IntPtr _reserved1;
		private readonly IntPtr _reserved2;
		private readonly IntPtr _reserved3;
#pragma warning restore CS0169
	}
}