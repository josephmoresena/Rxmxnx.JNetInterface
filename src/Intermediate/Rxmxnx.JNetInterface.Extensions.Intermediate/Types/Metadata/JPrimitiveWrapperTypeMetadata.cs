namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
/// <typeparam name="TWrapper">Type of java primitive wrapper class datatype.</typeparam>
public sealed record JPrimitiveWrapperTypeMetadata<TWrapper> : JClassTypeMetadata<TWrapper>.View
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
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
	public override String ToString()
		=> $"{nameof(JDataTypeMetadata)} {{ {base.ToString()}{nameof(JPrimitiveWrapperTypeMetadata<TWrapper>.PrimitiveClassName)} = {this.PrimitiveClassName}, " +
			$"{nameof(JPrimitiveWrapperTypeMetadata<TWrapper>.PrimitiveArgumentMetadata)} = {this.PrimitiveArgumentMetadata.ToSimplifiedString()}, {nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";
}