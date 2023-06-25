namespace Rxmxnx.JNetInterface.Internal;

internal static class ObjectSignatures
{
    /// <summary>
    ///     Prefix for fully-qualified-class type declaration in the JNI signature for methods and properties.
    /// </summary>
    public const String ObjectSignaturePrefix = "L";
    /// <summary>
    ///     Sufix for fully-qualified-class type declaration in the JNI signature for methods and properties.
    /// </summary>
    public const String ObjectSignatureSufix = ";";
    /// <summary>
    ///     Prefix for both array declaration in JNI signature for methods and properties and
    ///     for JNI name of array classes.
    /// </summary>
    public const String ArraySignaturePrefix = "[";

    /// <summary>
    ///     JNI signature for <c>java.lang.Object</c> object.
    /// </summary>
    public const String JObjectSignature = ObjectSignatures.ObjectSignaturePrefix + ClassNames.JObjectClassName +
		ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Byte</c> object.
    /// </summary>
    public const String JByteObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JByteObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Boolean</c> object.
    /// </summary>
    public const String JBooleanObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JBooleanObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Character</c> object.
    /// </summary>
    public const String JCharacterObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JCharacterObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Double</c> object.
    /// </summary>
    public const String JDoubleObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JDoubleObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Float</c> object.
    /// </summary>
    public const String JFloatObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JFloatObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Integer</c> object.
    /// </summary>
    public const String JIntegerObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JIntegerObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Long</c> object.
    /// </summary>
    public const String JLongObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JLongObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Short</c> object.
    /// </summary>
    public const String JShortObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JShortObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.String</c> object.
    /// </summary>
    public const String JStringObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JStringObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Class&lt;?&gt;</c> object.
    /// </summary>
    public const String JClassObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JClassObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Number</c> object.
    /// </summary>
    public const String JNumberObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JNumberObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.Throwable</c> object.
    /// </summary>
    public const String JThrowableObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JThrowableObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    ///     JNI signature for <c>java.lang.ThreadGroup</c> object.
    /// </summary>
    public const String JThreadGroupObjectSignature = ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JThreadGroupObjectClassName + ObjectSignatures.ObjectSignatureSufix;
}