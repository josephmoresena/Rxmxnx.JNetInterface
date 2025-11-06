using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Util.Concurrent;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal sealed class CloseCountDownState(JWindowObject window, JCountDownLatchObject? countDownLatch)
	: AwtEventListenerState
{
	private readonly JGlobalBase? _countDownLatch = CallbackState.UseGlobal(countDownLatch?.Global);
#if NET9_0_OR_GREATER
	private readonly Lock _lock = new();
#else
	private readonly Object _lock = new();
#endif
	private readonly JGlobalBase _window = CallbackState.UseGlobal(window.Global);

	public override void EventDispatched(JAwtEventObject awtEvent)
	{
		IEnvironment env = awtEvent.Environment;
#if NET9_0_OR_GREATER
		using (this._lock.EnterScope())
#else
		lock (this._lock)
#endif
		{
			try
			{
				if (awtEvent.GetId() is not JAwtEventObject.EventId.Closed) return;
				if (awtEvent.GetSource() is not { } localWindow) return;
				if (!env.IsSameObject(localWindow, this._window)) return;

				using JCountDownLatchObject? countDown = this._countDownLatch?.AsLocal<JCountDownLatchObject>(env);
				countDown?.CountDown();
			}
			catch (Exception ex)
			{
				UIAdapter.Instance.ShowError(ex);
				if (ex is JniException) env.PendingException = default;
			}
		}
	}
	public void Dispose()
	{
#if NET9_0_OR_GREATER
		using (this._lock.EnterScope())
#else
		lock (this._lock)
#endif
		{
			if (CallbackState.FreeGlobal(this._window))
				this._window.Dispose();
			if (CallbackState.FreeGlobal(this._countDownLatch))
				this._countDownLatch.Dispose();
		}
	}
}