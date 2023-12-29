namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract record JReferenceTypeMetadata : JDataTypeMetadata
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

	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jLocal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jLocal))]
	internal abstract JLocalObject? ParseInstance(JLocalObject? jLocal);

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