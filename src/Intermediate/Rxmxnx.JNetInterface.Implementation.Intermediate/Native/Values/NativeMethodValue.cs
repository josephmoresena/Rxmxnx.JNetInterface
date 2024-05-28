namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Native representation of <see cref="JNativeCallEntry"/> instance
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2376,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct NativeMethodValue
{
	/// <summary>
	/// Size if <see cref="NativeMethodValue"/>
	/// </summary>
	public static readonly Int32 Size = sizeof(NativeMethodValue);

	/// <summary>
	/// Pointer to method name.
	/// </summary>
#pragma warning disable CS0169
	private readonly Byte* _name;
	/// <summary>
	/// Pointer to method signature.
	/// </summary>
	private readonly Byte* _signature;
	/// <summary>
	/// Pointer to method implementation.
	/// </summary>
	private readonly void* _function;
#pragma warning restore CS0169

	/// <summary>
	/// Pointer to method name.
	/// </summary>
	private Byte* Name
	{
		init => this._name = value;
	}
	/// <summary>
	/// Pointer to method signature.
	/// </summary>
	private Byte* Signature
	{
		init => this._signature = value;
	}
	/// <summary>
	/// Pointer to method implementation.
	/// </summary>
	private void* Pointer
	{
		init => this._function = value;
	}

	/// <summary>
	/// Creates a <see cref="NativeMethodValue"/> from <paramref name="entry"/>.
	/// </summary>
	/// <param name="entry">A <see cref="JNativeCallEntry"/> instance.</param>
	/// <param name="handles">A <see cref="MemoryHandle"/> collection to append <paramref name="entry"/> pin.</param>
	/// <returns>A <see cref="NativeMethodValue"/> value.</returns>
	public static NativeMethodValue Create(JNativeCallEntry entry, ICollection<MemoryHandle> handles)
	{
		handles.Add(entry.Hash.AsMemory().Pin());
		return new()
		{
			Name = (Byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(entry.Name.AsSpan())),
			Signature = (Byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(entry.Descriptor.AsSpan())),
			Pointer = entry.Pointer.ToPointer(),
		};
	}
}