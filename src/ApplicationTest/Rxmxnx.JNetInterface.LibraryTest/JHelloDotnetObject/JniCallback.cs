using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JHelloDotnetObject
{
	private sealed class JniCallback(IManagedCallback managed)
	{
		static JniCallback() { JVirtualMachine.Register<JNullPointerExceptionObject>(); }

		private JStringLocalRef GetHelloString(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(managed.VirtualMachine, envRef, localRef,
				                                         out JLocalObject jLocal).Build();
				JStringObject? result = managed.GetHelloString(jLocal);
				return callAdapter.FinalizeCall(result);
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.GetHelloString ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
		private JInt GetThreadId(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(managed.VirtualMachine, envRef, localRef,
				                                         out JLocalObject jLocal).Build();
				JInt value = managed.GetThreadId(jLocal);
				return callAdapter.FinalizeCall(value);
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.GetThreadId ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
		private void PrintRuntimeInformation(JEnvironmentRef envRef, JObjectLocalRef localRef,
			JStringLocalRef stringRef)
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(managed.VirtualMachine, envRef, localRef,
				                                         out JLocalObject jLocal)
				                                 .WithParameter(stringRef, out JStringObject? jString).Build();
				managed.PrintRuntimeInformation(jLocal, jString);
				callAdapter.FinalizeCall();
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.PrintRuntimeInformation ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
		private void FieldAccess(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(managed.VirtualMachine, envRef, localRef,
				                                         out JLocalObject jLocal).Build();
				managed.ProcessField(jLocal);
				callAdapter.FinalizeCall();
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.FieldAccess ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
		private void Throw(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(managed.VirtualMachine, envRef, localRef,
				                                         out JLocalObject jLocal).Build();
				managed.Throw(jLocal);
				callAdapter.FinalizeCall();
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.FieldAccess ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}

		public static void RegisterNativeMethods<TManaged>(JClassObject helloDotnetClass, TManaged managed)
			where TManaged : IManagedCallback
		{
			JniCallback jniCallback = new(managed);
			TManaged.TypeVirtualMachine = managed.VirtualMachine;
			TManaged.TypeWriter = managed.Writer;
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
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
				                                         out JClassObject jClass)
				                                 .WithParameter(intArrayRef, out JArrayObject<JInt>? jArray).Build();
				JIntegerObject? result = TManaged.SumArray(jClass, jArray);
				return callAdapter.FinalizeCall(result);
			}
			catch (Exception ex)
			{
				TManaged.TypeWriter.WriteLine(
					$"JHelloDotnetObject.JniCallback.SumArray ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
		private static JArrayLocalRef GetIntArrayArray<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef,
			Int32 length) where TManaged : IManagedCallback
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
				                                         out JClassObject jClass).Build();
				JArrayObject<JArrayObject<JInt>>? result = TManaged.GetIntArrayArray(jClass, length);
				return callAdapter.FinalizeCall(result).ArrayValue;
			}
			catch (Exception ex)
			{
				TManaged.TypeWriter.WriteLine(
					$"JHelloDotnetObject.JniCallback.GetIntArrayArray ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
		private static void PrintClass<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef)
			where TManaged : IManagedCallback
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
				                                         out JClassObject jClass).Build();
				TManaged.PrintClass(jClass, TManaged.TypeWriter);
				callAdapter.FinalizeCall();
			}
			catch (Exception ex)
			{
				TManaged.TypeWriter.WriteLine(
					$"JHelloDotnetObject.JniCallback.PrintClass ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
		private static JArrayLocalRef GetPrimitiveClasses<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef)
			where TManaged : IManagedCallback
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
				                                         out JClassObject jClass).Build();
				JArrayObject<JClassObject> mainClasses = TManaged.GetPrimitiveClasses(jClass);
				return callAdapter.FinalizeCall(mainClasses).ArrayValue;
			}
			catch (Exception ex)
			{
				TManaged.TypeWriter.WriteLine(
					$"JHelloDotnetObject.JniCallback.GetPrimitiveClasses ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
		private static JClassLocalRef GetVoidClass<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef)
			where TManaged : IManagedCallback
		{
			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter
				                                 .Create(TManaged.TypeVirtualMachine, envRef, classRef,
				                                         out JClassObject jClass).Build();
				JClassObject voidClass = TManaged.GetVoidClass(jClass);
				return callAdapter.FinalizeCall(voidClass);
			}
			catch (Exception ex)
			{
				TManaged.TypeWriter.WriteLine(
					$"JHelloDotnetObject.JniCallback.GetVoidClass ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
	}
}