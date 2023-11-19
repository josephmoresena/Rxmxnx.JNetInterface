namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JVirtualMachineArgument
{
	internal Int32 Version { get; init; }
	internal IntPtr Name { get; init; }
	internal JObjectLocalRef Group { get; init; }
}