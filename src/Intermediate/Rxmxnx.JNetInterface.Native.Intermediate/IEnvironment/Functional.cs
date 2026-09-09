namespace Rxmxnx.JNetInterface;

public unsafe partial interface IEnvironment
{
	/// <summary>
	/// Executes a provided frame-based function within a controlled JNI environment frame and returns the result.
	/// </summary>
	/// <typeparam name="TFunction">The type of the function to execute, which must implement <see cref="IFrameFunction{TResult}"/>.</typeparam>
	/// <typeparam name="TResult">The result type produced by the function.</typeparam>
	/// <param name="func">A reference to the function to execute within the frame.</param>
	/// <returns>The result produced by the executed function.</returns>
	/// <remarks>A default implementation is provided to avoid binary compatibility with older and proxy implementations.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal TResult WithFrameExecute<TFunction, TResult>(ref TFunction func)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<TResult>
#else
		where TFunction : IFrameFunction<TResult>, allows ref struct
#endif
	{
#pragma warning disable CS8500
		fixed (TFunction* ptr = &func)
#pragma warning restore CS8500
		{
			// ReSharper disable once RedundantCast
			ValueTuple<IEnvironment, ValPtr<TFunction>> args = (this, (ValPtr<TFunction>)ptr);
			return this.WithFrame(func.RequiredCapacity, args, static a => a.Item2.Reference.Apply(a.Item1));
		}
	}
	/// <summary>
	/// Executes a specified action within a controlled JNI environment frame.
	/// </summary>
	/// <typeparam name="TAction">The type of the action to execute, which must implement <see cref="IFrameAction"/>.</typeparam>
	/// <param name="action">A reference to the action to be executed within the frame.</param>
	/// <remarks>A default implementation is provided to avoid binary compatibility with older and proxy implementations.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal void WithFrameExecute<TAction>(ref TAction action)
#if !NET9_0_OR_GREATER
		where TAction : IFrameAction
#else
		where TAction : IFrameAction, allows ref struct
#endif
	{
#pragma warning disable CS8500
		fixed (TAction* ptr = &action)
#pragma warning restore CS8500
		{
			// ReSharper disable once RedundantCast
			ValueTuple<IEnvironment, ValPtr<TAction>> args = (this, (ValPtr<TAction>)ptr);
			this.WithFrame(action.RequiredCapacity, args, static a => a.Item2.Reference.Accept(a.Item1));
		}
	}
}