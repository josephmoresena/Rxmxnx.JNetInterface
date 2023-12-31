namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java objects names.
/// </summary>
internal static class ObjectSignatures
{
	/// <summary>
	/// Prefix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	public const String ObjectSignaturePrefix = "L";
	/// <summary>
	/// Sufix for fully-qualified-class type declaration in the JNI signature for methods and properties.
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
	public const String JObjectSignature = ObjectSignatures.ObjectSignaturePrefix + ClassNames.JObjectClassName +
		ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Class&lt;?&gt;</c> object.
	/// </summary>
	public const String JClassObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JClassObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
}