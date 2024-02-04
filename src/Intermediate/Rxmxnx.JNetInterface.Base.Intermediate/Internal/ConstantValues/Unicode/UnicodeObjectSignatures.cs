namespace Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode;

/// <summary>
/// Unicode java objects signatures.
/// </summary>
internal static partial class UnicodeObjectSignatures
{
	/// <inheritdoc cref="ObjectSignatures.ObjectSignaturePrefix"/>
	public const Byte ObjectSignaturePrefixChar = (Byte)ObjectSignatures.ObjectSignaturePrefixChar;
	/// <inheritdoc cref="ObjectSignatures.ObjectSignatureSuffix"/>
	public const Byte ObjectSignatureSuffixChar = (Byte)ObjectSignatures.ObjectSignatureSuffixChar;
	/// <inheritdoc cref="ObjectSignatures.ArraySignaturePrefix"/>
	public const Byte ArraySignaturePrefixChar = (Byte)ObjectSignatures.ArraySignaturePrefixChar;

	/// <inheritdoc cref="ObjectSignatures.ObjectSignature"/>
	[DefaultValue(ObjectSignatures.ObjectSignature)]
	public static readonly CString ObjectSignature;
	/// <inheritdoc cref="ObjectSignatures.ClassObjectSignature"/>
	[DefaultValue(ObjectSignatures.ClassObjectSignature)]
	public static readonly CString ClassObjectSignature;
}