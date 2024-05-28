namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct VirtualMachineInitOptionValue
{
	internal ReadOnlyValPtr<Byte> Name { get; init; }
	internal IntPtr ExtraInfo { get; init; }
}