namespace Rxmxnx.JNetInterface;

/// <summary>
/// Functional interfaces extensions.
/// </summary>
public static class FunctionalExtensions
{
	/// <summary>
	/// Executes a provided frame action within the context of the current environment.
	/// </summary>
	/// <typeparam name="TAction">The <see cref="IFrameAction"/> type of the frame action to execute.</typeparam>
	/// <param name="environment">The <see cref="IEnvironment"/> instance which the action is executed.</param>
	/// <param name="action">The frame action to execute.</param>
	public static void WithFrame<TAction>(this IEnvironment? environment, TAction? action)
#if !NET9_0_OR_GREATER
		where TAction : IFrameAction
#else
		where TAction : IFrameAction, allows ref struct
#endif
	{
		if (environment is null || action is null)
			return;
		environment.WithFrameExecute(ref action);
	}
	/// <summary>
	/// Executes the current frame action within the context of the specified environment.
	/// </summary>
	/// <typeparam name="TAction">The <see cref="IFrameAction"/> type of the frame action to execute.</typeparam>
	/// <param name="environment">The <see cref="IEnvironment"/> instance which the action is executed.</param>
	/// <param name="action">The frame action to execute.</param>
	public static void WithFrame<TAction>(this ref TAction action, IEnvironment? environment)
#if !NET9_0_OR_GREATER
		where TAction : struct, IFrameAction
#else
		where TAction : struct, IFrameAction, allows ref struct
#endif
	{
		environment?.WithFrameExecute(ref action);
	}
	/// <summary>
	/// Executes a provided frame function within the context of the current environment.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TResult}"/> type of the frame action to execute.</typeparam>
	/// <typeparam name="TResult">The type of the result returned by the frame function.</typeparam>
	/// <param name="environment">The <see cref="IEnvironment"/> instance which the function is executed.</param>
	/// <param name="func">The frame function to execute.</param>
	/// <param name="result">Output. The result returned by the frame function.</param>
	public static void WithFrame<TFunction, TResult>(this IEnvironment? environment, TFunction? func,
		out TResult result)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<TResult>
#else
		where TFunction : IFrameFunction<TResult>, allows ref struct
#endif
	{
		if (environment is null || func is null)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		result = environment.WithFrameExecute<TFunction, TResult>(ref func);
	}
	/// <summary>
	/// Executes the current frame function within the context of the specified environment.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TResult}"/> type of the frame action to execute.</typeparam>
	/// <typeparam name="TResult">The type of the result returned by the frame function.</typeparam>
	/// <param name="environment">The <see cref="IEnvironment"/> instance which the function is executed.</param>
	/// <param name="func">The frame function to execute.</param>
	/// <param name="result">Output. The result returned by the frame function.</param>
	public static void WithFrame<TFunction, TResult>(this ref TFunction func, IEnvironment? environment,
		out TResult result)
#if !NET9_0_OR_GREATER
		where TFunction : struct, IFrameFunction<TResult>
#else
		where TFunction : struct, IFrameFunction<TResult>, allows ref struct
#endif
	{
		if (environment is null)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		result = environment.WithFrameExecute<TFunction, TResult>(ref func);
	}
}