﻿namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This record stores the metadata for a parameter <see cref="IDataType"/> type.
/// </summary>
public sealed class JArgumentMetadata
{
	/// <summary>
	/// Type signature.
	/// </summary>
	public CString Signature { get; private init; }
	/// <summary>
	/// Type size in bytes.
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

	/// <inheritdoc/>
	public override String? ToString()
#if !PACKAGE
		=> MetadataTextUtilities.TypeMetadataToStringEnabled ?
#else
		=> IVirtualMachine.TypeMetadataToStringEnabled ?
#endif
			MetadataTextUtilities.GetString(this) :
			base.ToString();

	/// <summary>
	/// Retrieves the <see cref="JArgumentMetadata"/> for <typeparamref name="TArg"/> type.
	/// </summary>
	/// <typeparam name="TArg"><see cref="IDataType"/> type.</typeparam>
	/// <returns>A <see cref="JArgumentMetadata"/> from <typeparamref name="TArg"/> type</returns>
	public static JArgumentMetadata Get<TArg>() where TArg : IDataType<TArg> => TArg.Argument;
	/// <summary>
	/// Creates a <see cref="JArgumentMetadata"/> from <paramref name="signature"/> value.
	/// </summary>
	/// <param name="signature">The signature of type.</param>
	/// <returns>A <see cref="JArgumentMetadata"/> from <paramref name="signature"/> value</returns>
	public static JArgumentMetadata Create(ReadOnlySpan<Byte> signature)
	{
		CommonValidationUtilities.ThrowIfInvalidObjectSignature(signature);
		return new(CString.Create(signature), IntPtr.Size);
	}

	/// <summary>
	/// Creates a <see cref="JArgumentMetadata"/> from <typeparamref name="TArg"/> type.
	/// </summary>
	/// <typeparam name="TArg"><see cref="IDataType"/> type.</typeparam>
	/// <returns>A <see cref="JArgumentMetadata"/> from <typeparamref name="TArg"/> type</returns>
	internal static JArgumentMetadata Create<TArg>() where TArg : IDataType<TArg>
	{
		JDataTypeMetadata typeMetadata = IDataType.GetMetadata<TArg>();
		return new(typeMetadata.Signature, typeMetadata.SizeOf);
	}
}