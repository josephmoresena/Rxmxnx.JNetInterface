namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JVirtualMachineArgumentValue
{
	public Int32 Version { get; init; }
	public IntPtr Name { get; init; }
	public JGlobalRef Group { get; init; }
}