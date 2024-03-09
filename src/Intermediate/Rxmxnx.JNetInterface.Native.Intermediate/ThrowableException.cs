namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents error that occur during JNI calls.
/// </summary>
public abstract partial class ThrowableException : JniException
{
	/// <summary>
	/// Global throwable instance.
	/// </summary>
	internal JGlobalBase Global { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> throwable instance.</param>
	/// <param name="message">Exception message.</param>
	private protected ThrowableException(JGlobalBase jGlobal, String? message) : base(message: message)
		=> this.Global = jGlobal;

	/// <summary>
	/// Performs an action using current global throwable instance.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
	/// <param name="call">A <see cref="JThrowableCall"/> containing action to perform.</param>
	internal static void WithSafeInvoke<TThrowable>(Object? call)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		=> (call as JThrowableCall)?.Invoke<TThrowable>();
	/// <summary>
	/// Executes an function using current global throwable instance and returns its result.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
	/// <typeparam name="TResult">The type of function result.</typeparam>
	/// <param name="call">A <see cref="JThrowableCall"/> containing function to execute.</param>
	/// <returns>The function result.</returns>
	internal static TResult WithSafeInvoke<TThrowable, TResult>(Object? call)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		=> call is JThrowableCall jCall ? jCall.Invoke<TThrowable, TResult>() : default!;
}

/// <summary>
/// Represents error that occur during JNI calls.
/// </summary>
/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
public sealed class ThrowableException<TThrowable> : ThrowableException, IThrowableException<TThrowable>
	where TThrowable : JThrowableObject, IThrowableType<TThrowable>
{
	/// <inheritdoc/>
	internal ThrowableException(JGlobalBase jGlobal, String? message) : base(jGlobal, message)
		=> jGlobal.SetAssignableTo<TThrowable>(true);

	/// <summary>
	/// Invokes <paramref name="action"/> using the current <typeparamref name="TThrowable"/> instance.
	/// </summary>
	/// <param name="action">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	public void WithSafeInvoke(Action<TThrowable> action)
	{
		JThrowableCall call = new(this.Global, action);
		Task.Factory.StartNew(ThrowableException.WithSafeInvoke<JThrowableObject>, call).Wait();
	}
	/// <summary>
	/// Executes <paramref name="func"/> using the current <typeparamref name="TThrowable"/> instance.
	/// </summary>
	/// <typeparam name="TResult">Type of <paramref name="func"/> result.</typeparam>
	/// <param name="func">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	/// <returns>Execution result of <paramref name="func"/>.</returns>
	public TResult WithSafeInvoke<TResult>(Func<TThrowable, TResult> func)
	{
		JThrowableCall call = new(this.Global, func);
		return Task.Factory.StartNew(ThrowableException.WithSafeInvoke<JThrowableObject, TResult>, call).Result;
	}
}