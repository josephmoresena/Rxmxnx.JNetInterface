namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Unmanaged type for <see cref="ThreadCreationArgs"/> value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
public readonly unsafe struct VirtualMachineArgumentValue(Int32 version, Byte* name, JGlobalRef group)
#else
internal readonly unsafe struct VirtualMachineArgumentValue(Int32 version, Byte* name, JGlobalRef group)
#endif
{
	/// <summary>
	/// JNI Version.
	/// </summary>
	public readonly Int32 Version = version;
	/// <summary>
	/// Pointer to thread name.
	/// </summary>
	public readonly Byte* Name = name;
	/// <summary>
	/// Global reference to thread group.
	/// </summary>
	public readonly JGlobalRef Group = group;

#if !PACKAGE
	/// <summary>
	/// Safe name pointer for tests purposes.
	/// </summary>
	internal IntPtr NamePtr => (IntPtr)this.Name;
#endif
}