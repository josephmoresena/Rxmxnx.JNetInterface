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
		private readonly struct SumArray<TManaged> : IFrameFunction<JIntegerObject?> where TManaged : IManagedCallback
		{
			JIntegerObject? IFrameFunction<JIntegerObject?>.Apply(IEnvironment env,
				scoped ReadOnlySpan<JReferenceObject?> objects)
			{
				if (objects.Length < 2 || objects[0] is not JClassObject jClass ||
				    objects[1] is not JArrayObject<JInt> jIntArray) return default;
				return TManaged.SumArray(jClass, jIntArray);
			}

			public static JObjectLocalRef EntryPoint(JEnvironmentRef envRef, JClassLocalRef classRef,
				JIntArrayLocalRef intArrayRef)
			{
				try
				{
					IVirtualMachine vm = TManaged.TypeVirtualMachine;
					SumArray<TManaged> func = new();
#if !NET9_0_OR_GREATER
					func.WithNativeFrame(vm, envRef, out JObjectLocalRef result, [classRef, intArrayRef,]);
#else
					func.WithNativeFrame(vm, envRef, out JObjectLocalRef result, classRef, intArrayRef);
#endif
					return result;
				}
				catch (Exception ex)
				{
					TManaged.TypeWriter.WriteLine(
						$"JHelloDotnetObject.JniCallback.SumArray ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
					throw;
				}
			}
		}
	}
}