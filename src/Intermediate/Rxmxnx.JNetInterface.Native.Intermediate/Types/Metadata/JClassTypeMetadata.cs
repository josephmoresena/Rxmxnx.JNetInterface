namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract record JClassTypeMetadata : JReferenceTypeMetadata
{
	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Class;

	/// <summary>
	/// Base class name.
	/// </summary>
	public CString? BaseClassName => this.BaseMetadata?.ClassName;

	/// <inheritdoc/>
	internal JClassTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) :
		base(className, signature) { }
	/// <inheritdoc/>
	internal JClassTypeMetadata(CStringSequence information) : base(information) { }

	/// <inheritdoc/>
	public override String ToString()
		=> $"{base.ToString()}{nameof(JDataTypeMetadata.Modifier)} = {this.Modifier}, " +
			$"{nameof(JClassTypeMetadata.BaseClassName)} = {this.BaseClassName}, ";
}