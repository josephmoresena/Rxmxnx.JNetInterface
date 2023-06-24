namespace Rxmxnx.JNetInterface.Internal;

internal static class MethodSignatures
{
    /// <summary>
    /// JNI signature for void return.
    /// </summary>
    public const String VoidReturnSignature = "V";
    /// <summary>
    /// Prefix for the parameters declaration in the JNI signature for methods.
    /// </summary>
    public const String MethodParameterPrefix = "(";
    /// <summary>
    /// Sufix for the parameters declaration in the JNI signature for methods.
    /// </summary>
    public const String MethodParameterSuffix = ")";

    /// <summary>
    /// JNI signature for <c>java.lang.Boolean(boolean value)</c> method.
    /// </summary>
    public const String JBooleanObjectConstructorSignature = MethodParameterPrefix + PrimitiveSignatures.JBooleanSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;
    /// <summary>
    /// JNI signature for <c>java.lang.Byte(byte value)</c> method.
    /// </summary>
    public const String JByteObjectConstructorSignature = MethodParameterPrefix + PrimitiveSignatures.JByteSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;
    /// <summary>
    /// JNI signature for <c>java.lang.Character(boolean value)</c> method.
    /// </summary>
    public const String JCharacterObjectConstructorSignature = MethodParameterPrefix + PrimitiveSignatures.JCharSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;
    /// <summary>
    /// JNI signature for <c>java.lang.Double(double value)</c> method.
    /// </summary>
    public const String JDoubleObjectConstructorSignature = MethodParameterPrefix + PrimitiveSignatures.JDoubleSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;
    /// <summary>
    /// JNI signature for <c>java.lang.Float(float value)</c> method.
    /// </summary>
    public const String JFloatObjectConstructorSignature = MethodParameterPrefix + PrimitiveSignatures.JFloatSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;
    /// <summary>
    /// JNI signature for <c>java.lang.Integer(int value)</c> method.
    /// </summary>
    public const String JIntegerObjectConstructorSignature = MethodParameterPrefix + PrimitiveSignatures.JIntSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;
    /// <summary>
    /// JNI signature for <c>java.lang.Long(long value)</c> method.
    /// </summary>
    public const String JLongObjectConstructorSignature = MethodParameterPrefix + PrimitiveSignatures.JLongSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;
    /// <summary>
    /// JNI signature for <c>java.lang.Short(short value)</c> method.
    /// </summary>
    public const String JShortObjectConstructorSignature = MethodParameterPrefix + PrimitiveSignatures.JShortSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;
    /// <summary>
    /// JNI signature for <c>java.lang.ThreadGroup(String name)</c> method.
    /// </summary>
    public const String JThreadGroupConstructorSignature = MethodParameterPrefix + ObjectSignatures.JStringObjectSignature + MethodSignatures.MethodParameterSuffix + VoidReturnSignature;

    /// <summary>
    /// JNI signature for <c>java.lang.Class&lt;?&gt;.getName()</c> method.
    /// </summary>
    public const String GetClassNameMethodSignature = MethodParameterPrefix + MethodSignatures.MethodParameterSuffix + ObjectSignatures.ObjectSignaturePrefix + ClassNames.JStringObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    /// JNI signature for <c>java.lang.Throwable.getMessage()</c> method.
    /// </summary>
    public const String GetThrowableMessageMethodSignature = MethodParameterPrefix + MethodSignatures.MethodParameterSuffix + ObjectSignatures.ObjectSignaturePrefix + ClassNames.JStringObjectClassName + ObjectSignatures.ObjectSignatureSufix;
    /// <summary>
    /// JNI signature for <c>java.lang.System.getProperty(String key)</c> method.
    /// </summary>
    public const String GetPropertyMethodSignature = MethodParameterPrefix + ObjectSignatures.ObjectSignaturePrefix + ClassNames.JStringObjectClassName + ObjectSignatures.ObjectSignatureSufix + MethodSignatures.MethodParameterSuffix + ObjectSignatures.ObjectSignaturePrefix + ClassNames.JStringObjectClassName + ObjectSignatures.ObjectSignatureSufix;
}