namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JVirtualMachineInitOptionValue
{
	internal ReadOnlyValPtr<Byte> Name { get; init; }
	internal IntPtr ExtraInfo { get; init; }
}