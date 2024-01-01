namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
internal sealed record JPrimitiveWrapperTypeMetadata<TWrapper> : JPrimitiveWrapperTypeMetadata
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
{
	/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
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
		TWrapper.PrimitiveMetadata.WrapperInformation)
	{
		this._baseMetadata = baseMetadata ?? IClassType.GetMetadata<JLocalObject>();
		this._baseTypes = [this._baseMetadata.Type,];
	}

	/// <inheritdoc/>
	internal override TWrapper? ParseInstance(JLocalObject? jLocal) => jLocal as TWrapper ?? TWrapper.Create(jLocal);
	/// <inheritdoc/>
	internal override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TWrapper>();
}