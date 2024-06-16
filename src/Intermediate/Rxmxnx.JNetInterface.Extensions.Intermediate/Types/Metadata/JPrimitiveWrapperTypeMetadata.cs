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
	/// Primitive class name.
	/// </summary>
	public CString PrimitiveClassName => this.PrimitiveMetadata.ClassName;
	/// <summary>
	/// Primitive argument metadata.
	/// </summary>
	public JArgumentMetadata PrimitiveArgumentMetadata => this.PrimitiveMetadata.ArgumentMetadata;

	/// <inheritdoc/>
	internal JPrimitiveWrapperTypeMetadata(JClassTypeMetadata<TWrapper> metadata) : base(metadata) { }

	/// <inheritdoc/>
	private protected override ClassProperty GetPrimaryProperty()
		=> new()
		{
			PropertyName = nameof(JPrimitiveWrapperTypeMetadata<TWrapper>.PrimitiveClassName),
			Value = $"{this.PrimitiveClassName}",
		};
	/// <inheritdoc/>
	private protected override IAppendableProperty? GetSecondaryProperty()
		=> this.PrimitiveMetadata.Type != typeof(void) ?
			this.PrimitiveArgumentMetadata.ToAppendableProperty(
				nameof(JPrimitiveWrapperTypeMetadata<TWrapper>.PrimitiveArgumentMetadata)) :
			default;
}