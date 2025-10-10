namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stores the metadata of a <see cref="IPrimitiveWrapperType{TPrimitive}"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
internal sealed record PrimitiveWrapperObjectMetadata<TPrimitive> : PrimitiveWrapperObjectMetadata
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>
{
	/// <summary>
	/// Internal value.
	/// </summary>
	public TPrimitive? Value { get; init; }

	/// <inheritdoc/>
	internal PrimitiveWrapperObjectMetadata(ObjectMetadata metadata) : base(
		IDataType.GetMetadata<TPrimitive>().Signature[0], metadata)
	{
		if (metadata is PrimitiveWrapperObjectMetadata<TPrimitive> wrapperMetadata)
			this.Value = wrapperMetadata.Value;
	}

	public override T? GetValue<T>() => this.Value.HasValue ? T.CreateFrom(this.Value.Value) : null;
}