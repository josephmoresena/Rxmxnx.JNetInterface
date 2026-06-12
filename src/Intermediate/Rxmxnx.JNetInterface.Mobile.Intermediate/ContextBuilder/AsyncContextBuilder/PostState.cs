namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Post(SendOrPostCallback, Object)"/>
	/// </summary>
	private sealed class PostState(TaskState state, TaskCompletionSource source)
	{
		/// <summary>
		/// Internal <see cref="TaskCompletionSource"/> instance.
		/// </summary>
		private readonly TaskCompletionSource _source = source;
		/// <summary>
		/// Internal <see cref="TaskState"/> instance.
		/// </summary>
		private readonly TaskState _state = state;

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
		}
	}

	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Post(SendOrPostCallback, Object)"/>
	/// </summary>
	private sealed class PostState<TResult>(TaskState state, TaskCompletionSource<TResult> source)
	{
		/// <summary>
		/// Internal <see cref="TaskCompletionSource"/> instance.
		/// </summary>
		private readonly TaskCompletionSource<TResult> _source = source;
		/// <summary>
		/// Internal <see cref="TaskState"/> instance.
		/// </summary>
		private readonly TaskState _state = state;

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
				TResult result = postState._state.Invoke<TResult>(env);
				postState._source.SetResult(result);
			}
			catch (Exception e)
			{
				postState._source.SetException(e);
			}
		}
	}

	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Post(SendOrPostCallback, Object)"/>
	/// </summary>
	private sealed class PostJniState<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		TaskState state,
		TaskCompletionSource<TResult> source) where TResult : class, IJavaPeerable
	{
		/// <summary>
		/// Internal <see cref="TaskCompletionSource"/> instance.
		/// </summary>
		private readonly TaskCompletionSource<TResult> _source = source;
		/// <summary>
		/// Internal <see cref="TaskState"/> instance.
		/// </summary>
		private readonly TaskState _state = state;

		/// <summary>
		/// Action delegate for <see cref="SynchronizationContext.Post(SendOrPostCallback, Object)"/>.
		/// </summary>
		/// <param name="state">Action state object.</param>
		public static void Post(Object? state)
		{
			if (state is not PostJniState<TResult> postState) return;

			IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
			INativeThread env = host.GetNativeThread();
			try
			{
				TResult result = postState._state.InvokeJni<TResult>(env);
				postState._source.SetResult(result);
			}
			catch (Exception e)
			{
				postState._source.SetException(e);
			}
		}
	}
}