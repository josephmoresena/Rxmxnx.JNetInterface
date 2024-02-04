namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial record JCallDefinition
{
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Call defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JCallDefinition(ReadOnlySpan<Byte> name, params JArgumentMetadata[] metadata) : this(
		name, stackalloc Byte[1] { UnicodePrimitiveSignatures.VoidSignatureChar, }, metadata) { }
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Call defined name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JCallDefinition(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> returnType,
		params JArgumentMetadata[] metadata) : base(new CStringSequence(
			                                            name,
			                                            JCallDefinition.CreateDescriptor(
				                                            returnType, out Int32 size, out Int32[] sizes,
				                                            out Int32 referenceCount, metadata)))
	{
		this._callSize = size;
		this._sizes = sizes;
		this._referenceCount = referenceCount;
		this._useJValue = this._sizes.Length > 1 &&
			Math.Abs(this._sizes.Length * JValue.Size - this._callSize) <= 0.15 * this._callSize;
	}
}