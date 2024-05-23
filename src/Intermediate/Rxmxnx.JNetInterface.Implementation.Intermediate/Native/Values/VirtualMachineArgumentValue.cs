namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct VirtualMachineArgumentValue
{
	public Int32 Version { get; init; }
	public ReadOnlyValPtr<Byte> Name { get; init; }
	public JGlobalRef Group { get; init; }
}