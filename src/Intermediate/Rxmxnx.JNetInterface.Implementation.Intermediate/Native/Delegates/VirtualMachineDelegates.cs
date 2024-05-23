namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate Int32 GetVersionDelegate(JEnvironmentRef env);

internal delegate JResult RegisterNativesDelegate(JEnvironmentRef env, JClassLocalRef jClass,
	ReadOnlyValPtr<NativeMethodValue> methods0, Int32 nMethods);

internal delegate JResult UnregisterNativesDelegate(JEnvironmentRef env, JClassLocalRef jClass);
internal delegate JResult MonitorEnterDelegate(JEnvironmentRef env, JObjectLocalRef jClass);
internal delegate JResult MonitorExitDelegate(JEnvironmentRef env, JObjectLocalRef jClass);
internal delegate JResult GetVirtualMachineDelegate(JEnvironmentRef env, out JVirtualMachineRef jvm);
internal delegate Byte IsVirtualThreadDelegate(JEnvironmentRef env, JObjectLocalRef obj);