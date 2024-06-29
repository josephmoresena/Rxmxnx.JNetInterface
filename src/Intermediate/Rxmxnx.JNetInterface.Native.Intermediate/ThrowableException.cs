using System.Runtime.Serialization;

namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents error that occur during JNI calls.
/// </summary>
public abstract partial class ThrowableException : JniException, IThrowableException<JThrowableObject>
{
	/// <summary>
	/// Environment reference.
	/// </summary>
	public JEnvironmentRef EnvironmentRef { get; }
	/// <summary>
	/// Exception thread identifier.
	/// </summary>
	public Int32 ThreadId { get; }
	/// <summary>
	/// Throwable reference.
	/// </summary>
	public JThrowableLocalRef ThrowableRef => this.GlobalThrowable?.To<JThrowableLocalRef>() ?? default; 

	/// <summary>
	/// Global throwable instance.
	/// </summary>
	internal JGlobalBase GlobalThrowable { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> throwable instance.</param>
	/// <param name="message">Exception message.</param>
	private protected ThrowableException(JGlobalBase jGlobal, String? message) : base(message: message)
	{
		this.ThreadId = Environment.CurrentManagedThreadId;
		this.GlobalThrowable = jGlobal;
		this.EnvironmentRef = this.GlobalThrowable.VirtualMachine.GetEnvironment()!.Reference;
	}

	/// <inheritdoc/>
	public void WithSafeInvoke(Action<JThrowableObject> action) => this.WithSafeInvokeBase(action);
	/// <inheritdoc/>
	public TResult WithSafeInvoke<TResult>(Func<JThrowableObject, TResult> func) => this.WithSafeInvokeBase(func);

	/// <summary>
	/// Invokes <paramref name="action"/> using the current <see cref="JThrowableObject"/> instance.
	/// </summary>
	/// <param name="action">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	private protected abstract void WithSafeInvokeBase(Action<JThrowableObject> action);
	/// <summary>
	/// Executes <paramref name="func"/> using the current <see cref="JThrowableObject"/> instance.
	/// </summary>
	/// <typeparam name="TResult">Type of <paramref name="func"/> result.</typeparam>
	/// <param name="func">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	/// <returns>Execution result of <paramref name="func"/>.</returns>
	private protected abstract TResult WithSafeInvokeBase<TResult>(Func<JThrowableObject, TResult> func);

	/// <summary>
	/// Performs an action using current global throwable instance.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
	/// <param name="call">A <see cref="JThrowableCall"/> containing action to perform.</param>
	internal static void
		WithSafeInvoke<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable>(Object? call)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		=> (call as JThrowableCall)?.Invoke<TThrowable>();
	/// <summary>
	/// Executes an function using current global throwable instance and returns its result.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
	/// <typeparam name="TResult">The type of function result.</typeparam>
	/// <param name="call">A <see cref="JThrowableCall"/> containing function to execute.</param>
	/// <returns>The function result.</returns>
	internal static TResult
		WithSafeInvoke<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable, TResult>(
			Object? call) where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		=> call is JThrowableCall jCall ? jCall.Invoke<TThrowable, TResult>() : default!;
}

/// <summary>
/// Represents error that occur during JNI calls.
/// </summary>
/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
internal sealed class
	ThrowableException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable> :
	ThrowableException, IThrowableException<TThrowable> where TThrowable : JThrowableObject, IThrowableType<TThrowable>
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
		JThrowableCall call = new(this.GlobalThrowable, this.ThreadId, action);
		Task.Factory.StartNew(ThrowableException.WithSafeInvoke<TThrowable>, call, TaskCreationOptions.LongRunning)
		    .Wait();
	}
	/// <summary>
	/// Executes <paramref name="func"/> using the current <typeparamref name="TThrowable"/> instance.
	/// </summary>
	/// <typeparam name="TResult">Type of <paramref name="func"/> result.</typeparam>
	/// <param name="func">A <see cref="Action{JThrowableObject}"/> delegate.</param>
	/// <returns>Execution result of <paramref name="func"/>.</returns>
	public TResult WithSafeInvoke<TResult>(Func<TThrowable, TResult> func)
	{
		JThrowableCall call = new(this.GlobalThrowable, this.ThreadId, func);
		return Task.Factory.StartNew(ThrowableException.WithSafeInvoke<TThrowable, TResult>, call,
		                             TaskCreationOptions.LongRunning).Result;
	}

	/// <inheritdoc/>
	private protected override void WithSafeInvokeBase(Action<JThrowableObject> action) => this.WithSafeInvoke(action);
	/// <inheritdoc/>
	private protected override TResult WithSafeInvokeBase<TResult>(Func<JThrowableObject, TResult> func)
		=> this.WithSafeInvoke(func);
}