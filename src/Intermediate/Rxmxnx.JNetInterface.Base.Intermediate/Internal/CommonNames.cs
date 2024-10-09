namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// JNI common names.
/// </summary>
internal static class CommonNames
{
	/// <summary>
	/// JNI name of Native Interface (<c>JNIEnv</c>).
	/// </summary>
	public const String JEnvironmentName = "JNIEnv_";
	/// <summary>
	/// JNI name of Invocation Interface (<c>JavaVM</c>).
	/// </summary>
	public const String JVirtualMachineName = "JavaVM_";

	/// <summary>
	/// JNI name of Native Interface Functions (<c>JNINativeInterface_</c>).
	/// </summary>
	public const String JNativeInterfaceName = "JNINativeInterface_";
	/// <summary>
	/// JNI name of Invocation Interface Functions (<c>JNIInvokeInterface_</c>).
	/// </summary>
	public const String JInvokeInterfaceName = "JNIInvokeInterface_";

	/// <summary>
	/// JNI name of method Id (<c>jmethodID</c>).
	/// </summary>
	public const String JMethodIdName = "jmethodID";
	/// <summary>
	/// JNI name of field Id (<c>jfieldID</c>).
	/// </summary>
	public const String JFieldIdName = "jfieldID";

	/// <summary>
	/// JNI name of <c>jboolean</c> value.
	/// </summary>
	public const String JBooleanName = "jboolean";
	/// <summary>
	/// JNI name of <c>jbyte</c> value.
	/// </summary>
	public const String JByteName = "jbyte";
	/// <summary>
	/// JNI name of <c>jchar</c> value.
	/// </summary>
	public const String JCharName = "jchar";
	/// <summary>
	/// JNI name of <c>jdouble</c> value.
	/// </summary>
	public const String JDoubleName = "jdouble";
	/// <summary>
	/// JNI name of <c>jfloat</c> value.
	/// </summary>
	public const String JFloatName = "jfloat";
	/// <summary>
	/// JNI name of <c>jint</c> value.
	/// </summary>
	public const String JIntName = "jint";
	/// <summary>
	/// JNI name of <c>jlong</c> value.
	/// </summary>
	public const String JLongName = "jlong";
	/// <summary>
	/// JNI name of <c>jshort</c> value.
	/// </summary>
	public const String JShortName = "jshort";

	/// <summary>
	/// JNI name of <c>JavaVMInitArgs</c> value.
	/// </summary>
	public const String JVirtualMachineInitArgumentName = "JavaVMInitArgs";

	/// <summary>
	/// JNI name of <c>jvalue</c> value.
	/// </summary>
	public const String JValueName = "jvalue";

	/// <summary>
	/// JNI name of Native Interface (<c>JNIEnv*</c>).
	/// </summary>
	public const String JEnvironmentRefName = "JNIEnv*";
	/// <summary>
	/// JNI name of Invocation Interface (<c>JavaVM*</c>).
	/// </summary>
	public const String JVirtualMachineRefName = "JavaVM*";

	/// <summary>
	/// JNI name of <c>java.lang.Object</c> reference (<c>jobject</c>).
	/// </summary>
	public const String JObjectLocalRefName = "jobject";
	/// <summary>
	/// JNI name of <c>java.lang.Class&lt;?&gt;</c> reference (<c>jclass</c>).
	/// </summary>
	public const String JClassLocalRefName = "jclass";
	/// <summary>
	/// JNI name of <c>java.lang.String</c> reference (<c>jstring</c>).
	/// </summary>
	public const String JStringLocalRefName = "jstring";
	/// <summary>
	/// JNI name of array reference (<c>jarray</c>).
	/// <list type="table">
	///     <listheader>
	///         <term>Array item</term>
	///         <description>Description</description>
	///     </listheader>
	///     <item>
	///         <term>
	///             <c>boolean</c>
	///         </term>
	///         <description><c>jbooleanArray</c>.</description>
	///     </item>
	///     <item>
	///         <term>
	///             <c>byte</c>
	///         </term>
	///         <description><c>jbyteArray</c>.</description>
	///     </item>
	///     <item>
	///         <term>
	///             <c>char</c>
	///         </term>
	///         <description><c>jcharArray</c>.</description>
	///     </item>
	///     <item>
	///         <term>
	///             <c>double</c>
	///         </term>
	///         <description><c>jdoubleArray</c>.</description>
	///     </item>
	///     <item>
	///         <term>
	///             <c>float</c>
	///         </term>
	///         <description><c>jfloatArray</c>.</description>
	///     </item>
	///     <item>
	///         <term>
	///             <c>int</c>
	///         </term>
	///         <description><c>jintArray</c>.</description>
	///     </item>
	///     <item>
	///         <term>
	///             <c>long</c>
	///         </term>
	///         <description><c>jlongArray</c>.</description>
	///     </item>
	///     <item>
	///         <term>Any fully-qualified-class type.</term><description><c>jobjectArray</c>.</description>
	///     </item>
	///     <item>
	///         <term>
	///             <c>short</c>
	///         </term>
	///         <description><c>jshortArray</c>.</description>
	///     </item>
	/// </list>
	/// </summary>
	public const String JArrayLocalRefName = "jarray";
	/// <summary>
	/// JNI name of <c>java.lang.Throwable</c> reference (<c>jthrowable</c>).
	/// </summary>
	public const String JThrowableLocalRefName = "jthrowable";

