namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract partial record JReferenceTypeMetadata : JDataTypeMetadata, IReflectionMetadata
{
	/// <inheritdoc/>
	public override Int32 SizeOf => NativeUtilities.PointerSize;

	/// <summary>
	/// Base type of current type metadata.
	/// </summary>
	public virtual JClassTypeMetadata? BaseMetadata => default;
	/// <summary>
	/// Set of interfaces metadata of current type implements.
	/// </summary>
	public abstract IReadOnlySet<JInterfaceTypeMetadata> Interfaces { get; }

	JFunctionDefinition IReflectionMetadata.CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] metadata)
		=> this.CreateFunctionDefinition(functionName, metadata);
	JFieldDefinition IReflectionMetadata.CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
		=> this.CreateFieldDefinition(fieldName);

	/// <summary>
	/// Indicates whether <paramref name="jObject"/> is instance of current type.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jObject"/> is instance of current type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public abstract Boolean InstanceOf(JReferenceObject? jObject);

	/// <inheritdoc/>
	public override String ToString()
		=> $"{base.ToString()}{nameof(JReferenceTypeMetadata.Interfaces)} = {this.Interfaces}, ";

	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> for <typeparamref name="TReference"/>
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	protected static JArrayTypeMetadata GetArrayMetadata<TReference>()
		where TReference : JReferenceObject, IReferenceType<TReference>
		=> IArrayType.GetMetadata<JArrayObject<TReference>>();

	/// <summary>
	/// Retrieves the metadata of an array whose elements are of the type that
	/// <paramref name="metadata"/> represents.
	/// </summary>
	/// <param name="metadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	public static JArrayTypeMetadata? GetArrayMetadata(JReferenceTypeMetadata metadata) => metadata.GetArrayMetadata();
}