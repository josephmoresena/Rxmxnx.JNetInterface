namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
/// <typeparam name="TWrapper">Type of java primitive wrapper class datatype.</typeparam>
public sealed class
	JPrimitiveWrapperTypeMetadata<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TWrapper> : JClassTypeMetadata<TWrapper>
	.
	View where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
{
	/// <summary>
	/// Primitive metadata.
	/// </summary>
	public JPrimitiveTypeMetadata PrimitiveMetadata => TWrapper.PrimitiveMetadata;
	/// <summary>
	/// Primitive argument metadata.
	/// </summary>
	public JArgumentMetadata? PrimitiveArgumentMetadata
		=> this.PrimitiveMetadata.Type != typeof(void) ? this.PrimitiveMetadata.ArgumentMetadata : default;

	/// <inheritdoc/>
	internal JPrimitiveWrapperTypeMetadata(JClassTypeMetadata<TWrapper> metadata) : base(metadata) { }

	/// <inheritdoc/>
	private protected override ClassProperty GetPrimaryProperty()
		=> new()
		{
			PropertyName = nameof(JPrimitiveWrapperTypeMetadata<TWrapper>.PrimitiveMetadata),
			Value = ClassNameHelper.GetClassName(this.PrimitiveMetadata.Signature),
		};
}