namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract partial class JClassTypeMetadata : JReferenceTypeMetadata
{
	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Class;

	/// <inheritdoc/>
	private protected JClassTypeMetadata(ReadOnlySpan<Byte> className) : base(className) { }
	/// <inheritdoc/>
	private protected JClassTypeMetadata(TypeInfoSequence information) : base(information) { }

	/// <summary>
	/// Base type property.
	/// </summary>
	private protected ClassProperty? GetBaseTypeProperty()
		=> this.BaseMetadata is not null ?
			new()
			{
				PropertyName = nameof(JReferenceTypeMetadata.BaseMetadata),
				Value = ITypeInformation.GetJavaClassName(this.BaseMetadata),
			} :
			default;
	/// <summary>
	/// Additional property.
	/// </summary>
	private protected virtual IAppendableProperty? GetPrimaryProperty() => default;
	/// <summary>
	/// Additional property.
	/// </summary>
	private protected virtual IAppendableProperty? GetSecondaryProperty() => default;

	/// <inheritdoc/>
	public override String? ToString()
		=> IVirtualMachine.TypeMetadataToStringEnabled ?
			MetadataTextUtilities.GetString(this, this.InterfaceProperties, this.GetBaseTypeProperty(),
			                                this.GetPrimaryProperty(), this.GetSecondaryProperty()) :
			base.ToString();
}

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract partial class JClassTypeMetadata<
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass> : JClassTypeMetadata
	where TClass : JLocalObject, IClassType<TClass>
{
	/// <inheritdoc/>
	public override Type Type => typeof(TClass);
	/// <inheritdoc/>
	public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TClass>();

	/// <inheritdoc/>
	private protected JClassTypeMetadata(ReadOnlySpan<Byte> className) : base(className) { }
	/// <inheritdoc/>
	private protected JClassTypeMetadata(TypeInfoSequence information) : base(information) { }

	/// <inheritdoc/>
	public override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TClass>();

	/// <inheritdoc/>
	internal override Boolean IsInstance(JReferenceObject jObject) => jObject is TClass || jObject.InstanceOf<TClass>();
	/// <inheritdoc/>
	internal override JFunctionDefinition<TClass> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		ReadOnlySpan<JArgumentMetadata> paramsMetadata)
		=> JFunctionDefinition<TClass>.Create(functionName, paramsMetadata);
	/// <inheritdoc/>
	internal override JFieldDefinition<TClass> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName) => new(fieldName);
}