namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JVirtualMachineInitOption
{
	internal IntPtr Name { get; init; }
	internal IntPtr ExtraInfo { get; init; }
}