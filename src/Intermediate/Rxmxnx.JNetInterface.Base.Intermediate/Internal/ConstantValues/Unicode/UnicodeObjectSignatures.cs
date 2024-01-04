namespace Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode;

/// <summary>
/// Unicode java objects signatures.
/// </summary>
internal static partial class UnicodeObjectSignatures
{
	/// <inheritdoc cref="ObjectSignatures.ObjectSignaturePrefix"/>
	[DefaultValue(ObjectSignatures.ObjectSignaturePrefix)]
	public static readonly CString ObjectSignaturePrefix;
	/// <inheritdoc cref="ObjectSignatures.ObjectSignatureSuffix"/>
	[DefaultValue(ObjectSignatures.ObjectSignatureSuffix)]
	public static readonly CString ObjectSignatureSuffix;
	/// <inheritdoc cref="ObjectSignatures.ArraySignaturePrefix"/>
	[DefaultValue(ObjectSignatures.ArraySignaturePrefix)]
	public static readonly CString ArraySignaturePrefix;

	/// <inheritdoc cref="ObjectSignatures.ObjectSignature"/>
	[DefaultValue(ObjectSignatures.ObjectSignature)]
	public static readonly CString ObjectSignature;
	/// <inheritdoc cref="ObjectSignatures.ClassObjectSignature"/>
	[DefaultValue(ObjectSignatures.ClassObjectSignature)]
	public static readonly CString ClassObjectSignature;
}