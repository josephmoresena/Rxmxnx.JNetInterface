using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JNativeCallback
{
	public abstract class CallbackState;

	public abstract class RunnableState : CallbackState, IWrapper<RunnableState>
	{
		RunnableState IWrapper<RunnableState>.Value => this;
		public abstract void Run();
	}
}