namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an exception <see cref="IThrowableType{TThrowable}"/> type.
/// </summary>
/// <typeparam name="TThrowable">Type of java enum type.</typeparam>
public sealed class
	JThrowableTypeMetadata<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable> : JClassTypeMetadata<
		TThrowable>
	.View where TThrowable : JThrowableObject, IThrowableType<TThrowable>
{
	/// <inheritdoc/>
	internal JThrowableTypeMetadata(JClassTypeMetadata<TThrowable> metadata) : base(metadata) { }

	/// <inheritdoc/>
	internal override ThrowableException CreateException(JGlobalBase jGlobalThrowable,
		String? exceptionMessage = default)
		=> new ThrowableException<TThrowable>(jGlobalThrowable, exceptionMessage);
}