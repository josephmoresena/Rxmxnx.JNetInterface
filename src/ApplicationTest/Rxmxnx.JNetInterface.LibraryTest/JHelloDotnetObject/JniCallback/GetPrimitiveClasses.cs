using System.Runtime.CompilerServices;

using Rxmxnx.JNetInterface.Functional;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
public partial class JHelloDotnetObject
{
	private sealed partial class JniCallback
	{
		private readonly struct GetPrimitiveClasses<TManaged> : IFrameFunction<JArrayObject<JClassObject>?>
			where TManaged : IManagedCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			JArrayObject<JClassObject>? IFrameFunction<JArrayObject<JClassObject>?>.Apply(IEnvironment env,
				scoped ReadOnlySpan<JReferenceObject?> objects)
			{
				if (objects.IsEmpty || objects[0] is not JClassObject jClass) return default;
				return TManaged.GetPrimitiveClasses(jClass);
			}

			public static JObjectArrayLocalRef EntryPoint(JEnvironmentRef envRef, JClassLocalRef classRef)
			{
				try
				{
					IVirtualMachine vm = TManaged.TypeVirtualMachine;
					GetPrimitiveClasses<TManaged> func = new();
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
						$"JHelloDotnetObject.JniCallback.GetPrimitiveClasses ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
					throw;
				}
			}
		}
	}
}