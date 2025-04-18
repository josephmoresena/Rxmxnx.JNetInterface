using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Util.Concurrent;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal sealed class CloseCountDownState(JWindowObject window, JCountDownLatchObject countDownLatch)
	: JNativeCallback.AwtEventListenerState
{
	private readonly JGlobalBase _countDownLatch = JNativeCallback.CallbackState.UseGlobal(countDownLatch.Global);
	private readonly Object _lock = new();
	private readonly JGlobalBase _window = JNativeCallback.CallbackState.UseGlobal(window.Global);

	public override void EventDispatched(JAwtEventObject awtEvent)
	{
		lock (this._lock)
		{
			IEnvironment env = awtEvent.Environment;
			if (awtEvent.GetId() is not JAwtEventObject.EventId.Closed) return;
			if (awtEvent.GetSource() is not { } localWindow) return;
			if (!env.IsSameObject(localWindow, this._window)) return;

			using JCountDownLatchObject countDown = this._countDownLatch.AsLocal<JCountDownLatchObject>(env);
			countDown.CountDown();
		}
	}
	public void Dispose()
	{
		lock (this._lock)
		{
			if (JNativeCallback.CallbackState.FreeGlobal(this._window))
				this._window.Dispose();
			if (JNativeCallback.CallbackState.FreeGlobal(this._countDownLatch))
				this._countDownLatch.Dispose();
		}
	}
}