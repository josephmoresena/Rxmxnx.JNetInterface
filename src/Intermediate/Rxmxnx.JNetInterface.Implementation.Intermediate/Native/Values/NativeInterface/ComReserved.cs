namespace Rxmxnx.JNetInterface.Native.Values;

internal partial struct NativeInterface
{
	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatibility.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal readonly struct ComReserved
	{
#pragma warning disable CS0169
		private readonly IntPtr _reserved0;
		private readonly IntPtr _reserved1;
		private readonly IntPtr _reserved2;
		private readonly IntPtr _reserved3;
#pragma warning restore CS0169
	}
}