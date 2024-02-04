namespace Rxmxnx.JNetInterface.Native.Values;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct JNativeMethodValue
{
	public static readonly Int32 Size = NativeUtilities.SizeOf<JNativeMethodValue>();

	internal ReadOnlyValPtr<Byte> Name { get; init; }
	internal ReadOnlyValPtr<Byte> Signature { get; init; }
	internal IntPtr Pointer { get; init; }

	public static JNativeMethodValue Create(JNativeCallEntry entry, ICollection<MemoryHandle> handles)
	{
		handles.Add(entry.Hash.AsMemory().Pin());
		return new()
		{
			Name = (ReadOnlyValPtr<Byte>)entry.Name.AsSpan().GetUnsafeIntPtr(),
			Signature = (ReadOnlyValPtr<Byte>)entry.Signature.AsSpan().GetUnsafeIntPtr(),
			Pointer = entry.Pointer,
		};
	}
}