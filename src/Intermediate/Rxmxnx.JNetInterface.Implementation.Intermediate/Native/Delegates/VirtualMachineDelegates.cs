namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate Int32 GetVersionDelegate(JEnvironmentRef env);

internal delegate JResult RegisterNativesDelegate(JEnvironmentRef env, JClassLocalRef jClass,
	ReadOnlyValPtr<JNativeMethodValue> methods0);

internal delegate JResult UnregisterNativesDelegate(JEnvironmentRef env, JClassLocalRef jClass);
internal delegate JResult MonitorEnterDelegate(JEnvironmentRef env, JObjectLocalRef jClass);
internal delegate JResult MonitorExitDelegate(JEnvironmentRef env, JObjectLocalRef jClass);
internal delegate JResult GetVirtualMachineDelegate(JEnvironmentRef env, out JVirtualMachineRef jvm);
internal delegate JResult DestroyVirtualMachineDelegate(JVirtualMachineRef vm);

internal delegate JResult AttachCurrentThreadDelegate(JVirtualMachineRef vm, out JEnvironmentRef env, in JVirtualMachineArgumentValue args);

internal delegate JResult DetachCurrentThreadDelegate(JVirtualMachineRef vm);
internal delegate JResult GetEnvDelegate(JVirtualMachineRef vm, out JEnvironmentRef env, Int32 version);

internal delegate JResult AttachCurrentThreadAsDaemonDelegate(JVirtualMachineRef vm, out JEnvironmentRef env,
	in JVirtualMachineArgumentValue args);

internal delegate Byte IsVirtualThreadDelegate(JEnvironmentRef env, JObjectLocalRef obj);
internal delegate JResult GetDefaultVirtualMachineInitArgsDelegate(ref JVirtualMachineInitArgumentValue args);

internal delegate JResult CreateVirtualMachineDelegate(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
	in JVirtualMachineInitArgumentValue args);

internal delegate JResult GetCreatedVirtualMachinesDelegate(ValPtr<JVirtualMachineRef> buffer0, Int32 bufferLength,
	out Int32 totalVms);