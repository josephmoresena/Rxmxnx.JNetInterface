using Rxmxnx.JNetInterface.Functional;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
public partial class JHelloDotnetObject
{
	private sealed partial class JniCallback
	{
		private readonly struct GetIntArrayArray<TManaged>(Int32 length)
			: IFrameFunction<JArrayObject<JArrayObject<JInt>>?> where TManaged : IManagedCallback
		{
			JArrayObject<JArrayObject<JInt>>? IFrameFunction<JArrayObject<JArrayObject<JInt>>?>.Apply(IEnvironment env,
				scoped ReadOnlySpan<JReferenceObject?> objects)
			{
				if (objects.IsEmpty || objects[0] is not JClassObject jClass) return default;
				return TManaged.GetIntArrayArray(jClass, length);
			}

			public static JObjectArrayLocalRef EntryPoint(JEnvironmentRef envRef, JClassLocalRef classRef, Int32 length)
			{
				try
				{
					IVirtualMachine vm = TManaged.TypeVirtualMachine;
					GetIntArrayArray<TManaged> func = new(length);
#if !NET9_0_OR_GREATER
					func.WithNativeFrame(vm, envRef, out JObjectArrayLocalRef result, [classRef,]);
#else
					func.WithNativeFrame(vm, envRef, out JObjectArrayLocalRef result, classRef);
#endif
					return result;
				}
				catch (Exception ex)
				{
					TManaged.TypeWriter.WriteLine(
						$"JHelloDotnetObject.JniCallback.GetIntArrayArray ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
					throw;
				}
			}
		}
	}
}