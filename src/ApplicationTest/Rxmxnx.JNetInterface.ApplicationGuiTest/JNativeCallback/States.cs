using System.Diagnostics.CodeAnalysis;

using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface;

public partial class JNativeCallback
{
	public abstract class CallbackState
	{
		[return: NotNullIfNotNull(nameof(jLocal))]
		protected static JGlobalBase? GetGlobalForState(JLocalObject? jLocal)
		{
			if (jLocal is null) return default;
			if (OperatingSystem.IsWindows() && jLocal.Environment.Version > 0x00010008)
				return jLocal.Global;
			return jLocal.Weak;
		}
	}

	public abstract class RunnableState : CallbackState, IWrapper<RunnableState>
	{
		RunnableState IWrapper<RunnableState>.Value => this;
		public abstract void Run(IEnvironment env);
	}

	public abstract class ActionListenerState : CallbackState, IWrapper<ActionListenerState>
	{
		ActionListenerState IWrapper<ActionListenerState>.Value => this;
		public abstract void ActionPerformed(JActionEventObject actionEvent);
	}

	public abstract class AwtEventListenerState : CallbackState, IWrapper<AwtEventListenerState>
	{
		AwtEventListenerState IWrapper<AwtEventListenerState>.Value => this;
		public abstract void EventDispatched(JAwtEventObject awtEvent);
	}
}