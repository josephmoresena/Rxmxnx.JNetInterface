namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This record stores the metadata for a parameter <see cref="IDataType"/> type.
/// </summary>
public sealed record JArgumentMetadata
{
	/// <summary>
	/// Method argument type signature.
	/// </summary>
	public CString Signature { get; private init; }
	/// <summary>
	/// Method argument type size in bytes.
	/// </summary>
	public Int32 Size { get; private init; }

	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="signature">Method argument type signature.</param>
	/// <param name="size">Method argument type size in bytes.</param>
	private JArgumentMetadata(CString signature, Int32 size)
	{
		this.Signature = signature;
		this.Size = size;
	}

	/// <summary>
	/// Creates a <see cref="JArgumentMetadata"/> from <paramref name="signature"/> value.
	/// </summary>
	/// <param name="signature">The signature of type.</param>
	/// <returns>A <see cref="JArgumentMetadata"/> from <paramref name="signature"/> value</returns>
	public static JArgumentMetadata Create(CString signature)
	{
		ValidationUtilities.ThrowIfInvalidSignature(signature, false);
		return new(signature, NativeUtilities.PointerSize);
	}
	/// <summary>
	/// Creates a <see cref="JArgumentMetadata"/> from <paramref name="signature"/> value.
	/// </summary>
	/// <param name="signature">The signature of type.</param>
	/// <returns>A <see cref="JArgumentMetadata"/> from <paramref name="signature"/> value</returns>
	public static JArgumentMetadata Create(ReadOnlySpan<Byte> signature)
	{
		ValidationUtilities.ThrowIfInvalidSignature(signature, false);
		return new(CString.Create(signature), NativeUtilities.PointerSize);
	}
	/// <summary>
	/// Creates a <see cref="JArgumentMetadata"/> from <typeparamref name="TArg"/> type.
	/// </summary>
	/// <typeparam name="TArg"><see cref="IDataType"/> type.</typeparam>
	/// <returns>A <see cref="JArgumentMetadata"/> from <typeparamref name="TArg"/> type</returns>
	public static JArgumentMetadata Create<TArg>() where TArg : IDataType<TArg>
		=> new(IDataType.GetMetadata<TArg>().Signature, IDataType.GetMetadata<TArg>().SizeOf);
}