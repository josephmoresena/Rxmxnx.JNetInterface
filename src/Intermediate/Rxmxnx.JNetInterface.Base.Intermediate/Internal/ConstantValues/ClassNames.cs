namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java classes names.
/// </summary>
internal static class ClassNames
{
	/// <summary>
	/// JNI representation of <c>java.lang</c> package.
	/// </summary>
	public const String LangPackage = "java/lang/";
	/// <summary>
	/// JNI representation of <c>java.io</c> package.
	/// </summary>
	public const String IoPackage = "java/io/";
	/// <summary>
	/// JNI representation of <c>java.nio</c> package.
	/// </summary>
	public const String NioPackage = "java/nio/";
	/// <summary>
	/// JNI representation of <c>java.reflect</c> package.
	/// </summary>
	public const String ReflectPackage = "java/reflect/";
	/// <summary>
	/// JNI representation of <c>sun.nio.ch</c> package.
	/// </summary>
	public const String SunNioChPackage = "sun/nio/ch/";

	/// <summary>
	/// Java class name of primitive <c>boolean</c> class.
	/// </summary>
	public const String BooleanPrimitive = "boolean";
	/// <summary>
	/// Java class name of primitive <c>byte</c> class.
	/// </summary>
	public const String BytePrimitive = "byte";
	/// <summary>
	/// Java class name of primitive <c>char</c> class.
	/// </summary>
	public const String CharPrimitive = "char";
	/// <summary>
	/// Java class name of primitive <c>double</c> class.
	/// </summary>
	public const String DoublePrimitive = "double";
	/// <summary>
	/// Java class name of primitive <c>float</c> class.
	/// </summary>
	public const String FloatPrimitive = "float";
	/// <summary>
	/// Java class name of primitive <c>int</c> class.
	/// </summary>
	public const String IntPrimitive = "int";
	/// <summary>
	/// Java class name of primitive <c>long</c> class.
	/// </summary>
	public const String LongPrimitive = "long";
	/// <summary>
	/// Java class name of primitive <c>short</c> class.
	/// </summary>
	public const String ShortPrimitive = "short";

