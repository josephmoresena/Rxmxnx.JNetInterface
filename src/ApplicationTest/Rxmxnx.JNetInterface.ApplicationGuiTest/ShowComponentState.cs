using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal sealed class ShowComponentState(JComponentObject component, IWrapper<Boolean> setVisible)
	: RunnableState, IDisposable
{
	private readonly JGlobalBase _component = CallbackState.UseGlobal(component.Weak);
	private readonly Object _lock = new();
	public void Dispose()
	{
		lock (this._lock)
		{
			if (CallbackState.FreeGlobal(this._component))
				this._component.Dispose();
		}
	}

	public override void Run(IEnvironment env)
	{
		lock (this._lock)
		{
			UIAdapter.Instance.PrintThreadInfo(env.Reference);
			if (!this._component.IsValid(env)) return;
			using JComponentObject localComponent = this._component.AsLocal<JComponentObject>(env);
			localComponent.SetVisible(setVisible.Value);
		}
	}
}