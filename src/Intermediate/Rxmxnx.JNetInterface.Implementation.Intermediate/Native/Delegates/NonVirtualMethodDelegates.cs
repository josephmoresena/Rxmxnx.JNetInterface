namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef CallNonVirtualObjectMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, IntPtr args);

internal delegate JObjectLocalRef CallNonVirtualObjectMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate JObjectLocalRef CallNonVirtualObjectMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Byte CallNonVirtualBooleanMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, IntPtr args);

internal delegate Byte CallNonVirtualBooleanMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate Byte CallNonVirtualBooleanMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate SByte CallNonVirtualByteMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, IntPtr args);

internal delegate SByte CallNonVirtualByteMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate SByte CallNonVirtualByteMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Char CallNonVirtualCharMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JClassLocalRef jclass,
	JMethodId jMethod, IntPtr args);

internal delegate Char CallNonVirtualCharMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate Char CallNonVirtualCharMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Int16 CallNonVirtualShortMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, IntPtr args);

internal delegate Int16 CallNonVirtualShortMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate Int16 CallNonVirtualShortMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Int32 CallNonVirtualIntMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JClassLocalRef jclass,
	JMethodId jMethod, IntPtr args);

internal delegate Int32 CallNonVirtualIntMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate Int32 CallNonVirtualIntMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Int64 CallNonVirtualLongMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, IntPtr args);

internal delegate Int64 CallNonVirtualLongMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate Int64 CallNonVirtualLongMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Single CallNonVirtualFloatMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, IntPtr args);

internal delegate Single CallNonVirtualFloatMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate Single CallNonVirtualFloatMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Double CallNonVirtualDoubleMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, IntPtr args);

internal delegate Double CallNonVirtualDoubleMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate Double CallNonVirtualDoubleMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate void CallNonVirtualVoidMethodDelegate(JEnvironmentRef env, JObjectLocalRef obj, JClassLocalRef jclass,
	JMethodId jMethod, IntPtr args);

internal delegate void CallNonVirtualVoidMethodVDelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);

internal delegate void CallNonVirtualVoidMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);