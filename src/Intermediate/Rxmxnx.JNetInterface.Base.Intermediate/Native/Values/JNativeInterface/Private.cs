namespace Rxmxnx.JNetInterface.Native.Values;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
internal readonly partial struct JNativeInterface
{
	/// <summary>
	/// internal reserved entries.
	/// </summary>
#pragma warning disable CS0169
	private readonly ComReserved _reserved;
#pragma warning disable CS0169
	/// <inheritdoc cref="JNativeInterface.GetVersionPointer"/>
	private readonly IntPtr _getVersionPointer;
	/// <summary>
	/// Internal inline array containing <c>JNINativeInterface_</c> operation pointers.
	/// </summary>
	private readonly Operations _operations;

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