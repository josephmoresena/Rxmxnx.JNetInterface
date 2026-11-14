namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Represents the state of an asynchronous action, providing mechanisms for tracking its progress, completion,
	/// and potential error handling.
	/// </summary>
#pragma warning disable CS8618
	private sealed class AsyncActionState<TAction>(
		IVirtualMachineHost host,
		AsyncContextBuilder builder,
		Boolean useWeakGlobal,
		Boolean isDaemon = false)
		: AsyncState(host, builder, useWeakGlobal, isDaemon) where TAction : IAndroidNativeAction
	{
		/// <summary>
		/// Internal action to be executed.
		/// </summary>
		public TAction Action { get; init; }

		/// <inheritdoc/>
		public override void Invoke(INativeThread env)
		{
			JLocalObject?[] objects = [];
			JavaInteropCache? interopCache = default;
			try
			{
				AndroidJniContext context = this.CreateContext(env, out interopCache, out objects);
				using InlineCache _ = new(env);
				this.Action.Accept(context);
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