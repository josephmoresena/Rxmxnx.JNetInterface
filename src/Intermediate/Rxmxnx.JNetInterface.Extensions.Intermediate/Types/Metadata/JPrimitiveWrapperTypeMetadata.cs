namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
internal sealed record JPrimitiveWrapperTypeMetadata<TWrapper> : JPrimitiveWrapperTypeMetadata
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
{
	/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
	private readonly JClassTypeMetadata _baseMetadata;

	/// <inheritdoc/>
	public override JPrimitiveTypeMetadata PrimitiveMetadata => TWrapper.PrimitiveMetadata;
	/// <inheritdoc/>
	public override Type Type => typeof(TWrapper);
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;
	/// <inheritdoc/>
	public override IImmutableSet<JInterfaceTypeMetadata> Interfaces
		=> this.PrimitiveMetadata.SizeOf != 0 ?
			JPrimitiveWrapperConstants.Interfaces :
			ImmutableHashSet<JInterfaceTypeMetadata>.Empty;
	/// <inheritdoc/>
	public override JClassTypeMetadata BaseMetadata => this._baseMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="baseMetadata">Base <see cref="JClassTypeMetadata"/> instance.</param>
	public JPrimitiveWrapperTypeMetadata(JClassTypeMetadata? baseMetadata = default) : base(
		TWrapper.PrimitiveMetadata.WrapperInformation)
		=> this._baseMetadata = baseMetadata ?? IClassType.GetMetadata<JLocalObject>();

	/// <inheritdoc/>
	internal override TWrapper? ParseInstance(JLocalObject? jLocal)
	{
		switch (jLocal)
		{
			case null:
				return default;
			case TWrapper result:
				return result;
			default:
				JLocalObject.Validate<TWrapper>(jLocal);
				return TWrapper.Create(jLocal);
		}
	}
	/// <inheritdoc/>
	internal override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TWrapper>();
}