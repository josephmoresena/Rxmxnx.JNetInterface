namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
#pragma warning disable CS8500
	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Send(SendOrPostCallback, Object)"/>
	/// </summary>
	private unsafe class SendState
	{
		/// <summary>
		/// A <see cref="SyncContextBuilder"/> unmanaged pointer.
		/// </summary>
		private readonly SyncContextBuilder* _builderPtr;
		/// <summary>
		/// A <see cref="Exception"/> unmanaged pointer.
		/// </summary>
		private readonly Exception* _exceptionPtr;

		/// <summary>
		/// State pointer.
		/// </summary>
		public void* StatePointer { get; init; }
		/// <summary>
		/// Result pointer.
		/// </summary>
		public void* ResultPointer { get; init; }
		/// <summary>
		/// Call delegate.
		/// </summary>
		public Delegate? CallDelegate { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="builderPtr">A <see cref="SyncContextBuilder"/> pointer.</param>
		/// <param name="exceptionPtr">A <see cref="Exception"/> pointer.</param>
		public SendState(SyncContextBuilder* builderPtr, Exception* exceptionPtr)
		{
			this._builderPtr = builderPtr;
			this._exceptionPtr = exceptionPtr;
		}

		/// <summary>
		/// Creates a new JNI context and invokes <paramref name="action"/>.
		/// </summary>
		/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Invoke(AndroidJniAction action)
		{
			// Execution is now in the UI thread.
			try
			{
				this.GetBuilder().Invoke(action);
			}
			catch (Exception ex)
			{
				this.GetException() = ex;
			}
		}
		/// <summary>
		/// Creates a new JNI context and invokes <paramref name="action"/>.
		/// </summary>
		/// <typeparam name="TState">Type of state object</typeparam>
		/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
		public void Invoke<TState>(AndroidJniAction<TState> action)
		{
			// Execution is now in the UI thread.
			try
			{
				this.GetBuilder().Invoke(this.GetState<TState>(), action);
			}
			catch (Exception ex)
			{
				this.GetException() = ex;
			}
		}
		/// <summary>
		/// Creates a new JNI context, invokes <paramref name="func"/> and save its result.
		/// </summary>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
		public void Invoke<TResult>(AndroidJniFunc<TResult> func)
		{
			// Execution is now in the UI thread.
			try
			{
				this.GetResult<TResult>() = this.GetBuilder().Invoke(func);
			}
			catch (Exception ex)
			{
				this.GetException() = ex;
			}
		}
		/// <summary>
		/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
		/// </summary>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
		public void InvokeJni<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
			AndroidJniFunc<JReferenceObject> func) where TResult : class, IJavaPeerable
		{
			// Execution is now in the UI thread.
			try
			{
				this.GetResult<TResult>() = this.GetBuilder().Invoke<TResult>(func);
			}
			catch (Exception ex)
			{
				this.GetException() = ex;
			}
		}
		/// <summary>
		/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
		/// </summary>
		/// <typeparam name="TState">Type of state object</typeparam>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
		public void Invoke<TState, TResult>(AndroidJniFunc<TState, TResult> func)
#if NET9_0_OR_GREATER
			where TState : allows ref struct
#endif
		{
			// Execution is now in the UI thread.
			try
			{
				this.GetResult<TResult>() = this.GetBuilder().Invoke(this.GetState<TState>(), func);
			}
			catch (Exception ex)
			{
				this.GetException() = ex;
			}
		}
		/// <summary>
		/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
		/// </summary>
		/// <typeparam name="TState">Type of state object</typeparam>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
		public void InvokeJni<TState, [DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
			AndroidJniFunc<TState, JReferenceObject?> func) where TResult : class, IJavaPeerable
#if NET9_0_OR_GREATER
			where TState : allows ref struct
#endif
		{
			// Execution is now in the UI thread.
			try
			{
				this.GetResult<TResult>() = this.GetBuilder().Invoke<TState, TResult>(this.GetState<TState>(), func);
			}
			catch (Exception ex)
			{
				this.GetException() = ex;
			}
		}

		/// <summary>
		/// Performs a volatile-read semantic operation on a remotely mutated result reference,
		/// forcing the JIT compiler to invalidate CPU registers and synchronize thread caches.
		/// </summary>
		/// <param name="refResult">The managed reference to the result variable mutated by the remote thread.</param>
		/// <typeparam name="TResult">The generic type of the result object or value structure.</typeparam>
		/// <returns>The up-to-date value of <typeparamref name="TResult"/> read directly from physical memory.</returns>
		public static TResult VolatileRead<TResult>(ref TResult refResult)
		{
			if (!typeof(TResult).IsValueType)
				return (TResult)Volatile.Read(ref Unsafe.As<TResult, Object>(ref refResult));
			return MemoryMarshal.CreateSpan(ref refResult, 1)[0];
		}

		/// <summary>
		/// Retrieves a read-only reference to the <see cref="SyncContextBuilder"/> instance.
		/// </summary>
		/// <returns>A read-only reference to the <see cref="SyncContextBuilder"/> instance.</returns>
		private ref SyncContextBuilder GetBuilder() => ref *this._builderPtr;
		/// <summary>
		/// Retrieves a read-only reference to the <typeparamref name="TState"/> state object.
		/// </summary>
		/// <typeparam name="TState">Type of state object</typeparam>
		/// <returns>A read-only reference to the <typeparamref name="TState"/> state object.</returns>
		private ref readonly TState GetState<TState>()
#if NET9_0_OR_GREATER
			where TState : allows ref struct
#endif
		{
			if (this.StatePointer == default)
				return ref Unsafe.NullRef<TState>();
			return ref Unsafe.AsRef<TState>(this.StatePointer);
		}
		/// <summary>
		/// Retrieves a reference to the result object.
		/// </summary>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <returns>A reference to the result object.</returns>
		private ref TResult GetResult<TResult>()
		{
			if (this.ResultPointer == default)
				return ref Unsafe.NullRef<TResult>();
			return ref Unsafe.AsRef<TResult>(this.ResultPointer);
		}
		/// <summary>
		/// Retrieves a reference to the exception object.
		/// </summary>
		/// <returns>A reference to the exception object.</returns>
		private ref Exception GetException() => ref Unsafe.AsRef<Exception>(this._exceptionPtr);
	}
#pragma warning restore CS8500
}