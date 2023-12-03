namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java native values names.
/// </summary>
internal static class ValueNames
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
}