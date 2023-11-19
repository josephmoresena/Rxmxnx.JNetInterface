namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef AllocObjectDelegate(JEnvironmentRef env, JClassLocalRef jClass);

internal delegate JObjectLocalRef NewObjectDelegate(JEnvironmentRef env, JClassLocalRef jClass, JMethodId jMethod,
	IntPtr args);

internal delegate JObjectLocalRef NewObjectVDelegate(JEnvironmentRef env, JClassLocalRef jClass, JMethodId jMethod,
	ArgIterator args);

internal delegate JObjectLocalRef NewObjectADelegate(JEnvironmentRef env, JClassLocalRef jClass, JMethodId jMethod,
	in JValue arg0);