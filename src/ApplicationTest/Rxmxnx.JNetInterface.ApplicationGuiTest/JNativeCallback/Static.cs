using System.Runtime.CompilerServices;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JNativeCallback
{
	public static JRunnableObject CreateRunnable(IEnvironment env, RunnableState state)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		ref Guid guidKey = ref Unsafe.As<JLong, Guid>(ref longKey[0]);
		guidKey = Guid.CreateVersion7();
		JNativeCallback.RegisterFinalize(env);
		RunnableCallable.RegisterRun(env);

		Boolean createState = true;
		try
		{
			return JNativeCallback.constructorDefinition.NewCall<RunnableCallable>(env, [longKey[0], longKey[1],])
			                      .CastTo<JRunnableObject>();
		}
		catch (Exception)
		{
			createState = false;
			throw;
		}
		finally
		{
			if (createState)
				JNativeCallback.states.TryAdd(guidKey, state);
		}
	}
}