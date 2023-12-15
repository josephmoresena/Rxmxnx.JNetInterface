namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef AllocObjectDelegate(JEnvironmentRef env, JClassLocalRef jClass);

internal delegate JObjectLocalRef NewObjectADelegate(JEnvironmentRef env, JClassLocalRef jClass, JMethodId jMethod,
	ReadOnlyValPtr<JValue> arg0);