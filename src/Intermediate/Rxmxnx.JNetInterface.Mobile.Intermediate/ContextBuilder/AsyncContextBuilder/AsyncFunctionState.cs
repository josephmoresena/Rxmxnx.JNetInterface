namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Represents the state of an asynchronous function, providing mechanisms for tracking its progress, completion,
	/// and potential error handling.
	/// </summary>
#pragma warning disable CS8618
	private sealed class AsyncFunctionState<TResult, TFunction>(
		IVirtualMachineHost host,
		AsyncContextBuilder builder,
		Boolean useWeakGlobal,
		Boolean isDaemon = false)
		: AsyncState<TResult>(host, builder, useWeakGlobal, isDaemon) where TFunction : IAndroidNativeFunction<TResult>
	{
		/// <summary>
		/// Internal function to be executed.
		/// </summary>
		public TFunction Function { get; init; }

		/// <inheritdoc/>
		public override void Invoke(INativeThread env)
		{
			JLocalObject?[] objects = [];
			JavaInteropCache? interopCache = default;
			try
			{
				AndroidJniContext context = this.CreateContext(env, out interopCache, out objects);
				using InlineCache _ = new(env);
				this.Result = this.Function.Apply(context);
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				AsyncState.Finalize(interopCache, objects);
			}
		}
	}
#pragma warning restore CS8618
}