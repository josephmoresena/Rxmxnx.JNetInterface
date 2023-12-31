namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java classes names.
/// </summary>
internal static class ClassNames
{
	/// <summary>
	/// JNI representation of <c>java.lang</c> package.
	/// </summary>
	public const String JLangPath = "java/lang/";
	/// <summary>
	/// JNI representation of <c>java.io</c> package.
	/// </summary>
	public const String JIoPath = "java/io/";
	/// <summary>
	/// JNI representation of <c>java.reflect</c> package.
	/// </summary>
	public const String JReflectPath = "java/reflect/";
	/// <summary>
	/// JNI name of <c>java.lang.Object</c> class.
	/// </summary>
	public const String JObjectClassName = ClassNames.JLangPath + "Object";

	/// <summary>
	/// JNI name of <c>java.lang.String</c> class.
	/// </summary>
	public const String JStringObjectClassName = ClassNames.JLangPath + "String";
	/// <summary>
	/// JNI name of <c>java.lang.Class&lt;?&gt;</c> class.
	/// </summary>
	public const String JClassObjectClassName = ClassNames.JLangPath + "Class";
	/// <summary>
	/// JNI name of <c>java.lang.Number</c> class.
	/// </summary>
	public const String JNumberObjectClassName = ClassNames.JLangPath + "Number";
	/// <summary>
	/// JNI name of <c>java.lang.Enum</c> class.
	/// </summary>
	public const String JEnumObjectClassName = ClassNames.JLangPath + "Enum";
	/// <summary>
	/// JNI name of <c>java.lang.Throwable</c> class.
	/// </summary>
	public const String JThrowableObjectClassName = ClassNames.JLangPath + "Throwable";
	/// <summary>
	/// JNI name of <c>java.lang.ThreadGroup</c> class.
	/// </summary>
	public const String JThreadGroupObjectClassName = ClassNames.JLangPath + "ThreadGroup";
	/// <summary>
	/// JNI name of <c>java.lang.System</c> class.
	/// </summary>
	public const String JSystemClassName = ClassNames.JLangPath + "System";
	/// <summary>
	/// JNI name of <c>java.lang.Cloneable</c> interface.
	/// </summary>
	public const String JCloneableInterfaceName = ClassNames.JLangPath + "Cloneable";
	/// <summary>
	/// JNI name of <c>java.lang.Comparable</c> interface.
	/// </summary>
	public const String JComparableInterfaceName = ClassNames.JLangPath + "Comparable";
	/// <summary>
	/// JNI name of <c>java.lang.CharSequence</c> interface.
	/// </summary>
	public const String JCharSequenceInterfaceName = ClassNames.JLangPath + "CharSequence";
	/// <summary>
	/// JNI name of <c>java.io.Serializable</c> interface.
	/// </summary>
	public const String JSerializableInterfaceName = ClassNames.JIoPath + "Serializable";
	/// <summary>
	/// JNI name of <c>java.reflect.AnnotatedElement</c> interface.
	/// </summary>
	public const String JAnnotatedElementInterfaceName = ClassNames.JReflectPath + "AnnotatedElement";
	/// <summary>
	/// JNI name of <c>java.reflect.GenericDeclaration</c> interface.
	/// </summary>
	public const String JGenericDeclarationInterfaceName = ClassNames.JReflectPath + "GenericDeclaration";
	/// <summary>
	/// JNI name of <c>java.reflect.Type</c> interface.
	/// </summary>
	public const String JTypeInterfaceName = ClassNames.JReflectPath + "Type";
	/// <summary>
	/// JNI name of <c>java.lang.StackTraceElement</c> class.
	/// </summary>
	public const String JStackTraceElementClassName = ClassNames.JLangPath + "StackTraceElement";
	/// <summary>
	/// JNI name of <c>java.lang.Thread.UncaughtExceptionHandler</c> interface.
	/// </summary>
	public const String JUncaughtExceptionHandlerInterfaceName =
		ClassNames.JLangPath + "Thread$UncaughtExceptionHandler";
}