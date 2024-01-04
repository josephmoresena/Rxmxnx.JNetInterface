namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java objects names.
/// </summary>
internal static class ObjectSignatures
{
	/// <inheritdoc cref="ObjectSignatures.ObjectSignaturePrefix"/>
	public const Char ObjectSignaturePrefixChar = 'L';
	/// <inheritdoc cref="ObjectSignatures.ObjectSignatureSuffix"/>
	public const Char ObjectSignatureSuffixChar = ';';
	/// <inheritdoc cref="ObjectSignatures.ArraySignaturePrefix"/>
	public const Char ArraySignaturePrefixChar = '[';

	/// <summary>
	/// Prefix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	public const String ObjectSignaturePrefix = "L";
	/// <summary>
	/// Suffix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	public const String ObjectSignatureSuffix = ";";
	/// <summary>
	/// Prefix for both array declaration in JNI signature for methods and properties and
	/// for JNI name of array classes.
	/// </summary>
	public const String ArraySignaturePrefix = "[";

	/// <summary>
	/// JNI signature for <c>java.lang.Object</c> object.
	/// </summary>
	public const String ObjectSignature = ObjectSignatures.ObjectSignaturePrefix + ClassNames.Object +
		ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Class&lt;?&gt;</c> object.
	/// </summary>
	public const String ClassObjectSignature = ObjectSignatures.ObjectSignaturePrefix + ClassNames.ClassObject +
		ObjectSignatures.ObjectSignatureSuffix;
}