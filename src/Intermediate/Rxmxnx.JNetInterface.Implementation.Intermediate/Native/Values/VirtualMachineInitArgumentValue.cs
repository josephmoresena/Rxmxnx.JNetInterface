namespace Rxmxnx.JNetInterface.Native.Values;

[StructLayout(LayoutKind.Sequential)]
internal readonly partial struct VirtualMachineInitArgumentValue : INativeType<VirtualMachineInitArgumentValue>
{
	static JNativeType INativeType.Type => JNativeType.JVirtualMachineInitArgument;
	String INativeType.TextValue
		=> $"Version: 0x{this.Version:x8} Options: {this.OptionsLength} Ignore Unrecognized: {this.IgnoreUnrecognized == JBoolean.TrueValue}";

	internal Int32 Version { get; init; }
	internal Int32 OptionsLength { get; init; }
	internal ValPtr<VirtualMachineInitOptionValue> Options { get; init; }
	internal Byte IgnoreUnrecognized { get; init; }
}