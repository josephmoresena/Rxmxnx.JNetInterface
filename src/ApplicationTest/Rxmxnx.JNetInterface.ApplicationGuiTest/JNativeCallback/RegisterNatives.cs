using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface;

public unsafe partial class JNativeCallback
{
	private static void RegisterNatives(IEnvironment env)
	{
		if (JNativeCallback.nativesRegistered) return;

		using JClassObject jClass = JClassObject.GetClass<JNativeCallback>(env);
		delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JLong, JLong, void>
			finalizePtr = &JNativeCallback.Finalize;
		delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JStringLocalRef> getExceptionMessagePtr =
			&JNativeCallback.GetExceptionMessage;
		JNativeCallEntry finalizeEntry = JNativeCallEntry.Create(JNativeCallback.finalizeDef, (IntPtr)finalizePtr);
		JNativeCallEntry getExceptionMessageEntry =
			JNativeCallEntry.Create(JNativeCallback.getExceptionMessageDef, (IntPtr)getExceptionMessagePtr);

		jClass.Register(finalizeEntry, getExceptionMessageEntry);
		JNativeCallback.nativesRegistered = true;
	}

	[UnmanagedCallersOnly]
	private static void Finalize(JEnvironmentRef environmentRef, JClassLocalRef _, JLong lowValue, JLong highValue)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		longKey[0] = lowValue;
		longKey[1] = highValue;
		JNativeCallback.states.Remove(Unsafe.As<JLong, Guid>(ref longKey[0]), out CallbackState? callback);
		if (callback is not IDisposable disposable)
			return;

		JNativeCallAdapter callAdapter;
		try
		{
			callAdapter = JNativeCallAdapter.Create(environmentRef).Build();
		}
		catch (ArgumentException)
		{
			return;
		}

		try
		{
			disposable.Dispose();
		}
		finally
		{
			callAdapter.FinalizeCall();
		}
	}
	[UnmanagedCallersOnly]
	private static JStringLocalRef GetExceptionMessage(JEnvironmentRef environmentRef, JClassLocalRef _)
	{
		JNativeCallAdapter callAdapter;
		try
		{
			callAdapter = JNativeCallAdapter.Create(environmentRef).Build();
		}
		catch (ArgumentException)
		{
			return default;
		}

		JStringObject? jString = default;
		try
		{
			jString = JStringObject.Create(callAdapter.Environment, "Invalid callback class."u8);
		}
		catch (Exception e)
		{
			if (e is JniException)
				callAdapter.Environment.PendingException = default;
		}
		return callAdapter.FinalizeCall(jString);
	}
}