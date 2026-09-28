namespace Rxmxnx.JNetInterface;

public unsafe partial interface IEnvironment
{
	/// <summary>
	/// Temporal thread static environment used to avoid closure.
	/// </summary>
	[ThreadStatic]
	private static IEnvironment? tempEnv;

	/// <summary>
	/// Executes a provided frame-based function within a controlled JNI environment frame and returns the result.
	/// </summary>
	/// <typeparam name="TFunction">
	/// The type of the function to execute, which must implement <see cref="IFrameFunction{TOutput}"/>.
	/// </typeparam>
	/// <typeparam name="TResult">The result type produced by the function.</typeparam>
	/// <param name="func">A reference to the function to execute within the frame.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <returns>The result produced by the executed function.</returns>
	/// <remarks>A default implementation is provided to avoid binary compatibility with older and proxy implementations.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal TResult WithFrameExecute<TFunction, TResult>(ref TFunction func, Int32 capacity)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<TResult>
#else
		where TFunction : IFrameFunction<TResult>, allows ref struct
#endif
	{
		IEnvironment.tempEnv = this;
		try
		{
#pragma warning disable CS8500
			fixed (TFunction* ptr = &func)
			{
				return this.WithFrame(capacity, (IntPtr)ptr,
				                      static p => ((TFunction*)p)[0].Apply(IEnvironment.tempEnv, []));
#pragma warning restore CS8500
			}
		}
		finally
		{
			IEnvironment.tempEnv = default;
		}
	}
	/// <summary>
	/// Executes a specified action within a controlled JNI environment frame.
	/// </summary>
	/// <typeparam name="TAction">The type of the action to execute, which must implement <see cref="IFrameAction"/>.</typeparam>
	/// <param name="action">A reference to the action to be executed within the frame.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <remarks>A default implementation is provided to avoid binary compatibility with older and proxy implementations.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal void WithFrameExecute<TAction>(ref TAction action, Int32 capacity)
#if !NET9_0_OR_GREATER
		where TAction : IFrameAction
#else
		where TAction : IFrameAction, allows ref struct
#endif
	{
		IEnvironment.tempEnv = this;
		try
		{
#pragma warning disable CS8500
			fixed (TAction* ptr = &action)
			{
				this.WithFrame(capacity, (IntPtr)ptr, static p => ((TAction*)p)[0].Accept(IEnvironment.tempEnv, []));
			}
#pragma warning restore CS8500
		}
		finally
		{
			IEnvironment.tempEnv = default;
		}
	}
}