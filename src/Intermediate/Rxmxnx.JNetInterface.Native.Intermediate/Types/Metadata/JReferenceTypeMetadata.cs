namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract record JReferenceTypeMetadata : JDataTypeMetadata
{
	/// <inheritdoc cref="JReferenceTypeMetadata.Invalid"/>
	private JLocalObject? _invalid;

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
	/// Internal invalid instance.
	/// </summary>
	internal JLocalObject Invalid => this._invalid ??= this.ParseInstance(JLocalObject.InvalidObject);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JReferenceTypeMetadata(CString className, CString? signature, CString? arraySignature = default) : base(
		className, signature ?? JDataTypeMetadata.ComputeReferenceTypeSignature(className), arraySignature) { }

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
	internal abstract JArrayTypeMetadata GetArrayMetadata();

	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> for <typeparamref name="TReference"/>
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	protected static JArrayTypeMetadata GetArrayMetadata<TReference>()
		where TReference : JReferenceObject, IReferenceType<TReference>
		=> IArrayType.GetMetadata<JArrayObject<TReference>>();
}