namespace Rxmxnx.JNetInterface.ContextBuilder;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public unsafe ref partial struct SyncContextBuilder
{
#pragma warning disable CS8500
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	public void Send(SynchronizationContext context, AndroidJniAction action)
	{
		SyncContextBuilder builder = this;
		Exception? ex = default;
		ref SyncContextBuilder refBuilder = ref builder;
		ref Exception? refEx = ref ex;
		fixed (SyncContextBuilder* builderPtr = &refBuilder)
		fixed (Exception* exPtr = &refEx)
		{
			context.Send(static s =>
			{
				if (s is not SendState { CallDelegate: AndroidJniAction a, } ss) return;
				ss.Invoke(a);
			}, new SendState(builderPtr, exPtr) { CallDelegate = action, });
			Thread.MemoryBarrier();
			ex = Volatile.Read(ref refEx);
		}
		if (ex is not null) throw ex;
	}
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	public void Send<TState>(SynchronizationContext context, TState state, AndroidJniAction<TState> action)
	{
		SyncContextBuilder builder = this;
		Exception? ex = default;
		ref SyncContextBuilder refBuilder = ref builder;
		ref Exception? refEx = ref ex;
		ref TState refState = ref state;
		fixed (SyncContextBuilder* builderPtr = &refBuilder)
		fixed (Exception* exPtr = &refEx)
		fixed (TState* sPtr = &refState)
		{
			context.Send(static s =>
			{
				if (s is not SendState { CallDelegate: AndroidJniAction<TState> a, } ss) return;
				if (ss.StatePointer == default) return;
				ss.Invoke(a);
			}, new SendState(builderPtr, exPtr) { StatePointer = sPtr, CallDelegate = action, });
			Thread.MemoryBarrier();
			ex = Volatile.Read(ref refEx);
		}
		if (ex is not null) throw ex;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	public TResult Send<TResult>(SynchronizationContext context, AndroidJniFunc<TResult> func)
	{
		Unsafe.SkipInit(out TResult result);
		SyncContextBuilder builder = this;
		Exception? ex = default;
		ref SyncContextBuilder refBuilder = ref builder;
		ref Exception? refEx = ref ex;
		ref TResult refResult = ref result;
		fixed (SyncContextBuilder* builderPtr = &refBuilder)
		fixed (Exception* exPtr = &refEx)
		fixed (TResult* rPtr = &refResult)
		{
			context.Send(static s =>
			{
				if (s is not SendState { CallDelegate: AndroidJniFunc<TResult> f, } ss) return;
				if (ss.ResultPointer == default) return;
				ss.Invoke(f);
			}, new SendState(builderPtr, exPtr) { ResultPointer = rPtr, CallDelegate = func, });
			Thread.MemoryBarrier();
			ex = Volatile.Read(ref refEx);
			result = SendState.VolatileRead(ref refResult);
		}
		return ex is not null ? throw ex : result;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	public TResult Send<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		SynchronizationContext context, AndroidJniFunc<JReferenceObject?> func) where TResult : class, IJavaPeerable
	{
		Unsafe.SkipInit(out TResult result);
		SyncContextBuilder builder = this;
		Exception? ex = default;
		ref SyncContextBuilder refBuilder = ref builder;
		ref Exception? refEx = ref ex;
		ref TResult refResult = ref result;
		fixed (SyncContextBuilder* builderPtr = &refBuilder)
		fixed (Exception* exPtr = &refEx)
		fixed (TResult* rPtr = &refResult)
		{
			context.Send(static s =>
			{
				if (s is not SendState { CallDelegate: AndroidJniFunc<JReferenceObject> f, } ss) return;
				if (ss.ResultPointer == default) return;
				ss.InvokeJni<TResult>(f);
			}, new SendState(builderPtr, exPtr) { ResultPointer = rPtr, CallDelegate = func, });
			Thread.MemoryBarrier();
			ex = Volatile.Read(ref refEx);
			result = SendState.VolatileRead(ref refResult);
		}
		return ex is not null ? throw ex : result;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	public TResult Send<TState, TResult>(SynchronizationContext context, TState state,
		AndroidJniFunc<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		Unsafe.SkipInit(out TResult result);
		SyncContextBuilder builder = this;
		Exception? ex = default;
		ref SyncContextBuilder refBuilder = ref builder;
		ref Exception? refEx = ref ex;
		ref TResult refResult = ref result;
		ref TState refState = ref state;
		fixed (SyncContextBuilder* builderPtr = &refBuilder)
		fixed (Exception* exPtr = &refEx)
		fixed (TState* sPtr = &refState)
		fixed (TResult* rPtr = &refResult)
		{
			context.Send(static s =>
			{
				if (s is not SendState { CallDelegate: AndroidJniFunc<TState, TResult> f, } ss) return;
				if (ss.StatePointer == default || ss.ResultPointer == default) return;
				ss.Invoke(f);
			}, new SendState(builderPtr, exPtr) { StatePointer = sPtr, ResultPointer = rPtr, CallDelegate = func, });
			Thread.MemoryBarrier();
			ex = Volatile.Read(ref refEx);
			result = SendState.VolatileRead(ref refResult);
		}
		return ex is not null ? throw ex : result;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	public TResult Send<TState, [DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		SynchronizationContext context, TState state, AndroidJniFunc<TState, JReferenceObject?> func)
		where TResult : class, IJavaPeerable
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		Unsafe.SkipInit(out TResult result);
		SyncContextBuilder builder = this;
		Exception? ex = default;
		ref SyncContextBuilder refBuilder = ref builder;
		ref Exception? refEx = ref ex;
		ref TResult refResult = ref result;
		ref TState refState = ref state;
		fixed (SyncContextBuilder* builderPtr = &refBuilder)
		fixed (Exception* exPtr = &refEx)
		fixed (TState* sPtr = &refState)
		fixed (TResult* rPtr = &refResult)
		{
			context.Send(static s =>
			{
				if (s is not SendState { CallDelegate: AndroidJniFunc<TState, JReferenceObject> f, } ss) return;
				if (ss.StatePointer == default || ss.ResultPointer == default) return;
				ss.InvokeJni<TState, TResult>(f);
			}, new SendState(builderPtr, exPtr) { StatePointer = sPtr, ResultPointer = rPtr, CallDelegate = func, });
			Thread.MemoryBarrier();
			ex = Volatile.Read(ref refEx);
			result = Volatile.Read(ref refResult);
		}
		return ex is not null ? throw ex : result;
	}
#pragma warning restore CS8500
}