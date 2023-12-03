namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JNativeMethodValue
{
	internal ReadOnlyValPtr<Byte> Name { get; init; }
	internal ReadOnlyValPtr<Byte> Signature { get; init; }
	internal IntPtr Pointer { get; init; }
}