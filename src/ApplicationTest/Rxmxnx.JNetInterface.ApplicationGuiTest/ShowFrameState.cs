using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal sealed class ShowFrameState(JFrameObjectAwt frame, UIAdapter ui, IWrapper<Boolean> setVisible)
	: JNativeCallback.RunnableState
{
	private readonly JWeak _frame = frame.Weak;

	public override void Run(IEnvironment env)
	{
		ui.PrintThreadInfo(env.Reference);
		if (!this._frame.IsValid(env)) return;
		using JFrameObjectAwt localFrame = this._frame.AsLocal<JFrameObjectAwt>(env);
		localFrame.SetVisible(setVisible.Value);
	}
}