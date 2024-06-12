namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class JCallDefinition
{
	/// <summary>
	/// Total size in bytes of call parameters.
	/// </summary>
	private readonly Int32 _callSize;
	/// <summary>
	/// Count of reference parameters.
	/// </summary>
	private readonly Int32 _referenceCount;
	/// <summary>
	/// Call argument's size.
	/// </summary>
	private readonly Int32[] _sizes;

	/// <summary>
	/// Creates the method descriptor using <paramref name="returnSignature"/> and <paramref name="metadata"/>.
	/// </summary>
	/// <param name="returnSignature">Method return type signature.</param>
	/// <param name="totalSize">Total size in bytes of call parameters.</param>
	/// <param name="sizes">Arguments sizes.</param>
	/// <param name="referenceCount">Reference counts.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>Method descriptor.</returns>
	private static CString CreateDescriptor(ReadOnlySpan<Byte> returnSignature, out Int32 totalSize, out Int32[] sizes,
		out Int32 referenceCount, params JArgumentMetadata[] metadata)
	{
		referenceCount = 0;
		totalSize = 0;
		sizes = new Int32[metadata.Length];

		using MemoryStream memory = new();
		memory.WriteByte(CommonNames.MethodParameterPrefixChar);
		for (Int32 i = 0; i < metadata.Length; i++)
		{
			ReadOnlySpan<Byte> signature = metadata[i].Signature.AsSpan();
			memory.Write(signature);
			totalSize += metadata[i].Size;
			sizes[i] = metadata[i].Size;
			if (signature.Length > 1) referenceCount++;
		}
		memory.WriteByte(CommonNames.MethodParameterSuffixChar);
		memory.Write(returnSignature);
		memory.WriteByte(default);
		return memory.ToArray();
	}
}