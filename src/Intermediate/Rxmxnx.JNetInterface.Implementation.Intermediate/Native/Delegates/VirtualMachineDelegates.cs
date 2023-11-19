namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate Int32 GetVersionDelegate(JEnvironmentRef env);

internal delegate JResult
	RegisterNativesDelegate(JEnvironmentRef env, JClassLocalRef jClass, in JNativeMethod methods0);

internal delegate JResult UnregisterNativesDelegate(JEnvironmentRef env, JClassLocalRef jClass);
internal delegate JResult MonitorEnterDelegate(JEnvironmentRef env, JObjectLocalRef jClass);
internal delegate JResult MonitorExitDelegate(JEnvironmentRef env, JObjectLocalRef jClass);
internal delegate JResult GetVirtualMachineDelegate(JEnvironmentRef env, ref JVirtualMachineRef jvm);
internal delegate JResult DestroyVirtualMachineDelegate(JVirtualMachineRef vm);

internal delegate JResult AttachCurrentThreadDelegate(JVirtualMachineRef vm, out JEnvironmentRef env,
	in JVirtualMachineArgument args0);

internal delegate JResult DetachCurrentThreadDelegate(JVirtualMachineRef vm);
internal delegate JResult GetEnvDelegate(JVirtualMachineRef vm, out JEnvironmentRef env, Int32 version);

internal delegate JResult AttachCurrentThreadAsDaemonDelegate(JVirtualMachineRef vm, out JEnvironmentRef env,
	in JVirtualMachineArgument args0);

internal delegate Byte IsVirtualThread(JEnvironmentRef env, JObjectLocalRef obj);