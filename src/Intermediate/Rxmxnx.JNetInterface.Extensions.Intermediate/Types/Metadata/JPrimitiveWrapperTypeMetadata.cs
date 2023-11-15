namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
internal sealed record
	JPrimitiveWrapperTypeMetadata<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TWrapper> :
		JPrimitiveWrapperTypeMetadata
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
{
	/// <inheritdoc cref="JDataTypeMetadata.BaseMetadata"/>
	private readonly JClassTypeMetadata _baseMetadata;
	/// <inheritdoc cref="JDataTypeMetadata.BaseTypes"/>
	private readonly HashSet<Type> _baseTypes;

	/// <inheritdoc/>
	public override JPrimitiveTypeMetadata PrimitiveMetadata => TWrapper.PrimitiveMetadata;
	/// <inheritdoc/>
	public override Type Type => typeof(TWrapper);
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;
	/// <inheritdoc/>
	public override IImmutableSet<JInterfaceTypeMetadata> Interfaces => JPrimitiveWrapperConstants.Interfaces;
	/// <inheritdoc/>
	public override JClassTypeMetadata BaseMetadata => this._baseMetadata;
	/// <inheritdoc/>
	public override IReadOnlySet<Type> BaseTypes => this._baseTypes;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="baseMetadata">Base <see cref="JClassTypeMetadata"/> instance.</param>
	public JPrimitiveWrapperTypeMetadata(JClassTypeMetadata? baseMetadata = default) : base(
		TWrapper.PrimitiveMetadata.ClassName, TWrapper.PrimitiveMetadata.ClassSignature, TWrapper.ArraySignature)
	{
		this._baseMetadata = baseMetadata ?? IClassType.GetMetadata<JLocalObject>();
		this._baseTypes = new(1) { this._baseMetadata.Type, };
	}
	/// <inheritdoc/>
	internal override IDataType? ParseInstance(JObject? jObject) => TWrapper.Create(jObject);
}