	/// <summary>
	/// JNI name of <c>java.lang.Object</c> class.
	/// </summary>
	public const String Object = ClassNames.LangPackage + "Object";
	/// <summary>
	/// JNI name of <c>java.lang.Boolean</c> class.
	/// </summary>
	public const String BooleanObject = ClassNames.LangPackage + "Boolean";
	/// <summary>
	/// JNI name of <c>java.lang.Byte</c> class.
	/// </summary>
	public const String ByteObject = ClassNames.LangPackage + "Byte";
	/// <summary>
	/// JNI name of <c>java.lang.Character</c> class.
	/// </summary>
	public const String CharacterObject = ClassNames.LangPackage + "Character";
	/// <summary>
	/// JNI name of <c>java.lang.Double</c> class.
	/// </summary>
	public const String DoubleObject = ClassNames.LangPackage + "Double";
	/// <summary>
	/// JNI name of <c>java.lang.Float</c> class.
	/// </summary>
	public const String FloatObject = ClassNames.LangPackage + "Float";
	/// <summary>
	/// JNI name of <c>java.lang.Integer</c> class.
	/// </summary>
	public const String IntegerObject = ClassNames.LangPackage + "Integer";
	/// <summary>
	/// JNI name of <c>java.lang.Long</c> class.
	/// </summary>
	public const String LongObject = ClassNames.LangPackage + "Long";
	/// <summary>
	/// JNI name of <c>java.lang.Short</c> class.
	/// </summary>
	public const String ShortObject = ClassNames.LangPackage + "Short";
	/// <summary>
	/// JNI name of <c>java.lang.String</c> class.
	/// </summary>
	public const String StringObject = ClassNames.LangPackage + "String";
	/// <summary>
	/// JNI name of <c>java.lang.Class&lt;?&gt;</c> class.
	/// </summary>
	public const String ClassObject = ClassNames.LangPackage + "Class";
	/// <summary>
	/// JNI name of <c>java.lang.Number</c> class.
	/// </summary>
	public const String NumberObject = ClassNames.LangPackage + "Number";
	/// <summary>
	/// JNI name of <c>java.lang.Enum</c> class.
	/// </summary>
	public const String EnumObject = ClassNames.LangPackage + "Enum";
	/// <summary>
	/// JNI name of <c>java.lang.Throwable</c> class.
	/// </summary>
	public const String ThrowableObject = ClassNames.LangPackage + "Throwable";
	/// <summary>
	/// JNI name of <c>java.lang.ThreadGroup</c> class.
	/// </summary>
	public const String ThreadGroupObject = ClassNames.LangPackage + "ThreadGroup";
	/// <summary>
	/// JNI name of <c>java.lang.System</c> class.
	/// </summary>
	public const String SystemObject = ClassNames.LangPackage + "System";
	/// <summary>
	/// JNI name of <c>java.lang.Cloneable</c> interface.
	/// </summary>
	public const String CloneableInterface = ClassNames.LangPackage + "Cloneable";
	/// <summary>
	/// JNI name of <c>java.lang.Comparable</c> interface.
	/// </summary>
	public const String ComparableInterface = ClassNames.LangPackage + "Comparable";
	/// <summary>
	/// JNI name of <c>java.lang.CharSequence</c> interface.
	/// </summary>
	public const String CharSequenceInterface = ClassNames.LangPackage + "CharSequence";
	/// <summary>
	/// JNI name of <c>java.io.Serializable</c> interface.
	/// </summary>
	public const String SerializableInterface = ClassNames.IoPackage + "Serializable";
	/// <summary>
	/// JNI name of <c>java.reflect.AnnotatedElement</c> interface.
	/// </summary>
	public const String AnnotatedElementInterface = ClassNames.ReflectPackage + "AnnotatedElement";
	/// <summary>
	/// JNI name of <c>java.reflect.GenericDeclaration</c> interface.
	/// </summary>
	public const String GenericDeclarationInterface = ClassNames.ReflectPackage + "GenericDeclaration";
	/// <summary>
	/// JNI name of <c>java.reflect.Type</c> interface.
	/// </summary>
	public const String TypeInterface = ClassNames.ReflectPackage + "Type";
	/// <summary>
	/// JNI name of <c>java.lang.StackTraceElement</c> class.
	/// </summary>
	public const String StackTraceElementObject = ClassNames.LangPackage + "StackTraceElement";

	/// <summary>
	/// JNI name of <c>java.nio.Buffer</c> class.
	/// </summary>
	public const String BufferObject = ClassNames.NioPackage + "Buffer";
	/// <summary>
	/// JNI name of <c>java.nio.ByteBuffer</c> class.
	/// </summary>
	public const String ByteBufferObject = ClassNames.NioPackage + "ByteBuffer";
	/// <summary>
	/// JNI name of <c>java.nio.MappedByteBuffer</c> class.
	/// </summary>
	public const String MappedByteBufferObject = ClassNames.NioPackage + "MappedByteBuffer";
	/// <summary>
	/// JNI name of <c>java.nio.MappedByteBuffer</c> class.
	/// </summary>
	public const String DirectByteBufferObject = ClassNames.NioPackage + "DirectByteBuffer";
	/// <summary>
	/// JNI name of <c>sun.nio.ch.DirectByteBuffer</c> interface.
	/// </summary>
	public const String DirectBufferObject = ClassNames.SunNioChPackage + "DirectBuffer";

	/// <summary>
	/// JNI name of <c>java.lang.Thread.UncaughtExceptionHandler</c> interface.
	/// </summary>
	public const String UncaughtExceptionHandlerInterface = ClassNames.LangPackage + "Thread$UncaughtExceptionHandler";
}