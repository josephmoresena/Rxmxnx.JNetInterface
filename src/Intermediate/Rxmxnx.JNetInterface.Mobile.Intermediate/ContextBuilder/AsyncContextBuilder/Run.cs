namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI async context and runs <paramref name="action"/>.
	/// </summary>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	/// <param name="task">Output. <paramref name="action"/> invocation operation task.</param>
	public void Run(AndroidJniAction action, out Task task)
	{
		AsyncActionState<AndroidNativeAction> tState =
			new(AndroidJniHost.GetAndroidHost(), this, false, true) { Action = new(action), };
		task = Task.Factory.StartNew(AsyncState.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
	/// <summary>
	/// Creates a new JNI async context and runs <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	/// <param name="task">Output. <paramref name="action"/> invocation operation task.</param>
	public void Run<TState>(TState state, AndroidJniAction<TState> action, out Task task)
	{
		AsyncActionState<AndroidNativeAction<TState>> tState =
			new(AndroidJniHost.GetAndroidHost(), this, false, true) { Action = new(state, action), };
		task = Task.Factory.StartNew(AsyncState.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
	/// <summary>
	/// Creates a new JNI async context and runs <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="action">A <typeparamref name="TAction"/> instance.</param>
	/// <param name="task">Output. <paramref name="action"/> invocation operation task.</param>
	public void Run<TAction>(TAction action, out Task task) where TAction : IAndroidNativeAction
	{
		AsyncActionState<TAction> tState = new(AndroidJniHost.GetAndroidHost(), this, false, true) { Action = action, };
		task = Task.Factory.StartNew(AsyncState.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
}