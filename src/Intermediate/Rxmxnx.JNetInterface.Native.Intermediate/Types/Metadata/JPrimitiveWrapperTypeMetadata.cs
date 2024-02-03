namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IDataType"/> type.
/// </summary>
public abstract record JPrimitiveWrapperTypeMetadata : JClassTypeMetadata
{
	/// <summary>
	/// Primitive metadata.
	/// </summary>
	public abstract JPrimitiveTypeMetadata PrimitiveMetadata { get; }

	/// <summary>
	/// Primitive class name.
	/// </summary>
	public CString PrimitiveClassName => this.PrimitiveMetadata.ClassName;
	/// <summary>
	/// Primitive argument metadata.
	/// </summary>
	public JArgumentMetadata PrimitiveArgumentMetadata => this.PrimitiveMetadata.ArgumentMetadata;

	/// <inheritdoc/>
	internal JPrimitiveWrapperTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }
	/// <inheritdoc/>
	internal JPrimitiveWrapperTypeMetadata(CStringSequence information) : base(information) { }

	/// <inheritdoc/>
	public override String ToString()
		=> $"{base.ToString()}{nameof(JPrimitiveWrapperTypeMetadata.PrimitiveClassName)} = {this.PrimitiveClassName}, " +
			$"{nameof(JPrimitiveWrapperTypeMetadata.PrimitiveArgumentMetadata)} = {this.PrimitiveArgumentMetadata}, ";
}