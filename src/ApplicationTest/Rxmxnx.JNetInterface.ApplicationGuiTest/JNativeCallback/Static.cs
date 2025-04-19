using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Rxmxnx.JNetInterface.ApplicationTest;
using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface;

public partial class JNativeCallback
{
	public static JRunnableObject CreateRunnable(IEnvironment env, RunnableState state)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		JNativeCallback.CreateKey(env, longKey);
		Runnable.RegisterRun(env);

		return JNativeCallback.Create<Runnable, JRunnableObject>(env, longKey, state);
	}
	public static JActionListenerObject CreateActionListener(IEnvironment env, ActionListenerState state)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		JNativeCallback.CreateKey(env, longKey);
		ActionListener.RegisterActionPerformed(env);

		return JNativeCallback.Create<ActionListener, JActionListenerObject>(env, longKey, state);
	}
	public static JAwtEventListenerObject CreateAwtEventListener(IEnvironment env, AwtEventListenerState state)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		JNativeCallback.CreateKey(env, longKey);
		AwtEventListener.RegisterEventDispatched(env);

		return JNativeCallback.Create<AwtEventListener, JAwtEventListenerObject>(env, longKey, state);
	}

	private static void CreateKey(IEnvironment env, Span<JLong> longKey)
	{
		ref Guid guidKey = ref Unsafe.As<JLong, Guid>(ref longKey[0]);
#if NET9_0_OR_GREATER
		guidKey = Guid.CreateVersion7();
#else
		guidKey = Guid.NewGuid();
#endif
		JNativeCallback.RegisterNatives(env);
	}
	private static TInterface
		Create<TObject, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>(
			IEnvironment env, ReadOnlySpan<JLong> longKey, CallbackState state)
		where TObject : JNativeCallback, IInterfaceObject<TInterface>, IClassType<TObject>
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
	{
		Boolean createState = true;
		try
		{
			return JNativeCallback.constructorDef.NewCall<TObject>(env, [longKey[0], longKey[1],]).CastTo<TInterface>();
		}
		catch (Exception)
		{
			createState = false;
			throw;
		}
		finally
		{
			if (createState)
				JNativeCallback.states.TryAdd(longKey.AsValues<JLong, Guid>()[0], state);
		}
	}
}