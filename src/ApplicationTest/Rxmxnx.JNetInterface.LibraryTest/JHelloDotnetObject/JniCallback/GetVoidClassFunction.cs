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
		private readonly struct GetVoidClass<TManaged> : IFrameFunction<JClassObject?> where TManaged : IManagedCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			JClassObject? IFrameFunction<JClassObject?>.Apply(IEnvironment env,
				scoped ReadOnlySpan<JReferenceObject?> objects)
			{
				if (objects.IsEmpty || objects[0] is not JClassObject jClass) return default;
				return TManaged.GetVoidClass(jClass);
			}

			public static JClassLocalRef EntryPoint(JEnvironmentRef envRef, JClassLocalRef classRef)
			{
				try
				{
					GetVoidClass<TManaged> func = new();
#if !NET9_0_OR_GREATER
					func.WithNativeFrame(TManaged.TypeVirtualMachine, envRef, out JClassLocalRef result, [classRef,]);
#else
					func.WithNativeFrame(TManaged.TypeVirtualMachine, envRef, out JClassLocalRef result, classRef);
#endif
					return result;
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
}