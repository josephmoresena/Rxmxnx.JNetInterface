namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef CallNonVirtualObjectMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Byte CallNonVirtualBooleanMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate SByte CallNonVirtualByteMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Char CallNonVirtualCharMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Int16 CallNonVirtualShortMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Int32 CallNonVirtualIntMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Int64 CallNonVirtualLongMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Single CallNonVirtualFloatMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate Double CallNonVirtualDoubleMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);

internal delegate void CallNonVirtualVoidMethodADelegate(JEnvironmentRef env, JObjectLocalRef obj,
	JClassLocalRef jclass, JMethodId jMethod, ReadOnlyValPtr<JValue> args0);