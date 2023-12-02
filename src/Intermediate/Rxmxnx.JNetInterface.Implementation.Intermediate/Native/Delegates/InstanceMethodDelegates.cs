namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef CallObjectMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	IntPtr args);

internal delegate JObjectLocalRef CallObjectMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate JObjectLocalRef CallObjectMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Byte CallBooleanMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	IntPtr args);

internal delegate Byte CallBooleanMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate Byte CallBooleanMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate SByte
	CallByteMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);

internal delegate SByte CallByteMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate SByte CallByteMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Char CallCharMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);

internal delegate Char CallCharMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate Char CallCharMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int16 CallShortMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	IntPtr args);

internal delegate Int16 CallShortMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate Int16 CallShortMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int32 CallIntMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);

internal delegate Int32 CallIntMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate Int32 CallIntMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Int64
	CallLongMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);

internal delegate Int64 CallLongMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate Int64 CallLongMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Single CallFloatMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	IntPtr args);

internal delegate Single CallFloatMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate Single CallFloatMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate Double CallDoubleMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	IntPtr args);

internal delegate Double CallDoubleMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate Double CallDoubleMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);

internal delegate void CallVoidMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);

internal delegate void CallVoidMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ArgIterator args);

internal delegate void CallVoidMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj, JMethodId jMethod,
	ReadOnlyValPtr<JValue> args0);