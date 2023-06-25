namespace Rxmxnx.JNetInterface;

/// <summary>
/// Unicode java objects signatures.
/// </summary>
public static partial class UnicodeObjectSignatures
{
	/// <summary>
	/// Prefix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	[DefaultValue(ObjectSignatures.ObjectSignaturePrefix)]
	public static readonly CString ObjectSignaturePrefix;
	/// <summary>
	/// Sufix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	[DefaultValue(ObjectSignatures.ObjectSignatureSuffix)]
	public static readonly CString ObjectSignatureSuffix;
	/// <summary>
	/// Prefix for both array declaration in JNI signature for methods and properties and
	/// for JNI name of array classes.
	/// </summary>
	[DefaultValue(ObjectSignatures.ArraySignaturePrefix)]
	public static readonly CString ArraySignaturePrefix;

	/// <summary>
	/// JNI signature for <c>java.lang.Object</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JObjectSignature)]
	internal static readonly CString JObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Byte</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JByteObjectSignature)]
	internal static readonly CString JByteObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Boolean</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JBooleanObjectSignature)]
	internal static readonly CString JBooleanObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Character</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JCharacterObjectSignature)]
	internal static readonly CString JCharacterObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Double</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JDoubleObjectSignature)]
	internal static readonly CString JDoubleObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Float</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JFloatObjectSignature)]
	internal static readonly CString JFloatObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Integer</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JIntegerObjectSignature)]
	internal static readonly CString JIntegerObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Long</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JLongObjectSignature)]
	internal static readonly CString JLongObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Short</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JShortObjectSignature)]
	internal static readonly CString JShortObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.String</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JStringObjectSignature)]
	internal static readonly CString JStringObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Class&lt;?&gt;</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JClassObjectSignature)]
	internal static readonly CString JClassObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Number</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JNumberObjectSignature)]
	internal static readonly CString JNumberObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Throwable</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JThrowableObjectSignature)]
	internal static readonly CString JThrowableObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.ThreadGroup</c> object.
	/// </summary>
	[DefaultValue(ObjectSignatures.JThreadGroupObjectSignature)]
	internal static readonly CString JThreadGroupObjectSignature;
}