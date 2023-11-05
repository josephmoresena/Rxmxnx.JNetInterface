namespace Rxmxnx.JNetInterface.Extensions.Intermediate.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
internal sealed record JPrimitiveWrapperTypeMetadata<TWrapper> : JPrimitiveWrapperTypeMetadata
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
{
	private static readonly JClassTypeMetadata classMetadata = IClassType.GetMetadata<TWrapper>();

	public static readonly JPrimitiveWrapperTypeMetadata Instance = new JPrimitiveWrapperTypeMetadata<TWrapper>();

	/// <inheritdoc/>
	public override JPrimitiveTypeMetadata PrimitiveMetadata => TWrapper.PrimitiveMetadata;
	/// <inheritdoc/>
	public override Type Type => JPrimitiveWrapperTypeMetadata<TWrapper>.classMetadata.Type;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private JPrimitiveWrapperTypeMetadata() : base(TWrapper.PrimitiveMetadata.ClassName,
	                                               TWrapper.PrimitiveMetadata.ClassSignature,
	                                               TWrapper.ArraySignature) { }

	/// <inheritdoc/>
	internal override IDataType? CreateInstance(JObject? jObject)
		=> JPrimitiveWrapperTypeMetadata<TWrapper>.classMetadata.CreateInstance(jObject);
}