namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Unmanaged type for <see cref="ThreadCreationArgs"/> value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct VirtualMachineArgumentValue(Int32 version, Byte* name, JGlobalRef group)
{
	/// <summary>
	/// JNI Version.
	/// </summary>
	public Int32 Version { get; init; } = version;
	/// <summary>
	/// Pointer to thread name.
	/// </summary>
	public Byte* Name { get; init; } = name;
	/// <summary>
	/// Global reference to thread group.
	/// </summary>
	public JGlobalRef Group { get; init; } = group;
}