namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JClassLocalRef DefineClassDelegate(JEnvironmentRef env, ReadOnlyValPtr<Byte> nameChars0, JObjectLocalRef loader,
	IntPtr binaryData, Int32 len);

internal delegate JClassLocalRef FindClassDelegate(JEnvironmentRef env, ReadOnlyValPtr<Byte> nameChars0);

internal delegate JObjectLocalRef ToReflectedMethodDelegate(JEnvironmentRef env, JClassLocalRef jClass,
	JMethodId methodId, Byte isStatic);

internal delegate JClassLocalRef GetSuperclassDelegate(JEnvironmentRef env, JClassLocalRef sub);
internal delegate Byte IsAssignableFromDelegate(JEnvironmentRef env, JClassLocalRef sub, JClassLocalRef sup);
internal delegate JClassLocalRef GetObjectClassDelegate(JEnvironmentRef env, JObjectLocalRef obj);
internal delegate Byte IsInstanceOfDelegate(JEnvironmentRef env, JObjectLocalRef obj, JClassLocalRef jclass);

internal delegate JObjectLocalRef ToReflectedFieldDelegate(JEnvironmentRef env, JClassLocalRef jClass, JFieldId fieldId,
	Byte isStatic);

internal delegate JMethodId GetMethodIdDelegate(JEnvironmentRef env, JClassLocalRef jClass, ReadOnlyValPtr<Byte> nameChars0,
	ReadOnlyValPtr<Byte> signatureChars0);

internal delegate JFieldId GetFieldIdDelegate(JEnvironmentRef env, JClassLocalRef jclass, ReadOnlyValPtr<Byte> nameChars0,
	ReadOnlyValPtr<Byte> signatureChars0);

internal delegate JMethodId GetStaticMethodIdDelegate(JEnvironmentRef env, JClassLocalRef jClass, ReadOnlyValPtr<Byte> nameChars0,
	ReadOnlyValPtr<Byte> signatureChars0);

internal delegate JFieldId GetStaticFieldIdDelegate(JEnvironmentRef env, JClassLocalRef jclass, ReadOnlyValPtr<Byte> nameChars0,
	ReadOnlyValPtr<Byte> signatureChars0);

internal delegate JObjectLocalRef GetModuleDelegate(JEnvironmentRef env, JClassLocalRef jClass);