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
		static JniCallback() { JVirtualMachine.Register<JNullPointerExceptionObject>(); }

		private JStringLocalRef GetHelloString(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .Build();
			JStringObject? result = managed.GetHelloString(jLocal);
			return callAdapter.FinalizeCall(result);
		}
		private JInt GetThreadId(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .Build();
			JInt value = managed.GetThreadId(jLocal);
			return callAdapter.FinalizeCall(value);
		}
		private void PrintRuntimeInformation(JEnvironmentRef envRef, JObjectLocalRef localRef,
			JStringLocalRef stringRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .WithParameter(stringRef, out JStringObject? jString).Build();
			managed.PrintRuntimeInformation(jLocal, jString);
			callAdapter.FinalizeCall();
		}
		private void FieldAccess(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
			                                 .Build();
			managed.ProcessField(jLocal);
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
			TManaged.TypeVirtualMachine = managed.VirtualMachine;
			helloDotnetClass.Register(new List<JNativeCallEntry>
			{
				JNativeCallEntry.Create<GetStringDelegate>(
					new JFunctionDefinition<JStringObject>.Parameterless("getHelloString"u8),
					jniCallback.GetHelloString),
				JNativeCallEntry.Create<GetThreadIdDelegate>(
					new JFunctionDefinition<JInt>.Parameterless("getThreadId"u8), jniCallback.GetThreadId),
				JNativeCallEntry.Create<PrintRuntimeInformationDelegate>(
					new StringConsumerDefinition("printRuntimeInformation"u8), jniCallback.PrintRuntimeInformation),
				JNativeCallEntry.CreateParameterless(new("nativeFieldAccess"u8), jniCallback.FieldAccess),
				JNativeCallEntry.CreateParameterless(new("nativeThrow"u8), jniCallback.Throw),
				// Static
				JNativeCallEntry.Create<SumArrayDelegate>(
					new PrimitiveSumArrayDefinition<JIntegerObject, JInt>("sumArray"u8),
					JniCallback.SumArray<TManaged>),
				JNativeCallEntry.Create<GetIntArrayArrayDelegate>(
					new InitPrimitiveArrayArrayDefinition<JInt>("getIntArrayArray"u8),
					JniCallback.GetIntArrayArray<TManaged>),
				JNativeCallEntry.CreateParameterless(new("printClass"u8), JniCallback.PrintClass<TManaged>),
				JNativeCallEntry.Create<GetClassArrayDelegate>(
					new JFunctionDefinition<JArrayObject<JClassObject>>.Parameterless("getPrimitiveClasses"u8),
					JniCallback.GetPrimitiveClasses<TManaged>),
				JNativeCallEntry.Create<GetClassDelegate>(
					new JFunctionDefinition<JClassObject>.Parameterless("getVoidClass"u8),
					JniCallback.GetVoidClass<TManaged>),
			});
		}

		private static JObjectLocalRef SumArray<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef,
			JIntArrayLocalRef intArrayRef) where TManaged : IManagedCallback
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
			                                         out JClassObject jClass)
			                                 .WithParameter(intArrayRef, out JArrayObject<JInt>? jArray).Build();
			JIntegerObject? result = TManaged.SumArray(jClass, jArray);
			return callAdapter.FinalizeCall(result);
		}
		private static JArrayLocalRef GetIntArrayArray<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef,
			Int32 length) where TManaged : IManagedCallback
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
			                                         out JClassObject jClass).Build();
			JArrayObject<JArrayObject<JInt>>? result = TManaged.GetIntArrayArray(jClass, length);
			return callAdapter.FinalizeCall(result);
		}
		private static void PrintClass<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef)
			where TManaged : IManagedCallback
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
			                                         out JClassObject jClass).Build();
			TManaged.PrintClass(jClass);
			callAdapter.FinalizeCall();
		}
		private static JArrayLocalRef GetPrimitiveClasses<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef)
			where TManaged : IManagedCallback
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
			                                         out JClassObject jClass).Build();
			JArrayObject<JClassObject> mainClasses = TManaged.GetPrimitiveClasses(jClass);
			return callAdapter.FinalizeCall(mainClasses);
		}
		private static JClassLocalRef GetVoidClass<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef)
			where TManaged : IManagedCallback
		{
			JNativeCallAdapter callAdapter = JNativeCallAdapter
			                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
			                                         out JClassObject jClass).Build();
			JClassObject voidClass = TManaged.GetVoidClass(jClass);
			return callAdapter.FinalizeCall(voidClass);
		}
	}
}