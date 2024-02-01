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
		result.Register(new List<JNativeCall>
		{
			JNativeCall.Create<GetStringDelegate>(
				new JFunctionDefinition<JStringObject>("getNativeString"u8),
				JHelloDotnetObject.GetString),
			JNativeCall.Create<GetIntDelegate>(new JFunctionDefinition<JInt>("getNativeInt"u8),
			                                   JHelloDotnetObject.GetInt),
			JNativeCall.Create<PassStringDelegate>(
				new StringConsumerDefinition("passNativeString"u8), JHelloDotnetObject.PassString),
		});
		return result;
	}

	private static JStringLocalRef GetString(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		JniCall jniCall = JniCall.Create(envRef, localRef, out _).Build();
		String? text = JHelloDotnetObject.GetStringEvent?.Invoke();
		JStringObject? result = JStringObject.Create(jniCall.Environment, text);
		return jniCall.FinalizeCall(result);
	}
	private static JInt GetInt(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		JniCall jniCall = JniCall.Create(envRef, localRef, out _).Build();
		Int32 integer = JHelloDotnetObject.GetIntegerEvent?.Invoke() ?? default;
		return jniCall.FinalizeCall<JInt>(integer);
	}
	private static void PassString(JEnvironmentRef envRef, JObjectLocalRef localRef, JStringLocalRef stringRef)
	{
		JniCall jniCall = JniCall.Create(envRef, localRef, out JLocalObject jLocal)
		                         .WithParameter(stringRef, out JStringObject jString).Build();
		JHelloDotnetObject.PassStringEvent?.Invoke(jString.Value);
		jniCall.FinalizeCall();
	}
	private delegate JStringLocalRef GetStringDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);
	private delegate JInt GetIntDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);

	private delegate void PassStringDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JStringLocalRef stringRef);
}