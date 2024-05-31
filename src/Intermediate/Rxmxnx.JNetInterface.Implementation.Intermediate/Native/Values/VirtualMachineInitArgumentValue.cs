namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Unmanaged type for <see cref="JVirtualMachineInitArg"/> value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe partial struct VirtualMachineInitArgumentValue : INativeType<VirtualMachineInitArgumentValue>
{
	static JNativeType INativeType.Type => JNativeType.JVirtualMachineInitArgument;
	String INativeType.TextValue
		=> $"Version: 0x{this.Version:x8} Options: {this.OptionsLength} Ignore Unrecognized: {this.IgnoreUnrecognized.Value}";

	/// <summary>
	/// JNI version.
	/// </summary>
	public Int32 Version { get; init; }
	/// <summary>
	/// Length of options values.
	/// </summary>
	public Int32 OptionsLength { get; init; }
	/// <summary>
	/// Pointer to options values.
	/// </summary>
	public VirtualMachineInitOptionValue* Options { get; init; }
	/// <summary>
	/// Indicates whether initialization ignores any unrecognized option.
	/// </summary>
	public JBoolean IgnoreUnrecognized { get; init; }
}