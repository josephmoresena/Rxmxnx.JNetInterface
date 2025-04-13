using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JNativeCallback
{
	private unsafe partial class RunnableCallable
	{
		private static readonly JMethodDefinition runDefinition = (JMethodDefinition)IndeterminateCall
			.CreateMethodDefinition("run"u8, [
				JArgumentMetadata.Get<JLong>(),
				JArgumentMetadata.Get<JLong>(),
			]).Definition;
		private static Boolean runRegistered;

		public static void RegisterRun(IEnvironment env)
		{
			if (RunnableCallable.runRegistered) return;

			using JClassObject jClass = JClassObject.GetClass<RunnableCallable>(env);
			delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JLong, JLong, void>
				ptr = &RunnableCallable.RegisterRun;
			JNativeCallEntry entry = JNativeCallEntry.Create(RunnableCallable.runDefinition, (IntPtr)ptr);

			jClass.Register(entry);
			RunnableCallable.runRegistered = true;
		}

		[UnmanagedCallersOnly]
		private static void RegisterRun(JEnvironmentRef _, JClassLocalRef __, JLong lowValue, JLong highValue)
		{
			Span<JLong> longKey = stackalloc JLong[2];
			longKey[0] = lowValue;
			longKey[1] = highValue;
			if (JNativeCallback.states.TryGetValue(Unsafe.As<JLong, Guid>(ref longKey[0]), out CallbackState? state) &&
			    state is IWrapper.IBase<RunnableState> ws)
				ws.Value.Run();
		}
	}
}