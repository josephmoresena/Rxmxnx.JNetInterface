namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a Java Primitive Object to create a <see cref="JGlobalBase"/> instance.
/// </summary>
public abstract record PrimitiveWrapperObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// Primitive signature.
	/// </summary>
	// ReSharper disable once UnusedAutoPropertyAccessor.Global
	public Byte PrimitiveSignature { get; }

	/// <inheritdoc/>
	private protected PrimitiveWrapperObjectMetadata(Byte signature, ObjectMetadata metadata) : base(metadata)
		=> this.PrimitiveSignature = signature;

	/// <summary>
	/// Retrieves the value of the current primitive as a <typeparamref name="T"/> value.
	/// </summary>
	/// <typeparam name="T">A <see cref="IPrimitiveType{T}"/> type.</typeparam>
	/// <returns>A <see cref="IPrimitiveType{T}"/> value.</returns>
	public abstract T? GetValue<T>() where T : unmanaged, IPrimitiveType<T>, IEqualityOperators<T, T, Boolean>;
}