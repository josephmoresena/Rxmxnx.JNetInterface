namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly partial struct JNativeInterface
{
	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatibility.
	/// </summary>
	[InlineArray(4)]
	[StructLayout(LayoutKind.Sequential)]
	internal record struct ComReserved
	{
		private IntPtr _reserved0;
	}
}