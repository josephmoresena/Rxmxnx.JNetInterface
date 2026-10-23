using Rxmxnx.JNetInterface.Functional;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
public partial class JHelloDotnetObject
{
	private sealed partial class JniCallback : IFrameFunction<JStringObject?>
	{
		JStringObject? IFrameFunction<JStringObject?>.Apply(IEnvironment env,
			scoped ReadOnlySpan<JReferenceObject?> objects)
		{
			if (objects.IsEmpty || objects[0] is not JLocalObject jLocal) return default;
			return managed.GetHelloString(jLocal);
		}

		private JStringLocalRef GetHelloString(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			try
			{
#if !NET9_0_OR_GREATER
				managed.VirtualMachine.WithNativeFrame(envRef, this, out JStringLocalRef result, [localRef,]);
#else
				managed.VirtualMachine.WithNativeFrame(envRef, this, out JStringLocalRef result, localRef);
#endif
				return result;
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.GetHelloString ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
		}
	}
}