namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an exception <see cref="IClassType"/> type.
/// </summary>
public abstract record JThrowableTypeMetadata : JClassTypeMetadata
{
	/// <inheritdoc />
	internal JThrowableTypeMetadata(CString className, CString? signature, CString? arraySignature) : base(
		className, signature, arraySignature)
	{
	}

	/// <summary>
	/// Creates an exception instance from a <see cref="JGlobalBase"/> throwable instance.
	/// </summary>
	/// <param name="jGlobalThrowable">A <see cref="JGlobalBase"/> throwable instance.</param>
	/// <returns>A <see cref="JThrowableException"/> instance.</returns>
	internal abstract JThrowableException CreateException(JGlobalBase jGlobalThrowable);
}