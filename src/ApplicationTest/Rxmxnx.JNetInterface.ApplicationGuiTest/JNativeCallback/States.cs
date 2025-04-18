using System.Collections.Concurrent;
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
		private static readonly ConcurrentDictionary<IntPtr, Int32> counter = new();

		[return: NotNullIfNotNull(nameof(jGlobal))]
		protected static TGlobal? UseGlobal<TGlobal>(TGlobal? jGlobal) where TGlobal : JGlobalBase
		{
			IntPtr nintRef = JGlobalBase.GetReference(jGlobal);
			if (nintRef == default) return jGlobal;

			CallbackState.counter.TryAdd(nintRef, 0);
			CallbackState.counter[nintRef]++;
			return jGlobal;
		}
		protected static Boolean FreeGlobal(JGlobalBase? jGlobal)
		{
			IntPtr nintRef = JGlobalBase.GetReference(jGlobal);
			if (nintRef == default) return false;

			Boolean result = !CallbackState.counter.TryGetValue(nintRef, out Int32 value) || value - 1 <= 0;
			if (!result)
				CallbackState.counter[nintRef]--;
			else
				CallbackState.counter.TryRemove(nintRef, out _);
			return result;
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