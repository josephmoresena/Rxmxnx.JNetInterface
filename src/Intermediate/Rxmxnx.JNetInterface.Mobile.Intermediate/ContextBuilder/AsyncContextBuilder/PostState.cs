namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Post(SendOrPostCallback, Object)"/>
	/// </summary>
	private sealed class PostState(AsyncState state, TaskCompletionSource source)
	{
		/// <summary>
		/// Internal <see cref="TaskCompletionSource"/> instance.
		/// </summary>
		private readonly TaskCompletionSource _source = source;
		/// <summary>
		/// Internal <see cref="AsyncState"/> instance.
		/// </summary>
		private readonly AsyncState _state = state;

		/// <summary>
		/// Action delegate for <see cref="SynchronizationContext.Post(SendOrPostCallback, Object)"/>.
		/// </summary>
		/// <param name="state">Action state object.</param>
		public static void Post(Object? state)
		{
			if (state is not PostState postState) return;

			IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
			INativeThread env = host.GetNativeThread();
			try
			{
				postState._state.Invoke(env);
				postState._source.SetResult();
			}
			catch (Exception e)
			{
				postState._source.SetException(e);
			}
			finally
			{
				postState._state.Dispose();
			}
		}
	}

	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Post(SendOrPostCallback, Object)"/>
	/// </summary>
	private sealed class PostState<TResult>(AsyncState<TResult> state, TaskCompletionSource<TResult> source)
	{
		/// <summary>
		/// Internal <see cref="TaskCompletionSource"/> instance.
		/// </summary>
		private readonly TaskCompletionSource<TResult> _source = source;
		/// <summary>
		/// Internal <see cref="AsyncState{TResult}"/> instance.
		/// </summary>
		private readonly AsyncState<TResult> _state = state;

		/// <summary>
		/// Action delegate for <see cref="SynchronizationContext.Post(SendOrPostCallback, Object)"/>.
		/// </summary>
		/// <param name="state">Action state object.</param>
		public static void Post(Object? state)
		{
			if (state is not PostState<TResult> postState) return;

			IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
			INativeThread env = host.GetNativeThread();
			try
			{
				postState._state.Invoke(env);
				postState._source.SetResult(postState._state.Result!);
			}
			catch (Exception e)
			{
				postState._source.SetException(e);
			}
			finally
			{
				postState._state.Dispose();
			}
		}
	}
}