namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an exception <see cref="IClassType"/> type.
/// </summary>
public abstract record JThrowableTypeMetadata : JClassTypeMetadata
{
	/// <inheritdoc/>
	internal JThrowableTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }

	/// <summary>
	/// Creates an exception instance from a <see cref="JGlobalBase"/> throwable instance.
	/// </summary>
	/// <param name="jGlobalThrowable">A <see cref="JGlobalBase"/> throwable instance.</param>
	/// <param name="exceptionMessage">Exception message.</param>
	/// <returns>A <see cref="JThrowableException"/> instance.</returns>
	internal abstract JThrowableException CreateException(JGlobalBase jGlobalThrowable,
		String? exceptionMessage = default);
}