namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef CallStaticObjectMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass,
	JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Byte CallStaticBooleanMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate SByte CallStaticByteMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Char CallStaticCharMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int16 CallStaticShortMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int32 CallStaticIntMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int64 CallStaticLongMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Single CallStaticFloatMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Double CallStaticDoubleMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate void CallStaticVoidMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);