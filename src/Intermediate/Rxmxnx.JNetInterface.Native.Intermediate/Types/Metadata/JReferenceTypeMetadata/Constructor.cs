namespace Rxmxnx.JNetInterface.Types.Metadata;

public abstract partial class JReferenceTypeMetadata
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name of the current type.</param>
	private protected JReferenceTypeMetadata(ReadOnlySpan<Byte> className) : base(className) { }
	/// <inheritdoc/>
	private protected JReferenceTypeMetadata(TypeInfoSequence information) : base(information) { }
}