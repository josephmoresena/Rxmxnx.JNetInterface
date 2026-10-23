using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
public partial class JHelloDotnetObject
{
	private sealed partial class JniCallback(IManagedCallback managed)
	{
		static JniCallback() { JVirtualMachine.Register<JNullPointerExceptionObject>(); }

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
					new PrimitiveSumArrayDefinition<JIntegerObject, JInt>("sumArray"u8), SumArray<TManaged>.EntryPoint),
				JNativeCallEntry.Create<GetIntArrayArrayDelegate>(
					new InitPrimitiveArrayArrayDefinition<JInt>("getIntArrayArray"u8),
					GetIntArrayArray<TManaged>.EntryPoint),
				JNativeCallEntry.CreateParameterless(new("printClass"u8), PrintClass<TManaged>.EntryPoint),
				JNativeCallEntry.Create<GetClassArrayDelegate>(
					new JFunctionDefinition<JArrayObject<JClassObject>>.Parameterless("getPrimitiveClasses"u8),
					GetPrimitiveClasses<TManaged>.EntryPoint),
				JNativeCallEntry.Create<GetClassDelegate>(
					new JFunctionDefinition<JClassObject>.Parameterless("getVoidClass"u8),
					GetVoidClass<TManaged>.EntryPoint),
			});
		}
	}
}