namespace Rxmxnx.JNetInterface.Internal;

internal static class ObjectSignatures
{
	/// <summary>
	/// Prefix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	public const String ObjectSignaturePrefix = "L";
	/// <summary>
	/// Sufix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	public const String ObjectSignatureSufix = ";";
	/// <summary>
	/// Prefix for both array declaration in JNI signature for methods and properties and 
	/// for JNI name of array classes.
	/// </summary>
	public const String ArraySignaturePrefix = "[";

	/// <summary>
	/// JNI signature for <c>java.lang.Object</c> object.
	/// </summary>
	public const String JObjectSignature = ObjectSignaturePrefix + ClassNames.JObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Byte</c> object.
	/// </summary>
	public const String JByteObjectSignature = ObjectSignaturePrefix + ClassNames.JByteObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Boolean</c> object.
	/// </summary>
	public const String JBooleanObjectSignature = ObjectSignaturePrefix + ClassNames.JBooleanObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Character</c> object.
	/// </summary>
	public const String JCharacterObjectSignature = ObjectSignaturePrefix + ClassNames.JCharacterObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Double</c> object.
	/// </summary>
	public const String JDoubleObjectSignature = ObjectSignaturePrefix + ClassNames.JDoubleObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Float</c> object.
	/// </summary>
	public const String JFloatObjectSignature = ObjectSignaturePrefix + ClassNames.JFloatObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Integer</c> object.
	/// </summary>
	public const String JIntegerObjectSignature = ObjectSignaturePrefix + ClassNames.JIntegerObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Long</c> object.
	/// </summary>
	public const String JLongObjectSignature = ObjectSignaturePrefix + ClassNames.JLongObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Short</c> object.
	/// </summary>
	public const String JShortObjectSignature = ObjectSignaturePrefix + ClassNames.JShortObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.String</c> object.
	/// </summary>
	public const String JStringObjectSignature = ObjectSignaturePrefix + ClassNames.JStringObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Class&lt;?&gt;</c> object.
	/// </summary>
	public const String JClassObjectSignature = ObjectSignaturePrefix + ClassNames.JClassObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Number</c> object.
	/// </summary>
	public const String JNumberObjectSignature = ObjectSignaturePrefix + ClassNames.JNumberObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.Throwable</c> object.
	/// </summary>
	public const String JThrowableObjectSignature = ObjectSignaturePrefix + ClassNames.JThrowableObjectClassName + ObjectSignatureSufix;
	/// <summary>
	/// JNI signature for <c>java.lang.ThreadGroup</c> object.
	/// </summary>
	public const String JThreadGroupObjectSignature = ObjectSignaturePrefix + ClassNames.JThreadGroupObjectClassName + ObjectSignatureSufix;
}