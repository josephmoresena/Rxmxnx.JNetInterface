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
	public abstract IInterfaceSet Interfaces { get; }

	JFunctionDefinition IReflectionMetadata.CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] metadata)
		=> this.CreateFunctionDefinition(functionName, metadata);
	JFieldDefinition IReflectionMetadata.CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
		=> this.CreateFieldDefinition(fieldName);

	/// <summary>
	/// Indicates whether <paramref name="jObject"/> is an instance of current type.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jObject"/> is an instance of current type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean IsInstance(IEnvironment env, JReferenceObject? jObject)
		=> jObject is null || this.IsInstance(jObject) ||
			env.ClassFeature.IsInstanceOf(jObject, env.ClassFeature.GetClass(this.Hash));

	/// <summary>
	/// Indicates whether an instance of current type is instance of the type of
	/// <paramref name="otherMetadata"/>.
	/// </summary>
	/// <param name="otherMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if an instance of current type is instance of
	/// the type of <paramref name="otherMetadata"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public virtual Boolean TypeOf(JReferenceTypeMetadata otherMetadata)
		=> JReferenceTypeMetadata.TypeOf(this, otherMetadata);

	/// <summary>
	/// Indicates whether <paramref name="jObject"/> is instance of current type.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jObject"/> is instance of current type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal abstract Boolean IsInstance(JReferenceObject jObject);

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

	/// <summary>
	/// Indicates whether an instance of type <paramref name="metadata"/> is instance of
	/// the type of <paramref name="otherMetadata"/>.
	/// </summary>
	/// <param name="metadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="otherMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if an instance of type <paramref name="metadata"/> is instance of
	/// the type of <paramref name="otherMetadata"/>; otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean TypeOf(JReferenceTypeMetadata metadata, JReferenceTypeMetadata otherMetadata)
	{
		if (otherMetadata is JInterfaceTypeMetadata interfaceMetadata)
			return metadata.Interfaces.Contains(interfaceMetadata);
		while (metadata.BaseMetadata is not null)
		{
			if (otherMetadata.Equals(metadata.BaseMetadata))
				return true;
			metadata = metadata.BaseMetadata;
		}
		return otherMetadata.ClassName.AsSpan().SequenceEqual(UnicodeClassNames.Object);
	}
}