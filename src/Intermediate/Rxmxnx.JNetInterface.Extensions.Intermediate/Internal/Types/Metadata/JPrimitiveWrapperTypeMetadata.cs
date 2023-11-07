namespace Rxmxnx.JNetInterface.Internal.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
internal sealed record
	JPrimitiveWrapperTypeMetadata<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TWrapper> :
		JPrimitiveWrapperTypeMetadata
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>,
	IInterfaceImplementation<TWrapper, JSerializableObject>, IInterfaceImplementation<TWrapper, JComparableObject>
{
	/// <summary>
	/// Instance.
	/// </summary>
	public static readonly JPrimitiveWrapperTypeMetadata Instance = new JPrimitiveWrapperTypeMetadata<TWrapper>();

	/// <inheritdoc/>
	public override JPrimitiveTypeMetadata PrimitiveMetadata => TWrapper.PrimitiveMetadata;
	/// <inheritdoc/>
	public override Type Type => typeof(TWrapper);
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;
	/// <inheritdoc/>
	public override IImmutableSet<JInterfaceTypeMetadata> Interfaces => JPrimitiveWrapperConstants.Interfaces;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private JPrimitiveWrapperTypeMetadata() : base(TWrapper.PrimitiveMetadata.ClassName,
	                                               TWrapper.PrimitiveMetadata.ClassSignature,
	                                               TWrapper.ArraySignature) { }

	/// <inheritdoc/>
	internal override IDataType? CreateInstance(JObject? jObject) => TWrapper.Create(jObject);
}