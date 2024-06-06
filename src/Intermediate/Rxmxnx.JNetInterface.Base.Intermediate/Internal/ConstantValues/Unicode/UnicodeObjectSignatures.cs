namespace Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode;

/// <summary>
/// Unicode java objects signatures.
/// </summary>
internal static partial class UnicodeObjectSignatures
{
	/// <inheritdoc cref="ObjectSignatures.ObjectSignaturePrefix"/>
	public const Byte ObjectSignaturePrefixChar = (Byte)'L';
	/// <inheritdoc cref="ObjectSignatures.ObjectSignatureSuffix"/>
	public const Byte ObjectSignatureSuffixChar = (Byte)';';
	/// <summary>
	/// Prefix for both array declaration in JNI signature for methods and properties and
	/// for JNI name of array classes.
	/// </summary>
	public const Byte ArraySignaturePrefixChar = (Byte)'[';

	/// <inheritdoc cref="ObjectSignatures.ObjectSignature"/>
	[DefaultValue(ObjectSignatures.ObjectSignature)]
	public static readonly CString ObjectSignature;
	/// <inheritdoc cref="ObjectSignatures.ClassObjectSignature"/>
	[DefaultValue(ObjectSignatures.ClassObjectSignature)]
	public static readonly CString ClassObjectSignature;
}