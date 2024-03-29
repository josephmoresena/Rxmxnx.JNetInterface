namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public abstract partial record JDataTypeMetadata : ITypeInformation
{
	/// <summary>
	/// Array signature for current type.
	/// </summary>
	public CString ArraySignature => this._arraySignature;

	/// <summary>
	/// Metadata argument for current type.
	/// </summary>
	public abstract JArgumentMetadata ArgumentMetadata { get; }
	/// <summary>
	/// CLR type of <see cref="IDataType"/>.
	/// </summary>
	public abstract Type Type { get; }
	/// <summary>
	/// Kind of current type.
	/// </summary>
	public abstract JTypeKind Kind { get; }
	/// <summary>
	/// Modifier of current type.
	/// </summary>
	public abstract JTypeModifier Modifier { get; }
	/// <summary>
	/// Size of current type in bytes.
	/// </summary>
	public abstract Int32 SizeOf { get; }

	/// <inheritdoc/>
	public CString ClassName => this._className;
	/// <inheritdoc/>
	public CString Signature => this._signature;
	/// <inheritdoc/>
	public String Hash => this._sequence.ToString();

	/// <inheritdoc/>
	public override String ToString()
		=> $"{nameof(JDataTypeMetadata.ClassName)} = {this.ClassName}, " +
			$"{nameof(JDataTypeMetadata.Type)} = {this.Type}, " + $"{nameof(JDataTypeMetadata.Kind)} = {this.Kind}, " +
			$"{nameof(JDataTypeMetadata.ArgumentMetadata)} = {this.ArgumentMetadata.ToSimplifiedString()}, " +
			$"{nameof(JDataTypeMetadata.ArraySignature)} = {this.ArraySignature}, ";
}