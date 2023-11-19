namespace Rxmxnx.JNetInterface.Native.Values;

[StructLayout(LayoutKind.Sequential)]
internal readonly partial struct JNativeInterface
{
	/// <summary>
	/// public reserved entries.
	/// </summary>
	private readonly ComReserved _reserved;
	/// <inheritdoc cref="JNativeInterface.GetVersionPointer"/>
	private readonly IntPtr _getVersionPointer;
	/// <summary>
	/// Internal inline array containing <c>JNINativeInterface_</c> operation pointers.
	/// </summary>
	private readonly Operations _operations;

	/// <summary>
	/// This struct represent the reserved pointer for Microsoft COM compatibility.
	/// </summary>
	[InlineArray(4)]
	[StructLayout(LayoutKind.Sequential)]
	private record struct ComReserved
	{
		private IntPtr _reserved0;
	}

	/// <summary>
	/// This struct represent the operation pointers in <c>JNINativeInterface_</c>.
	/// </summary>
	[InlineArray(228)]
	[StructLayout(LayoutKind.Sequential)]
	private record struct Operations
	{
		private IntPtr _reserved0;
	}
}