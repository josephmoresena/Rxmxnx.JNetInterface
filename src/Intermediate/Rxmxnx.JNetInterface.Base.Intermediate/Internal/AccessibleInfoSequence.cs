namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal type information sequence.
/// </summary>
internal sealed partial class AccessibleInfoSequence : InfoSequenceBase
{
	/// <summary>
	/// Accessible member descriptor.
	/// </summary>
	public CString Descriptor { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="hash">Accessible hash.</param>
	/// <param name="nameLength">Name length.</param>
	/// <param name="descriptorLength">Descriptor length.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public AccessibleInfoSequence(String hash, Int32 nameLength, Int32 descriptorLength) : base(hash, nameLength)
		=> this.Descriptor = CString.Create<ItemState>(new(this.Hash, descriptorLength, nameLength + 1));

	/// <summary>
	/// Creates a <see cref="AccessibleInfoSequence"/> for a field definition.
	/// </summary>
	/// <param name="name">Field name.</param>
	/// <param name="returnType">Return type signature.</param>
	/// <returns>A <see cref="AccessibleInfoSequence"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe AccessibleInfoSequence CreateFieldInfo(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> returnType)
	{
		Int32 bufferLength = name.Length + returnType.Length + 2;
		fixed (Byte* nameChr0 = &MemoryMarshal.GetReference(name))
		fixed (Byte* returnTypeChr0 = &MemoryMarshal.GetReference(returnType))
		{
			Int32 stringLength = bufferLength / sizeof(Char) + bufferLength % sizeof(Char);
			FieldSpanState state = new(nameChr0, name.Length, returnTypeChr0, returnType.Length);
			String hash = String.Create(stringLength, state, AccessibleInfoSequence.WriteFieldBuffer);
			return new(hash, name.Length, returnType.Length);
		}
	}
	/// <summary>
	/// Creates a <see cref="AccessibleInfoSequence"/> for a call definition.
	/// </summary>
	/// <param name="name">Call name.</param>
	/// <param name="returnType">Return type signature.</param>
	/// <param name="args">List of call arguments.</param>
	/// <param name="callSize">Total size in bytes of call parameters.</param>
	/// <param name="sizes">Arguments sizes.</param>
	/// <param name="referenceCount">Reference counts.</param>
	/// <returns>A <see cref="AccessibleInfoSequence"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe AccessibleInfoSequence CreateCallInfo(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> returnType,
		ReadOnlySpan<JArgumentMetadata> args, out Int32 callSize, out Int32[] sizes, out Int32 referenceCount)
	{
		Int32 descriptorLength = returnType.Length +
			AccessibleInfoSequence.GetArgumentLength(args, out callSize, out referenceCount, out sizes) + 2;
		Int32 bufferLength = name.Length + descriptorLength + 2;
		fixed (Byte* nameChr0 = &MemoryMarshal.GetReference(name))
		fixed (Byte* returnTypeChr0 = &MemoryMarshal.GetReference(returnType))
#pragma warning disable CS8500
		fixed (void* argsPtr = &MemoryMarshal.GetReference(args))
#pragma warning restore CS8500
		{
			Int32 stringLength = bufferLength / sizeof(Char) + bufferLength % sizeof(Char);
			CallSpanState state = new(nameChr0, name.Length, returnTypeChr0, returnType.Length, argsPtr, args.Length);
			String hash = String.Create(stringLength, state, AccessibleInfoSequence.WriteCallBuffer);
			return new(hash, name.Length, descriptorLength);
		}
	}
}