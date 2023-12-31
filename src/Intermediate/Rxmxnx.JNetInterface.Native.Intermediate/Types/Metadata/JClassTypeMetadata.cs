namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract record JClassTypeMetadata : JReferenceTypeMetadata
{
	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Class;

	/// <inheritdoc/>
	internal JClassTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) :
		base(className, signature) { }
	/// <inheritdoc/>
	internal JClassTypeMetadata(CStringSequence information) : base(information) { }
}