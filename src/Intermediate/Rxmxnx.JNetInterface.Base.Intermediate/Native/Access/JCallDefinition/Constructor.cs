namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class JCallDefinition
{
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Call defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	private protected JCallDefinition(ReadOnlySpan<Byte> name, ReadOnlySpan<JArgumentMetadata> metadata = default) :
		this(name, [CommonNames.VoidSignatureChar,], metadata) { }
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Call defined name.</param>
	/// <param name="returnTypeSignature">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private protected JCallDefinition(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> returnTypeSignature,
		ReadOnlySpan<JArgumentMetadata> metadata) : base(AccessibleInfoSequence.CreateCallInfo(
			                                                 name, returnTypeSignature, metadata, out Int32 callSize,
			                                                 out Int32[] sizes, out Int32 referenceCount))
	{
		this._callSize = callSize;
		this._sizes = sizes;
		this._referenceCount = referenceCount;
	}
	/// <inheritdoc/>
	private protected JCallDefinition(JCallDefinition definition) : base(definition)
	{
		this._callSize = definition._callSize;
		this._sizes = definition._sizes;
		this._referenceCount = definition._referenceCount;
	}
}