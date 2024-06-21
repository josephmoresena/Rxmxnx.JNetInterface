using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
	private sealed class JniCallback(IManagedCallback managed)
	{
		private JStringLocalRef GetString(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .Build();
			JStringObject? result = managed.GetString(jLocal);
			return callAdapter.FinalizeCall(result);
		}
		private JInt GetInt(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .Build();
			JInt value = managed.GetInt(jLocal);
			return callAdapter.FinalizeCall(value);
		}
		private void PassString(JEnvironmentRef envRef, JObjectLocalRef localRef, JStringLocalRef stringRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .WithParameter(stringRef, out JStringObject? jString).Build();
			managed.PassString(jLocal, jString);
			callAdapter.FinalizeCall();
		}

		public static void RegisterNativeMethods<TManaged>(JClassObject helloDotnetClass, TManaged managed)
			where TManaged : IManagedCallback
		{
			JniCallback jniCallback = new(managed);
			helloDotnetClass.Register(new List<JNativeCallEntry>
			{
				JNativeCallEntry.Create<GetStringDelegate>(
					new JFunctionDefinition<JStringObject>("getNativeString"u8),
					jniCallback.GetString),
				JNativeCallEntry.Create<GetIntDelegate>(
					new JFunctionDefinition<JInt>("getNativeInt"u8), jniCallback.GetInt),
				JNativeCallEntry.Create<PassStringDelegate>(
					new StringConsumerDefinition("passNativeString"u8),
					jniCallback.PassString),
			});
		}
	}
}