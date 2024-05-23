namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// This struct represent the reserved pointer for Call and CallV functions.
/// </summary>
[InlineArray(2)]
[StructLayout(LayoutKind.Sequential)]
internal record struct MethodOffset
{
	private IntPtr _reserved0;
}