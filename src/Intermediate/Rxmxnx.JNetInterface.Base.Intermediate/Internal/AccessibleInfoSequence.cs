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

	/// <inheritdoc/>
	public override String ToString() => this.Hash;

	/// <summary>
	/// Creates a <see cref="AccessibleInfoSequence"/> for a field definition.
	/// </summary>
	/// <param name="name">Field name.</param>
	/// <param name="returnType">Return type signature.</param>
	/// <returns>A <see cref="AccessibleInfoSequence"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe AccessibleInfoSequence CreateFieldInfo(ReadOnlySpan<Byte> name, JArgumentMetadata returnType)
	{
		Int32 signatureLength = returnType.Signature.Length;
		Int32 bufferLength = name.Length + signatureLength + 2;
		fixed (Byte* nameChr0 = &MemoryMarshal.GetReference(name))
		fixed (Byte* returnTypeChr0 = &MemoryMarshal.GetReference(returnType.Signature.AsSpan()))
		{
			Int32 stringLength = bufferLength / sizeof(Char) + bufferLength % sizeof(Char);
			FieldSpanState state = new(nameChr0, name.Length, returnTypeChr0, signatureLength);
			String hash = String.Create(stringLength, state, AccessibleInfoSequence.WriteFieldBuffer);
			return new(hash, name.Length, signatureLength);
		}
	}
	/// <summary>
	/// Creates a <see cref="AccessibleInfoSequence"/> for a call definition.
	/// </summary>
	/// <param name="name">Method name.</param>
	/// <param name="returnType">Return type name.</param>
	/// <param name="args"></param>
	/// <returns>A <see cref="AccessibleInfoSequence"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe AccessibleInfoSequence CreateCallInfo(ReadOnlySpan<Byte> name, JArgumentMetadata returnType,
		ReadOnlySpan<JArgumentMetadata> args)
	{
		Int32 signatureLength = returnType.Signature.Length;
		Int32 paramsLength = 2 + AccessibleInfoSequence.GetArgumentLength(args);
		Int32 bufferLength = name.Length + signatureLength + paramsLength + 2;
		fixed (Byte* nameChr0 = &MemoryMarshal.GetReference(name))
		fixed (Byte* returnTypeChr0 = &MemoryMarshal.GetReference(returnType.Signature.AsSpan()))
#pragma warning disable CS8500
		fixed (void* argsPtr = &MemoryMarshal.GetReference(args))
#pragma warning restore CS8500
		{
			Int32 stringLength = bufferLength / sizeof(Char) + bufferLength % sizeof(Char);
			CallSpanState state = new(nameChr0, name.Length, returnTypeChr0, returnType.Signature.Length, argsPtr,
			                          paramsLength);
			String hash = String.Create(stringLength, state, AccessibleInfoSequence.WriteCallBuffer);
			return new(hash, name.Length, signatureLength);
		}
	}
}