using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
	public static JClassObject LoadClass(IEnvironment env, Byte[] classByteCode)
	{
		JClassObject result = JClassObject.LoadClass<JHelloDotnetObject>(env, classByteCode);
		result.Register(new List<JNativeCallEntry>
		{
			JNativeCallEntry.Create<GetStringDelegate>(
				new JFunctionDefinition<JStringObject>("getNativeString"u8),
				JHelloDotnetObject.GetString),
			JNativeCallEntry.Create<GetIntDelegate>(new JFunctionDefinition<JInt>("getNativeInt"u8),
			                                        JHelloDotnetObject.GetInt),
			JNativeCallEntry.Create<PassStringDelegate>(
				new StringConsumerDefinition("passNativeString"u8), JHelloDotnetObject.PassString),
		});
		return result;
	}

	private static JStringLocalRef GetString(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(envRef, localRef, out _).Build();
		String? text = JHelloDotnetObject.GetStringEvent?.Invoke();
		JStringObject? result = JStringObject.Create(callAdapter.Environment, text);
		return callAdapter.FinalizeCall(result);
	}
	private static JInt GetInt(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(envRef, localRef, out _).Build();
		Int32 integer = JHelloDotnetObject.GetIntegerEvent?.Invoke() ?? default;
		return callAdapter.FinalizeCall<JInt>(integer);
	}
	private static void PassString(JEnvironmentRef envRef, JObjectLocalRef localRef, JStringLocalRef stringRef)
	{
		JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(envRef, localRef, out JLocalObject jLocal)
		                                                   .WithParameter(stringRef, out JStringObject jString).Build();
		JHelloDotnetObject.PassStringEvent?.Invoke(jString.Value);
		callAdapter.FinalizeCall();
	}
	private delegate JStringLocalRef GetStringDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);
	private delegate JInt GetIntDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);

	private delegate void PassStringDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JStringLocalRef stringRef);
}