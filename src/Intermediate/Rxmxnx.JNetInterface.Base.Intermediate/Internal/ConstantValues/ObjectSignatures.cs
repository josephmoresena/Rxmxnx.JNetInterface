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
	/// JNI signature for <c>java.lang.Byte</c> object.
	/// </summary>
	public const String JByteObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JByteObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Boolean</c> object.
	/// </summary>
	public const String JBooleanObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JBooleanObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Character</c> object.
	/// </summary>
	public const String JCharacterObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JCharacterObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Double</c> object.
	/// </summary>
	public const String JDoubleObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JDoubleObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Float</c> object.
	/// </summary>
	public const String JFloatObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JFloatObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Integer</c> object.
	/// </summary>
	public const String JIntegerObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JIntegerObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Long</c> object.
	/// </summary>
	public const String JLongObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JLongObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Short</c> object.
	/// </summary>
	public const String JShortObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JShortObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.String</c> object.
	/// </summary>
	public const String JStringObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JStringObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Class&lt;?&gt;</c> object.
	/// </summary>
	public const String JClassObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JClassObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Number</c> object.
	/// </summary>
	public const String JNumberObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JNumberObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Enum</c> object.
	/// </summary>
	public const String JEnumObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JEnumObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Throwable</c> object.
	/// </summary>
	public const String JThrowableObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JThrowableObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.ThreadGroup</c> object.
	/// </summary>
	public const String JThreadGroupObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JThreadGroupObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.System</c> object.
	/// </summary>
	public const String JSystemObjectSignature = ObjectSignatures.ObjectSignaturePrefix + ClassNames.JSystemClassName +
		ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Cloneable</c> object.
	/// </summary>
	public const String JCloneableObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JCloneableInterfaceName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Comparable</c> object.
	/// </summary>
	public const String JComparableObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JComparableInterfaceName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.CharSequence</c> object.
	/// </summary>
	public const String JCharSequenceObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JCharSequenceInterfaceName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.io.Serializable</c> object.
	/// </summary>
	public const String JSerializableObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JSerializableInterfaceName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.reflect.AnnotatedElement</c> object.
	/// </summary>
	public const String JAnnotatedElementObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JAnnotatedElementInterfaceName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.reflect.GenericDeclaration</c> object.
	/// </summary>
	public const String JGenericDeclarationObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JGenericDeclarationInterfaceName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.reflect.Type</c> object.
	/// </summary>
	public const String JTypeObjectSignature = ObjectSignatures.ObjectSignaturePrefix + ClassNames.JTypeInterfaceName +
		ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.StackTraceElement</c> object.
	/// </summary>
	public const String JStackTraceElementObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JStackTraceElementClassName + ObjectSignatures.ObjectSignatureSuffix;
}