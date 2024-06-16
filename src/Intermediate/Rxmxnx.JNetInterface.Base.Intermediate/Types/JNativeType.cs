namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// JNI types.
/// </summary>
public enum JNativeType : Byte
{
	/// <summary>
	///     <c>void</c>
	/// </summary>
	Void = 0x0,
	/// <summary>
	///     <c>JNIEnv*</c>
	/// </summary>
	JEnvironmentRef = 0x01,
	/// <summary>
	///     <c>JavaVM*</c>
	/// </summary>
	JVirtualMachineRef = 0x02,

	/// <summary>
	///     <c>JNIEnv</c>
	/// </summary>
	JEnvironment = 0x11,
	/// <summary>
	///     <c>JavaVM</c>
	/// </summary>
	JVirtualMachine = 0x12,

	/// <summary>
	///     <c>JNINativeInterface_</c>
	/// </summary>
	JNativeInterface = 0x21,
	/// <summary>
	///     <c>JNIInvokeInterface_</c>
	/// </summary>
	JInvokeInterface = 0x22,

	/// <summary>
	///     <c>jfieldID</c>
	/// </summary>
	JField = 0x31,
	/// <summary>
	///     <c>jmethodID</c>
	/// </summary>
	JMethod = 0x32,

	/// <summary>
	///     <c>jboolean</c>
	/// </summary>
	JBoolean = 0x41,
	/// <summary>
	///     <c>jbyte</c>
	/// </summary>
	JByte = 0x42,
	/// <summary>
	///     <c>jchar</c>
	/// </summary>
	JChar = 0x43,
	/// <summary>
	///     <c>jdouble</c>
	/// </summary>
	JDouble = 0x44,
	/// <summary>
	///     <c>jfloat</c>
	/// </summary>
	JFloat = 0x45,
	/// <summary>
	///     <c>jint</c>
	/// </summary>
	JInt = 0x46,
	/// <summary>
	///     <c>jlong</c>
	/// </summary>
	JLong = 0x47,
	/// <summary>
	///     <c>jshort</c>
	/// </summary>
	JShort = 0x48,

	/// <summary>
	///     <c>jobject</c>
	/// </summary>
	JObject = 0x51,
	/// <summary>
	///     <c>jclass</c>
	/// </summary>
	JClass = 0x52,
	/// <summary>
	///     <c>jstring</c>
	/// </summary>
	JString = 0x53,
	/// <summary>
	///     <c>jarray</c>
	/// </summary>
	JArray = 0x54,
	/// <summary>
	///     <c>jthrowable</c>
	/// </summary>
	JThrowable = 0x55,

	/// <summary>
	///     <c>jweak</c>
	/// </summary>
	JWeak = 0x61,
	/// <summary>
	/// Global <c>jobject</c>
	/// </summary>
	JGlobal = 0x62,

	/// <summary>
	///     <c>jbooleanArray</c>
	/// </summary>
	JBooleanArray = 0x71,
	/// <summary>
	///     <c>jbyteArray</c>
	/// </summary>
	JByteArray = 0x72,
	/// <summary>
	///     <c>jcharArray</c>
	/// </summary>
	JCharArray = 0x73,
	/// <summary>
	///     <c>jdoubleArray</c>
	/// </summary>
	JDoubleArray = 0x74,
	/// <summary>
	///     <c>jfloatArray</c>
	/// </summary>
	JFloatArray = 0x75,
	/// <summary>
	///     <c>jintArray</c>
	/// </summary>
	JIntArray = 0x76,
	/// <summary>
	///     <c>jlongArray</c>
	/// </summary>
	JLongArray = 0x77,
	/// <summary>
	///     <c>jshortArray</c>
	/// </summary>
	JShortArray = 0x78,
	/// <summary>
	///     <c>jobjectArray</c>
	/// </summary>
	JObjectArray = 0x79,

	/// <summary>
	///     <c>JavaVMInitArgs</c>
	/// </summary>
	JVirtualMachineInitArgument = 0x81,

	/// <summary>
	///     <c>jvalue</c>
	/// </summary>
	JValue = 0xFF,
}

/// <summary>
/// Extensions for <see cref="JNativeType"/> enum.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class JNativeTypeExtensions
{
	/// <summary>
	/// Retrieves the name of <paramref name="nativeType"/> value.
	/// </summary>
	/// <param name="nativeType"><see cref="JNativeType"/> value.</param>
	/// <returns>The name of <paramref name="nativeType"/> value.</returns>
	public static String GetTypeName(this JNativeType nativeType)
	{
		return nativeType switch
		{
			JNativeType.JEnvironmentRef => CommonNames.JEnvironmentRefName,
			JNativeType.JVirtualMachineRef => CommonNames.JVirtualMachineRefName,

			JNativeType.JEnvironment => CommonNames.JEnvironmentName,
			JNativeType.JVirtualMachine => CommonNames.JVirtualMachineName,

			JNativeType.JNativeInterface => CommonNames.JNativeInterfaceName,
			JNativeType.JInvokeInterface => CommonNames.JInvokeInterfaceName,

			JNativeType.JField => CommonNames.JFieldIdName,
			JNativeType.JMethod => CommonNames.JMethodIdName,

			JNativeType.Void => CommonNames.JVoidName,
			JNativeType.JBoolean => CommonNames.JBooleanName,
			JNativeType.JByte => CommonNames.JByteName,
			JNativeType.JChar => CommonNames.JCharName,
			JNativeType.JDouble => CommonNames.JDoubleName,
			JNativeType.JFloat => CommonNames.JFloatName,
			JNativeType.JInt => CommonNames.JIntName,
			JNativeType.JLong => CommonNames.JLongName,
			JNativeType.JShort => CommonNames.JShortName,

			JNativeType.JObject => CommonNames.JObjectLocalRefName,
			JNativeType.JClass => CommonNames.JClassLocalRefName,
			JNativeType.JString => CommonNames.JStringLocalRefName,
			JNativeType.JArray => CommonNames.JArrayLocalRefName,
			JNativeType.JThrowable => CommonNames.JThrowableLocalRefName,

			JNativeType.JGlobal => CommonNames.JGlobalRefName,
			JNativeType.JWeak => CommonNames.JWeakRefName,

			JNativeType.JBooleanArray => CommonNames.JBooleanArrayLocalRefName,
			JNativeType.JByteArray => CommonNames.JByteArrayLocalRefName,
			JNativeType.JCharArray => CommonNames.JCharArrayLocalRefName,
			JNativeType.JDoubleArray => CommonNames.JDoubleArrayLocalRefName,
			JNativeType.JFloatArray => CommonNames.JFloatArrayLocalRefName,
			JNativeType.JIntArray => CommonNames.JIntArrayLocalRefName,
			JNativeType.JLongArray => CommonNames.JLongArrayLocalRefName,
			JNativeType.JShortArray => CommonNames.JShortArrayLocalRefName,
			JNativeType.JObjectArray => CommonNames.JObjectArrayLocalRefName,

			JNativeType.JVirtualMachineInitArgument => CommonNames.JVirtualMachineInitArgumentName,

			JNativeType.JValue => CommonNames.JValueName,
			_ => CommonValidationUtilities.ThrowInvalidNativeType(nativeType),
		};
	}
}