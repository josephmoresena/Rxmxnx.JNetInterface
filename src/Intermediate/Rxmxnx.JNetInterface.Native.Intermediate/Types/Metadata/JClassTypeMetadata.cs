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
	private protected JClassTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }
	/// <inheritdoc/>
	private protected JClassTypeMetadata(CStringSequence information) : base(information) { }

	/// <inheritdoc/>
	public override String ToString()
		=> $"{base.ToString()}{nameof(JDataTypeMetadata.Modifier)} = {this.Modifier}, " +
			$"{nameof(JClassTypeMetadata.BaseClassName)} = {this.BaseClassName}, ";
}

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract partial record JClassTypeMetadata<TClass> : JClassTypeMetadata
	where TClass : JReferenceObject, IClassType<TClass>
{
	/// <inheritdoc/>
	public override Type Type => typeof(TClass);
	/// <inheritdoc/>
	public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TClass>();

	/// <inheritdoc/>
	private protected JClassTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }
	/// <inheritdoc/>
	private protected JClassTypeMetadata(CStringSequence information) : base(information) { }

	/// <inheritdoc/>
	public override String ToString() => base.ToString();

	/// <inheritdoc/>
	internal override JFunctionDefinition<TClass> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] metadata)
		=> JFunctionDefinition<TClass>.Create(functionName, metadata);
	/// <inheritdoc/>
	internal override JFieldDefinition<TClass> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName) => new(fieldName);
	/// <inheritdoc/>
	internal override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TClass>();
}