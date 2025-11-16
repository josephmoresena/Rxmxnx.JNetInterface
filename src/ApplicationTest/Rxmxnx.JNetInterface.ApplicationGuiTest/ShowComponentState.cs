using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal sealed class ShowComponentState(JComponentObject component, IWrapper<Boolean> setVisible)
	: RunnableState, IDisposable
{
	private readonly JGlobalBase _component = CallbackState.UseGlobal(component.Weak);
#if NET9_0_OR_GREATER
	private readonly Lock _lock = new();
#else
	private readonly Object _lock = new();
#endif

	public void Dispose()
	{
#if NET9_0_OR_GREATER
		using (this._lock.EnterScope())
#else
		lock (this._lock)
#endif
		{
			if (CallbackState.FreeGlobal(this._component))
				this._component.Dispose();
		}
	}

	public override void Run(IEnvironment env)
	{
#if NET9_0_OR_GREATER
		using (this._lock.EnterScope())
#else
		lock (this._lock)
#endif
		{
			UIAdapter.Instance.PrintThreadInfo(env.Reference);
			if (!this._component.IsValid(env)) return;
			try
			{
				using JComponentObject localComponent = this._component.AsLocal<JComponentObject>(env);
				localComponent.SetVisible(setVisible.Value);
			}
			catch (Exception ex)
			{
				UIAdapter.Instance.ShowError(ex);
				if (ex is JniException) env.PendingException = default;
			}
		}
	}
}