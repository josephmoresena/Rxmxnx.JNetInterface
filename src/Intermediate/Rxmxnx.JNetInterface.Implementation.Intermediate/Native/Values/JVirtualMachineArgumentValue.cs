namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JVirtualMachineArgumentValue
{
	public Int32 Version { get; init; }
	public ReadOnlyValPtr<Byte> Name { get; init; }
	public JGlobalRef Group { get; init; }
}