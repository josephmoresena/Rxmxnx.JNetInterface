namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Unmanaged type for <see cref="JVirtualMachineInitOption"/> value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly struct VirtualMachineInitOptionValue(ReadOnlyValPtr<Byte> name, IntPtr extraInfo)
{
	/// <summary>
	/// Pointer to option name.
	/// </summary>
	public readonly ReadOnlyValPtr<Byte> Name = name;
	/// <summary>
	/// Pointer to option extra info.
	/// </summary>
	public readonly IntPtr ExtraInfo = extraInfo;
}