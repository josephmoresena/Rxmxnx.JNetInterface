namespace Rxmxnx.JNetInterface;

/// <summary>
/// Unicode method signatures.
/// </summary>
public static partial class UnicodeMethodSignatures
{
	/// <summary>
	/// JNI signature for void return.
	/// </summary>
	[DefaultValue(MethodSignatures.VoidReturnSignature)]
	public static readonly CString VoidReturnSignature;
	/// <summary>
	/// Prefix for the parameters declaration in the JNI signature for methods.
	/// </summary>
	[DefaultValue(MethodSignatures.MethodParameterPrefix)]
	public static readonly CString MethodParameterPrefix;
	/// <summary>
	/// Suffix for the parameters declaration in the JNI signature for methods.
	/// </summary>
	[DefaultValue(MethodSignatures.MethodParameterSuffix)]
	public static readonly CString MethodParameterSuffix;

	/// <summary>
	/// JNI signature for <c>java.lang.Boolean(boolean value)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JBooleanObjectConstructorSignature)]
	internal static readonly CString JBooleanObjectConstructorSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Byte(byte value)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JByteObjectConstructorSignature)]
	internal static readonly CString JByteObjectConstructorSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Character(boolean value)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JCharacterObjectConstructorSignature)]
	internal static readonly CString JCharacterObjectConstructorSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Double(double value)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JDoubleObjectConstructorSignature)]
	internal static readonly CString JDoubleObjectConstructorSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Float(float value)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JFloatObjectConstructorSignature)]
	internal static readonly CString JFloatObjectConstructorSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Integer(int value)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JIntegerObjectConstructorSignature)]
	internal static readonly CString JIntegerObjectConstructorSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Long(long value)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JLongObjectConstructorSignature)]
	internal static readonly CString JLongObjectConstructorSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Short(short value)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JShortObjectConstructorSignature)]
	internal static readonly CString JShortObjectConstructorSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.ThreadGroup(String name)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.JThreadGroupConstructorSignature)]
	internal static readonly CString JThreadGroupConstructorSignature;

	/// <summary>
	/// JNI signature for <c>java.lang.Class&lt;?&gt;.getName()</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.GetClassNameMethodSignature)]
	internal static readonly CString GetClassNameMethodSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Throwable.getMessage()</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.GetThrowableMessageMethodSignature)]
	internal static readonly CString GetThrowableMessageMethodSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.System.getProperty(String key)</c> method.
	/// </summary>
	[DefaultValue(MethodSignatures.GetPropertyMethodSignature)]
	internal static readonly CString GetPropertyMethodSignature;
}