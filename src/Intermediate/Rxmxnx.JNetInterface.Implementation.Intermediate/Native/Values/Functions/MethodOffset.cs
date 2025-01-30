namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// This struct represent the reserved pointer for Call and CallV functions.
/// </summary>
[InlineArray(2)]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
internal struct MethodOffset
{
	/// <summary>
	/// Indicates whether generic JNI calls should be done with managed function pointers.
	/// </summary>
	public static readonly Boolean UseManagedGenericPointers =
		RuntimeInformation.ProcessArchitecture is Architecture.Arm;

#pragma warning disable CS0169
	private IntPtr _reserved0;
#pragma warning restore CS0169
}