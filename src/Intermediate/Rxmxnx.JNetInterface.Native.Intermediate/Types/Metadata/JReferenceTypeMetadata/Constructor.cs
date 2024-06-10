namespace Rxmxnx.JNetInterface.Types.Metadata;

public abstract partial class JReferenceTypeMetadata
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name of the current type.</param>
	/// <param name="signature">JNI signature for the current type.</param>
	private protected JReferenceTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }
	/// <inheritdoc/>
	private protected JReferenceTypeMetadata(CStringSequence information) : base(information) { }
}