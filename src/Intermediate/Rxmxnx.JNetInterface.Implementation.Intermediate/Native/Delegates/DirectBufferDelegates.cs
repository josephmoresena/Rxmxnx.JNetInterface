namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef NewDirectByteBufferDelegate(JEnvironmentRef env, IntPtr address, Int64 capacity);
internal delegate IntPtr GetDirectBufferAddressDelegate(JEnvironmentRef env, JObjectLocalRef buffObj);
internal delegate Int64 GetDirectBufferCapacityDelegate(JEnvironmentRef env, JObjectLocalRef buffObj);