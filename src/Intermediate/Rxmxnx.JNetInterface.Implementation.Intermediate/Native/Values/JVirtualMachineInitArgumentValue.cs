namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JVirtualMachineInitArgumentValue
{
	internal Int32 Version { get; init; }
	internal Int32 OptionsLenght { get; init; }
	internal IntPtr Options { get; init; }
	internal Byte IgnoreUnrecognized { get; init; }
}