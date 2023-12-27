namespace Rxmxnx.JNetInterface.Native.Values;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct JNativeMethodValue
{
	public static readonly Int32 Size = NativeUtilities.SizeOf<JNativeMethodValue>();
	
	internal ReadOnlyValPtr<Byte> Name { get; init; }
	internal ReadOnlyValPtr<Byte> Signature { get; init; }
	internal IntPtr Pointer { get; init; }

	public static JNativeMethodValue Create(JNativeCall call, ICollection<MemoryHandle> handles)
	{
		handles.Add(call.Hash.AsMemory().Pin());
		return new()
		{
			Name = (ReadOnlyValPtr<Byte>)call.Name.AsSpan().GetUnsafeIntPtr(),
			Signature = (ReadOnlyValPtr<Byte>)call.Signature.AsSpan().GetUnsafeIntPtr(),
			Pointer = call.Pointer,
		};
	}
}