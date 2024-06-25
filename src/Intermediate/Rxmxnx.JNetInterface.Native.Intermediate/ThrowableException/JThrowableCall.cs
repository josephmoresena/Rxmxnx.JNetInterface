namespace Rxmxnx.JNetInterface;

public partial class ThrowableException
{
	/// <summary>
	/// Helper to perform a call over a <see cref="JThrowableObject"/> instance.
	/// </summary>
	private protected sealed class JThrowableCall
	{
		/// <summary>
		/// A <see cref="JThrowableObject"/> delegate.
		/// </summary>
		private readonly Delegate _delegate;
		/// <summary>
		/// A <see cref="JGlobalBase"/> instance.
		/// </summary>
		private readonly JGlobalBase _global;
		/// <summary>
		/// Exception thread identifier.
		/// </summary>
		private readonly Int32 _threadId;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="global">A global throwable instance.</param>
		/// <param name="threadId">Exception thread id.</param>
		/// <param name="del">A throwable delegate.</param>
		public JThrowableCall(JGlobalBase global, Int32 threadId, Delegate del)
		{
			this._global = global;
			this._threadId = threadId;
			this._delegate = del;
		}

		/// <summary>
		/// Invokes current delegate as <see cref="Action{TThrowable}"/>.
		/// </summary>
		/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
		public void Invoke<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable>()
			where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			using IThread env = this._global.VirtualMachine.CreateThread(ThreadPurpose.ExceptionExecution);
			JTrace.InvokeAt(env);
			using JWeak jWeak = env.ReferenceFeature.CreateWeak(this._global);
			using TThrowable throwableT = jWeak.AsLocal<TThrowable>(env);
			throwableT.ThreadId = this._threadId;
			(this._delegate as Action<TThrowable>)!(throwableT);
			this._global.RefreshMetadata(throwableT);
		}
		/// <summary>
		/// Execute current delegate as <see cref="Func{TThrowable, TResult}"/>.
		/// </summary>
		/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
		/// <typeparam name="TResult">Type of function result.</typeparam>
		/// <returns>Function result.</returns>
		public TResult Invoke<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable,
			TResult>() where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			using IThread env = this._global.VirtualMachine.CreateThread(ThreadPurpose.ExceptionExecution);
			JTrace.InvokeAt(env);
			using JWeak jWeak = env.ReferenceFeature.CreateWeak(this._global);
			TThrowable throwableT = jWeak.AsLocal<TThrowable>(env);
			throwableT.ThreadId = this._threadId;
			TResult result = (this._delegate as Func<TThrowable, TResult>)!(throwableT);
			this._global.RefreshMetadata(throwableT);
			return result;
		}
	}
}