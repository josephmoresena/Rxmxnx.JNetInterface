namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JResult PushLocalFrameDelegate(JEnvironmentRef env, Int32 capacity);
internal delegate JObjectLocalRef PopLocalFrameDelegate(JEnvironmentRef env, JObjectLocalRef result);
internal delegate JGlobalRef NewGlobalRefDelegate(JEnvironmentRef env, JObjectLocalRef localRef);
internal delegate void DeleteGlobalRefDelegate(JEnvironmentRef env, JGlobalRef globalRef);
internal delegate void DeleteLocalRefDelegate(JEnvironmentRef env, JObjectLocalRef localRef);
internal delegate Byte IsSameObjectDelegate(JEnvironmentRef env, JObjectLocalRef obj1, JObjectLocalRef obj2);
internal delegate JObjectLocalRef NewLocalRefDelegate(JEnvironmentRef env, JObjectLocalRef objRef);
internal delegate JResult EnsureLocalCapacityDelegate(JEnvironmentRef env, Int32 capacity);
internal delegate JWeakRef NewWeakGlobalRefDelegate(JEnvironmentRef env, JObjectLocalRef obj);
internal delegate void DeleteWeakGlobalRefDelegate(JEnvironmentRef env, JWeakRef jWeak);
internal delegate JReferenceType GetObjectRefTypeDelegate(JEnvironmentRef env, JObjectLocalRef obj);