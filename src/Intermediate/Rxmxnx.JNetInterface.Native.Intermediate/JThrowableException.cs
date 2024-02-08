namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents error that occur during JNI calls.
/// </summary>
public abstract partial class JThrowableException : Exception
{
	/// <inheritdoc cref="Global"/>
	private readonly JGlobalBase _jGlobal;
	/// <inheritdoc cref="JThrowableException.Thread"/>
	private readonly Thread _thread;

	/// <summary>
	/// Global throwable instance.
	/// </summary>
	internal JGlobalBase Global => this._jGlobal;
	/// <summary>
	/// Thread that owns the exception.
	/// </summary>
	internal Thread Thread => this._thread;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> throwable instance.</param>
	/// <param name="message">Exception message.</param>
	internal JThrowableException(JGlobalBase jGlobal, String? message) : base(message)
	{
		this._jGlobal = jGlobal;
		this._thread = Thread.CurrentThread;
	}

	/// <summary>
	/// Invokes <paramref name="action"/> using the current <see cref="JThrowableObject"/> instance.
	/// </summary>
	/// <param name="action">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	public virtual void WithSafeInvoke(Action<JThrowableObject> action)
	{
		JThrowableCall call = new(this._jGlobal, action);
		Task.Factory.StartNew(JThrowableException.WithSafeInvoke<JThrowableObject>, call).Wait();
	}
	/// <summary>
	/// Executes <paramref name="func"/> using the current <see cref="JThrowableObject"/> instance.
	/// </summary>
	/// <typeparam name="TResult">Type of <paramref name="func"/> result.</typeparam>
	/// <param name="func">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	/// <returns>Execution result of <paramref name="func"/>.</returns>
	public virtual TResult WithSafeInvoke<TResult>(Func<JThrowableObject, TResult> func)
	{
		JThrowableCall call = new(this._jGlobal, func);
		return Task.Factory.StartNew(JThrowableException.WithSafeInvoke<JThrowableObject, TResult>, call).Result;
	}

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
public sealed class JThrowableException<TThrowable> : JThrowableException
	where TThrowable : JThrowableObject, IThrowableType<TThrowable>
{
	/// <inheritdoc/>
	internal JThrowableException(JGlobalBase jGlobal, String? message) : base(jGlobal, message)
	{
		jGlobal.SetAssignableTo<TThrowable>(true);
	}

	/// <summary>
	/// Invokes <paramref name="action"/> using the current <typeparamref name="TThrowable"/> instance.
	/// </summary>
	/// <param name="action">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	public void WithSafeInvoke(Action<TThrowable> action)
	{
		JThrowableCall call = new(this.Global, action);
		Task.Factory.StartNew(JThrowableException.WithSafeInvoke<JThrowableObject>, call).Wait();
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
		return Task.Factory.StartNew(JThrowableException.WithSafeInvoke<JThrowableObject, TResult>, call).Result;
	}
	/// <inheritdoc/>
	public override void WithSafeInvoke(Action<JThrowableObject> action)
		=> JThrowableException<TThrowable>.WithSafeInvoke(this, action);
	/// <inheritdoc/>
	public override TResult WithSafeInvoke<TResult>(Func<JThrowableObject, TResult> func)
		=> JThrowableException<TThrowable>.WithSafeInvoke(this, func);

	/// <summary>
	/// Invokes <paramref name="action"/> using <paramref name="exception"/> instance.
	/// </summary>
	/// <param name="exception">A <see cref="JThrowableException{TThrowable}"/> instance.</param>
	/// <param name="action">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	private static void WithSafeInvoke(JThrowableException<TThrowable> exception, Action<TThrowable> action)
		=> exception.WithSafeInvoke(action);
	/// <summary>
	/// Executes <paramref name="func"/> using using &lt;paramref name="exception"/&gt; instance.
	/// </summary>
	/// <typeparam name="TResult">Type of <paramref name="func"/> result.</typeparam>
	/// <param name="exception">A <see cref="JThrowableException{TThrowable}"/> instance.</param>
	/// <param name="func">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	/// <returns>Execution result of <paramref name="func"/>.</returns>
	private static TResult WithSafeInvoke<TResult>(JThrowableException<TThrowable> exception,
		Func<TThrowable, TResult> func)
		=> exception.WithSafeInvoke(func);
}