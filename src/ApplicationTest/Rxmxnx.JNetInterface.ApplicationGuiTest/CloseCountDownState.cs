using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Util.Concurrent;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal sealed class CloseCountDownState(JWindowObject window, JCountDownLatchObject countDownLatch)
	: JNativeCallback.AwtEventListenerState
{
	private readonly JWeak _countDownLatch = countDownLatch.Weak;
	private readonly JWeak _window = window.Weak;

	public override void EventDispatched(JAwtEventObject awtEvent)
	{
		IEnvironment env = awtEvent.Environment;
		if (awtEvent.GetId() is not JAwtEventObject.EventId.Closed || !env.IsSameObject(awtEvent, this._window)) return;

		using JCountDownLatchObject countDown = this._countDownLatch.AsLocal<JCountDownLatchObject>(env);
		countDown.CountDown();
	}
	public void Dispose()
	{
		this._window.Dispose();
		this._countDownLatch.Dispose();
	}
}