	/// <summary>
	/// JNI name of array reference (<c>jbooleanArray</c>).
	/// </summary>
	public const String JBooleanArrayLocalRefName = "jbooleanArray";
	/// <summary>
	/// JNI name of array reference (<c>jbyteArray</c>).
	/// </summary>
	public const String JByteArrayLocalRefName = "jbyteArray";
	/// <summary>
	/// JNI name of array reference (<c>jcharArray</c>).
	/// </summary>
	public const String JCharArrayLocalRefName = "jcharArray";
	/// <summary>
	/// JNI name of array reference (<c>jdoubleArray</c>).
	/// </summary>
	public const String JDoubleArrayLocalRefName = "jdoubleArray";
	/// <summary>
	/// JNI name of array reference (<c>jfloatArray</c>).
	/// </summary>
	public const String JFloatArrayLocalRefName = "jfloatArray";
	/// <summary>
	/// JNI name of array reference (<c>jintArray</c>).
	/// </summary>
	public const String JIntArrayLocalRefName = "jintArray";
	/// <summary>
	/// JNI name of array reference (<c>jlongArray</c>).
	/// </summary>
	public const String JLongArrayLocalRefName = "jlongArray";
	/// <summary>
	/// JNI name of array reference (<c>jobjectArray</c>).
	/// </summary>
	public const String JObjectArrayLocalRefName = "jobjectArray";
	/// <summary>
	/// JNI name of array reference (<c>jshortArray</c>).
	/// </summary>
	public const String JShortArrayLocalRefName = "jshortArray";

	/// <summary>
	/// JNI name of weak global reference (<c>jweak</c>).
	/// </summary>
	public const String JWeakRefName = "jweak";
	/// <summary>
	/// Internal name of global <c>jobject</c> reference.
	/// </summary>
	public const String JGlobalRefName = "jobject-global";

	/// <summary>
	/// Java class name of primitive <c>void</c> class.
	/// </summary>
	public const String VoidPrimitive = "void";
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
	/// Prefix for the parameters declaration in the JNI signature for methods.
	/// </summary>
	public const Byte MethodParameterPrefixChar = (Byte)'(';
	/// <summary>
	/// Suffix for the parameters declaration in the JNI signature for methods.
	/// </summary>
	public const Byte MethodParameterSuffixChar = (Byte)')';
	/// <summary>
	/// Prefix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	public const Byte ObjectSignaturePrefixChar = (Byte)'L';
	/// <summary>
	/// Suffix for fully-qualified-class type declaration in the JNI signature for methods and properties.
	/// </summary>
	public const Byte ObjectSignatureSuffixChar = (Byte)';';
	/// <summary>
	/// Prefix for both array declaration in JNI signature for methods and properties and
	/// for JNI name of array classes.
	/// </summary>
	public const Byte ArraySignaturePrefixChar = (Byte)'[';
	/// <summary>
	/// JNI signature for primitive <c>void</c>.
	/// </summary>
	public const Byte VoidSignatureChar = (Byte)'V';
	/// <summary>
	/// JNI signature for primitive <c>boolean</c>.
	/// </summary>
	public const Byte BooleanSignatureChar = (Byte)'Z';
	/// <summary>
	/// JNI signature for primitive <c>byte</c>.
	/// </summary>
	public const Byte ByteSignatureChar = (Byte)'B';
	/// <summary>
	/// JNI signature for primitive <c>char</c>.
	/// </summary>
	public const Byte CharSignatureChar = (Byte)'C';
	/// <summary>
	/// JNI signature for primitive <c>double</c>.
	/// </summary>
	public const Byte DoubleSignatureChar = (Byte)'D';
	/// <summary>
	/// JNI signature for primitive <c>float</c>.
	/// </summary>
	public const Byte FloatSignatureChar = (Byte)'F';
	/// <summary>
	/// JNI signature for primitive <c>int</c>.
	/// </summary>
	public const Byte IntSignatureChar = (Byte)'I';
	/// <summary>
	/// JNI signature for primitive <c>long</c>.
	/// </summary>
	public const Byte LongSignatureChar = (Byte)'J';
	/// <summary>
	/// JNI signature for primitive <c>short</c>.
	/// </summary>
	public const Byte ShortSignatureChar = (Byte)'S';
	/// <summary>
	/// JNI name of <c>java.lang.Object</c> class.
	/// </summary>
	public static ReadOnlySpan<Byte> Object => "java/lang/Object"u8;
	/// <summary>
	/// JNI name of <c>java.lang.Class&lt;?&gt;</c> class.
	/// </summary>
	public static ReadOnlySpan<Byte> ClassObject => "java/lang/Class"u8;
	/// <summary>
	/// JNI name of <c>java.lang.reflect.Proxy</c> class.
	/// </summary>
	public static ReadOnlySpan<Byte> ProxyObject => "java/lang/reflect/Proxy"u8;
	/// <summary>
	/// JNI name for class constructors.
	/// </summary>
	public static ReadOnlySpan<Byte> Constructor => "<init>"u8;
}