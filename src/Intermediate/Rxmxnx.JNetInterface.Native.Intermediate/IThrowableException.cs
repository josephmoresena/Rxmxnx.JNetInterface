namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents error that occur during JNI calls.
/// </summary>
public interface IThrowableException
{
	/// <summary>
	/// Invokes <paramref name="action"/> using the current <see cref="JThrowableObject"/>  instance.
	/// </summary>
	/// <param name="action">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	void WithSafeInvoke(Action<JThrowableObject> action);
	/// <summary>
	/// Executes <paramref name="func"/> using the current <see cref="JThrowableObject"/> instance.
	/// </summary>
	/// <typeparam name="TResult">Type of <paramref name="func"/> result.</typeparam>
	/// <param name="func">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	/// <returns>Execution result of <paramref name="func"/>.</returns>
	TResult WithSafeInvoke<TResult>(Func<JThrowableObject, TResult> func);
}

/// <summary>
/// Represents error that occur during JNI calls.
/// </summary>
/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
public interface IThrowableException<out TThrowable> : IThrowableException
	where TThrowable : JThrowableObject, IThrowableType<TThrowable>
{
	/// <summary>
	/// Invokes <paramref name="action"/> using the current <typeparamref name="TThrowable"/> instance.
	/// </summary>
	/// <param name="action">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	void WithSafeInvoke(Action<TThrowable> action);
	/// <summary>
	/// Executes <paramref name="func"/> using the current <typeparamref name="TThrowable"/> instance.
	/// </summary>
	/// <typeparam name="TResult">Type of <paramref name="func"/> result.</typeparam>
	/// <param name="func">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	/// <returns>Execution result of <paramref name="func"/>.</returns>
	TResult WithSafeInvoke<TResult>(Func<TThrowable, TResult> func);
}