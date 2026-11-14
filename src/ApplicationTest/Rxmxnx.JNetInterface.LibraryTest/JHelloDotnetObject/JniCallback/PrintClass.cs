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
		private readonly struct PrintClass<TManaged> : IFrameAction where TManaged : IManagedCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IFrameAction.Accept(IEnvironment env, scoped ReadOnlySpan<JReferenceObject?> objects)
			{
				if (objects.IsEmpty || objects[0] is not JClassObject jClass) return;
				TManaged.PrintClass(jClass, TManaged.TypeWriter);
			}

			public static void EntryPoint(JEnvironmentRef envRef, JClassLocalRef classRef)
			{
				try
				{
					PrintClass<TManaged> action = new();
#if !NET9_0_OR_GREATER
					action.WithNativeFrame(TManaged.TypeVirtualMachine, envRef, [classRef,]);
#else
					action.WithNativeFrame(TManaged.TypeVirtualMachine, envRef, classRef);
#endif
				}
				catch (Exception ex)
				{
					TManaged.TypeWriter.WriteLine(
						$"JHelloDotnetObject.JniCallback.PrintClass ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
					throw;
				}
			}
		}
	}
}