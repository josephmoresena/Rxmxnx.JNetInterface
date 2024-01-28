namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract record JReferenceTypeMetadata : JDataTypeMetadata, IReflectionMetadata
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
	public virtual IImmutableSet<JInterfaceTypeMetadata> Interfaces => ImmutableHashSet<JInterfaceTypeMetadata>.Empty;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	internal JReferenceTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }
	/// <inheritdoc/>
	internal JReferenceTypeMetadata(CStringSequence information) : base(information) { }

	JFunctionDefinition IReflectionMetadata.CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] metadata)
		=> this.CreateFunctionDefinition(functionName, metadata);
	JFieldDefinition IReflectionMetadata.CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
		=> this.CreateFieldDefinition(fieldName);

	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="localRef"/> using
	/// <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="realClass">Indicates whether <paramref name="jClass"/> is instance real class.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="localRef"/> and <paramref name="jClass"/>.</returns>
	internal abstract JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
		Boolean realClass = false);
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jLocal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jLocal))]
	internal abstract JLocalObject? ParseInstance(JLocalObject? jLocal);
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jGlobal"/> and
	/// <paramref name="env"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jGlobal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jGlobal))]
	internal abstract JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal);

	/// <inheritdoc cref="IReflectiblIReflectionMetadatationDefinition(ReadOnlySpan{Byte}, JArgumentMetadata[])"/>
	internal abstract JFunctionDefinition CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] metadata);
	/// <inheritdoc cref="IReflectiblIReflectionMetadatadDefinition(ReadOnlySpan{Byte})"/>
	internal abstract JFieldDefinition CreateFieldDefinition(ReadOnlySpan<Byte> fieldName);

	/// <summary>
	/// Creates a <see cref="JArrayTypeMetadata"/> from current instance.
	/// </summary>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	internal abstract JArrayTypeMetadata? GetArrayMetadata();

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