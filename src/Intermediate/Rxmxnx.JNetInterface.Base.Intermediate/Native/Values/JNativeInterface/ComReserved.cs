namespace Rxmxnx.JNetInterface.Native.Values;

[SuppressMessage(CommonConstants.CodeQuality, CommonConstants.CheckId0051,
                 Justification = CommonConstants.BinaryStructJustification)]
internal readonly partial struct JNativeInterface
{
	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatibility.
	/// </summary>
	[InlineArray(4)]
	private record struct ComReserved
	{
#pragma warning disable CS0169
		private IntPtr _reserved0;
#pragma warning restore CS0169
	}
}