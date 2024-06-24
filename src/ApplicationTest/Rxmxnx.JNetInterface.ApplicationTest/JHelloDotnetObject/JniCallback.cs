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
		static JniCallback()
		{
			JVirtualMachine.Register<JNullPointerExceptionObject>();
			JVirtualMachine.Register<JHelloDotnetObject>();
		}

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
		private void AccessStringField(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .Build();
			managed.AccessStringField(jLocal);
			callAdapter.FinalizeCall();
		}
		private void Throw(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .Build();
			managed.Throw(jLocal);
			callAdapter.FinalizeCall();
		}

		public static void RegisterNativeMethods<TManaged>(JClassObject helloDotnetClass, TManaged managed)
			where TManaged : IManagedCallback
		{
			JniCallback jniCallback = new(managed);
			helloDotnetClass.Register(new List<JNativeCallEntry>
			{
				JNativeCallEntry.Create<GetStringDelegate>(
					new JFunctionDefinition<JStringObject>.Parameterless("getNativeString"u8), jniCallback.GetString),
				JNativeCallEntry.Create<GetIntDelegate>(new JFunctionDefinition<JInt>.Parameterless("getNativeInt"u8),
				                                        jniCallback.GetInt),
				JNativeCallEntry.Create<PassStringDelegate>(new StringConsumerDefinition("passNativeString"u8),
				                                            jniCallback.PassString),
				JNativeCallEntry.CreateParameterless(new("accessStringField"u8), jniCallback.AccessStringField),
				JNativeCallEntry.CreateParameterless(new("throwNative"u8), jniCallback.Throw),
				// Static
				JNativeCallEntry.Create<SumArrayDelegate>(
					new PrimitiveSumArrayDefinition<JIntegerObject, JInt>("sumArray"u8),
					JniCallback.SumArray<TManaged>),
				JNativeCallEntry.Create<GetIntArrayArrayDelegate>(
					new InitPrimitiveArrayArrayDefinition<JInt>("getIntArrayArray"u8),
					JniCallback.GetIntArrayArray<TManaged>),
				JNativeCallEntry.CreateParameterless(new("printClass"u8), JniCallback.PrintClass<TManaged>),
			});
		}

		private static JObjectLocalRef SumArray<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef,
			JIntArrayLocalRef intArrayRef) where TManaged : IManagedCallback
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(envRef, classRef, out JClassObject jClass)
			                                                   .WithParameter(
				                                                   intArrayRef, out JArrayObject<JInt>? jArray).Build();
			JIntegerObject? result = TManaged.SumArray(jClass, jArray);
			return callAdapter.FinalizeCall(result);
		}
		private static JArrayLocalRef GetIntArrayArray<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef,
			Int32 length) where TManaged : IManagedCallback
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(envRef, classRef, out JClassObject jClass)
			                                                   .Build();
			JArrayObject<JArrayObject<JInt>>? result = TManaged.GetIntArrayArray(jClass, length);
			return callAdapter.FinalizeCall(result);
		}
		private static void PrintClass<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef)
			where TManaged : IManagedCallback
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(envRef, classRef, out JClassObject jClass)
			                                                   .Build();
			TManaged.PrintClass(jClass);
			callAdapter.FinalizeCall();
		}
	}
}