namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef CallStaticObjectMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass,
	JMethodId jMethod, IntPtr args);

internal delegate JObjectLocalRef CallStaticObjectMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass,
	JMethodId jMethod, ArgIterator args);

internal delegate JObjectLocalRef CallStaticObjectMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass,
	JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Byte CallStaticBooleanMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate Byte CallStaticBooleanMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate Byte CallStaticBooleanMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate SByte CallStaticByteMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate SByte CallStaticByteMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate SByte CallStaticByteMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Char CallStaticCharMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate Char CallStaticCharMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate Char CallStaticCharMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int16 CallStaticShortMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate Int16 CallStaticShortMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate Int16 CallStaticShortMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int32 CallStaticIntMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate Int32 CallStaticIntMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate Int32 CallStaticIntMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int64 CallStaticLongMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate Int64 CallStaticLongMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate Int64 CallStaticLongMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Single CallStaticFloatMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate Single CallStaticFloatMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate Single CallStaticFloatMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Double CallStaticDoubleMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate Double CallStaticDoubleMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate Double CallStaticDoubleMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate void CallStaticVoidMethodDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	IntPtr args);

internal delegate void CallStaticVoidMethodVDelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ArgIterator args);

internal delegate void CallStaticVoidMethodADelegate(JEnvironmentRef env, JClassLocalRef jclass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);