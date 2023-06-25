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
	public const String JBooleanObjectConstructorSignature = MethodSignatures.MethodParameterPrefix +
		PrimitiveSignatures.JBooleanSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Byte(byte value)</c> method.
	/// </summary>
	public const String JByteObjectConstructorSignature = MethodSignatures.MethodParameterPrefix +
		PrimitiveSignatures.JByteSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Character(boolean value)</c> method.
	/// </summary>
	public const String JCharacterObjectConstructorSignature = MethodSignatures.MethodParameterPrefix +
		PrimitiveSignatures.JCharSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Double(double value)</c> method.
	/// </summary>
	public const String JDoubleObjectConstructorSignature = MethodSignatures.MethodParameterPrefix +
		PrimitiveSignatures.JDoubleSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Float(float value)</c> method.
	/// </summary>
	public const String JFloatObjectConstructorSignature = MethodSignatures.MethodParameterPrefix +
		PrimitiveSignatures.JFloatSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Integer(int value)</c> method.
	/// </summary>
	public const String JIntegerObjectConstructorSignature = MethodSignatures.MethodParameterPrefix +
		PrimitiveSignatures.JIntSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Long(long value)</c> method.
	/// </summary>
	public const String JLongObjectConstructorSignature = MethodSignatures.MethodParameterPrefix +
		PrimitiveSignatures.JLongSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Short(short value)</c> method.
	/// </summary>
	public const String JShortObjectConstructorSignature = MethodSignatures.MethodParameterPrefix +
		PrimitiveSignatures.JShortSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.ThreadGroup(String name)</c> method.
	/// </summary>
	public const String JThreadGroupConstructorSignature = MethodSignatures.MethodParameterPrefix +
		ObjectSignatures.JStringObjectSignature + MethodSignatures.MethodParameterSuffix +
		MethodSignatures.VoidReturnSignature;

	/// <summary>
	/// JNI signature for <c>java.lang.Class&lt;?&gt;.getName()</c> method.
	/// </summary>
	public const String GetClassNameMethodSignature = MethodSignatures.MethodParameterPrefix +
		MethodSignatures.MethodParameterSuffix + ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JStringObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.Throwable.getMessage()</c> method.
	/// </summary>
	public const String GetThrowableMessageMethodSignature = MethodSignatures.MethodParameterPrefix +
		MethodSignatures.MethodParameterSuffix + ObjectSignatures.ObjectSignaturePrefix +
		ClassNames.JStringObjectClassName + ObjectSignatures.ObjectSignatureSuffix;
	/// <summary>
	/// JNI signature for <c>java.lang.System.getProperty(String key)</c> method.
	/// </summary>
	public const String GetPropertyMethodSignature = MethodSignatures.MethodParameterPrefix +
		ObjectSignatures.ObjectSignaturePrefix + ClassNames.JStringObjectClassName +
		ObjectSignatures.ObjectSignatureSuffix + MethodSignatures.MethodParameterSuffix +
		ObjectSignatures.ObjectSignaturePrefix + ClassNames.JStringObjectClassName +
		ObjectSignatures.ObjectSignatureSuffix;
}