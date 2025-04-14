using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface;

public unsafe partial class JNativeCallback
{
	private static void RegisterFinalize(IEnvironment env)
	{
		if (JNativeCallback.finalizeRegistered) return;

		using JClassObject jClass = JClassObject.GetClass<JNativeCallback>(env);
		delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JLong, JLong, void>
			ptr = &JNativeCallback.RegisterFinalize;
		JNativeCallEntry entry = JNativeCallEntry.Create(JNativeCallback.finalizeDef, (IntPtr)ptr);

		jClass.Register(entry);
		JNativeCallback.finalizeRegistered = true;
	}

	[UnmanagedCallersOnly]
	private static void RegisterFinalize(JEnvironmentRef _, JClassLocalRef __, JLong lowValue, JLong highValue)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		longKey[0] = lowValue;
		longKey[1] = highValue;
		JNativeCallback.states.Remove(Unsafe.As<JLong, Guid>(ref longKey[0]), out CallbackState? ___);
	}
}