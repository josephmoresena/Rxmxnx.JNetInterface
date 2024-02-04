namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef CallObjectMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Byte CallBooleanMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate SByte CallByteMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Char CallCharMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int16 CallShortMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int32 CallIntMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int64 CallLongMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Single CallFloatMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Double CallDoubleMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate void CallVoidMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);