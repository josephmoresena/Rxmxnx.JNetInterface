namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Unmanaged type for <c>JavaVMOption</c> value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly struct VirtualMachineInitOptionValue(ReadOnlyValPtr<Byte> optionString, IntPtr extraInfo = default)
{
	/// <summary>
	/// Pointer to option name.
	/// </summary>
	public readonly ReadOnlyValPtr<Byte> OptionString = optionString;
	/// <summary>
	/// Pointer to option extra info.
	/// </summary>
	public readonly IntPtr ExtraInfo = extraInfo;
}