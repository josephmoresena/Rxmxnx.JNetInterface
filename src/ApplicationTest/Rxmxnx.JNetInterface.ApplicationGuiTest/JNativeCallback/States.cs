using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface;

public partial class JNativeCallback
{
	public abstract class CallbackState;

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
}