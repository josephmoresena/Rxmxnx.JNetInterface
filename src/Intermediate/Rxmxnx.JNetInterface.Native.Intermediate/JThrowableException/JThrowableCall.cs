namespace Rxmxnx.JNetInterface;

public partial class JThrowableException
{
	/// <summary>
	/// Helper to perform a call over a <see cref="JThrowableObject"/> instance.
	/// </summary>
	internal sealed record JThrowableCall
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
		/// Constructor.
		/// </summary>
		/// <param name="global">A global throwable instance.</param>
		/// <param name="del">A throwable delegate.</param>
		public JThrowableCall(JGlobalBase global, Delegate del)
		{
			this._global = global;
			this._delegate = del;
		}

		/// <summary>
		/// Invokes current delegate as <see cref="Action{TThrowable}"/>.
		/// </summary>
		/// <typeparam name="TThrowable">A <see cref="IThrowableType"/> type.</typeparam>
		public void Invoke<TThrowable>() where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			if (this._delegate is not Action<TThrowable> action) return;
			using IThread env = this._global.VirtualMachine.CreateThread(ThreadPurpose.ExceptionExecution);
			TThrowable throwableT =
				JThrowableCall.Parse<TThrowable>(new(env, this._global), this._global.ObjectMetadata);
			action(throwableT);
			this._global.RefreshMetadata(throwableT);
		}
		/// <summary>
		/// Execute current delegate as <see cref="Func{TThrowable, TResult}"/>.
		/// </summary>
		/// <typeparam name="TThrowable">A <see cref="IThrowableType"/> type.</typeparam>
		/// <typeparam name="TResult">Type of function result.</typeparam>
		/// <returns>Function result.</returns>
		public TResult Invoke<TThrowable, TResult>() where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			if (this._delegate is not Func<TThrowable, TResult> func) return default!;
			using IThread env = this._global.VirtualMachine.CreateThread(ThreadPurpose.ExceptionExecution);
			TThrowable throwableT =
				JThrowableCall.Parse<TThrowable>(new(env, this._global), this._global.ObjectMetadata);
			TResult result = func(throwableT);
			this._global.RefreshMetadata(throwableT);
			return result;
		}

		/// <summary>
		/// Creates a <typeparamref name="TThrowable"/> instances from <paramref name="throwable"/>.
		/// </summary>
		/// <param name="throwable">A <see cref="JThrowableObject"/> instance.</param>
		/// <param name="metadata">The <see cref="JThrowableObject"/> instance.</param>
		/// <typeparam name="TThrowable"></typeparam>
		/// <returns></returns>
		private static TThrowable Parse<TThrowable>(JThrowableObject throwable, JObjectMetadata metadata)
			where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			if (throwable is TThrowable throwableT) return throwableT;
			throwableT = (TThrowable)IReferenceType.GetMetadata<TThrowable>().ParseInstance(throwable)!;
			ILocalObject.ProcessMetadata(throwableT, metadata);
			return throwableT;
		}
	}
}