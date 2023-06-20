namespace Rxmxnx.JNetInterface.Internal;

internal static class ReferenceNames
{
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
    /// <listheader>
    /// <term>Array item</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item><term><c>boolean</c></term><description><c>jbooleanArray</c>.</description></item>
    /// <item><term><c>byte</c></term><description><c>jbyteArray</c>.</description></item>
    /// <item><term><c>char</c></term><description><c>jcharArray</c>.</description></item>
    /// <item><term><c>double</c></term><description><c>jdoubleArray</c>.</description></item>
    /// <item><term><c>float</c></term><description><c>jfloatArray</c>.</description></item>
    /// <item><term><c>int</c></term><description><c>jintArray</c>.</description></item>
    /// <item><term><c>long</c></term><description><c>jlongArray</c>.</description></item>
    /// <item><term>Any fully-qualified-class type.</term><description><c>jobjectArray</c>.</description></item>
    /// <item><term><c>short</c></term><description><c>jshortArray</c>.</description></item>
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
}