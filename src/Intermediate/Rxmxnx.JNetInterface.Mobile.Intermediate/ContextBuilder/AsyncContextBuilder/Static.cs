namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	private partial class TaskState
	{
		/// <summary>
		/// Action delegate for <see cref="TaskFactory.StartNew(Action{Object}, Object)"/>.
		/// </summary>
		/// <param name="state">Action state object.</param>
		public static void Invoke(Object? state)
		{
			if (state is not TaskState taskState) return;
			using IThread __ = taskState.CreateThread(out INativeThread env);
			taskState.Invoke(env);
		}
		/// <summary>
		/// Function delegate for <see cref="TaskFactory.StartNew{TResult}(Func{Object, TResult}, Object)"/>.
		/// </summary>
		/// <param name="state">Function state object.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		public static TResult Invoke<TResult>(Object? state)
		{
			if (state is not TaskState taskState) return default!;
			using IThread __ = taskState.CreateThread(out INativeThread env);
			return taskState.Invoke<TResult>(env);
		}
		/// <summary>
		/// Function delegate for <see cref="TaskFactory.StartNew{TResult}(Func{Object, TResult}, Object)"/>.
		/// </summary>
		/// <param name="state">Function state object.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		public static TResult
			InvokeJni<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(Object? state)
			where TResult : class, IJavaPeerable
		{
			if (state is not TaskState taskState) return default!;
			using IThread __ = taskState.CreateThread(out INativeThread env);
			return taskState.InvokeJni<TResult>(env);
		}
	}
}