namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JVirtualMachineInitArgument
{
	internal Int32 Version { get; init; }
	internal Int32 OptionsLenght { get; init; }
	internal IntPtr Options { get; init; }
	internal Boolean IgnoreUnrecognized { get; init; }
}