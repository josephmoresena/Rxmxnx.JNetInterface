namespace Rxmxnx.JNetInterface.Types.Metadata;

public abstract partial record JReferenceTypeMetadata
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	internal JReferenceTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }
	/// <inheritdoc/>
	internal JReferenceTypeMetadata(CStringSequence information) : base(information) { }
}