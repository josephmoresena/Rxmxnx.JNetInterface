namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an exception <see cref="IThrowableType{TThrowable}"/> type.
/// </summary>
/// <typeparam name="TThrowable">Type of java enum type.</typeparam>
public sealed record JThrowableTypeMetadata<TThrowable> : JClassTypeMetadata<TThrowable>.View
	where TThrowable : JThrowableObject, IThrowableType<TThrowable>
{
	/// <inheritdoc/>
	internal JThrowableTypeMetadata(JClassTypeMetadata<TThrowable> metadata) : base(metadata) { }

	/// <inheritdoc/>
	public override String ToString()
		=> $"{nameof(JThrowableTypeMetadata<TThrowable>)} {{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";

	/// <inheritdoc/>
	internal override JThrowableException CreateException(JGlobalBase jGlobalThrowable,
		String? exceptionMessage = default)
		=> new JThrowableException<TThrowable>(jGlobalThrowable, exceptionMessage);
}