namespace Rxmxnx.JNetInterface.ContextBuilder;

/// <summary>
/// Builder type for initialize an async JNI context.
/// </summary>
public partial struct AsyncContextBuilder()
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="threadName">Thread creation name.</param>
	/// <param name="threadGroup">Thread creation group.</param>
	internal AsyncContextBuilder(CString? threadName, JGlobalBase? threadGroup) : this()
	{
		this._threadName = threadName;
		this._threadGroup = threadGroup;
	}

	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params JBoolean[] values)
	{
		this._booleans = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params Boolean[] values)
	{
		this._booleans = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params JByte[] values)
	{
		this._bytes = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params SByte[] values)
	{
		this._bytes = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params JChar[] values)
	{
		this._chars = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params Char[] values)
	{
		this._chars = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params JDouble[] values)
	{
		this._doubles = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params Double[] values)
	{
		this._doubles = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params JFloat[] values)
	{
		this._floats = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params Single[] values)
	{
		this._floats = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params JInt[] values)
	{
		this._ints = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params Int32[] values)
	{
		this._ints = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params JLong[] values)
	{
		this._longs = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params Int64[] values)
	{
		this._longs = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params JShort[] values)
	{
		this._shorts = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params Int16[] values)
	{
		this._shorts = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public AsyncContextBuilder With(params IJavaPeerable?[] values)
	{
		this._objects = values;
		return this;
	}
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	public Task InvokeAsync(AndroidJniAction action)
	{
		TaskState taskState = new(AndroidJniHost.GetAndroidHost(), this, action);
		return Task.Factory.StartNew(TaskState.Invoke, taskState);
	}
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	public Task InvokeAsync<TState>(TState state, AndroidJniAction<TState> action)
	{
		TaskState<TState> taskState = new(AndroidJniHost.GetAndroidHost(), this, action, state);
		return Task.Factory.StartNew(TaskState.Invoke, taskState);
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	public Task<TResult> InvokeAsync<TResult>(AndroidJniFunc<TResult> func)
	{
		TaskState taskState = new(AndroidJniHost.GetAndroidHost(), this, func);
		return Task.Factory.StartNew(TaskState.Invoke<TResult>, taskState);
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	public Task<TResult> InvokeAsync<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		AndroidJniFunc<JReferenceObject?> func) where TResult : class, IJavaPeerable
	{
		TaskState taskState = new(AndroidJniHost.GetAndroidHost(), this, func);
		return Task.Factory.StartNew(TaskState.InvokeJni<TResult>, taskState);
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	public Task<TResult> InvokeAsync<TState, TResult>(TState state, AndroidJniFunc<TState, TResult> func)
	{
		TaskState<TState> taskState = new(AndroidJniHost.GetAndroidHost(), this, func, state);
		return Task.Factory.StartNew(TaskState.Invoke<TResult>, taskState);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	public Task<TResult> InvokeAsync<TState,
		[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(TState state,
		AndroidJniFunc<TState, JReferenceObject?> func) where TResult : class, IJavaPeerable
	{
		TaskState<TState> taskState = new(AndroidJniHost.GetAndroidHost(), this, func, state);
		return Task.Factory.StartNew(TaskState.InvokeJni<TResult>, taskState);
	}
	/// <summary>
	/// Creates a new JNI async context and runs <paramref name="action"/>.
	/// </summary>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	/// <param name="task">Output. <paramref name="action"/> invocation operation task.</param>
	public void Run(AndroidJniAction action, out Task task)
	{
		TaskState taskState = new(AndroidJniHost.GetAndroidHost(), this, action, true);
		task = Task.Factory.StartNew(TaskState.Invoke, taskState);
	}
	/// <summary>
	/// Creates a new JNI async context and runs <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	/// <param name="task">Output. <paramref name="action"/> invocation operation task.</param>
	public void Run<TState>(TState state, AndroidJniAction<TState> action, out Task task)
	{
		TaskState<TState> taskState = new(AndroidJniHost.GetAndroidHost(), this, action, state, true);
		task = Task.Factory.StartNew(TaskState.Invoke, taskState);
	}
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	public Task Post(SynchronizationContext context, AndroidJniAction action)
	{
		TaskState taskState = new(AndroidJniHost.GetAndroidHost(), this, action);
		TaskCompletionSource source = new();
		PostState postState = new(taskState, source);
		context.Post(PostState.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	public Task Post<TState>(SynchronizationContext context, TState state, AndroidJniAction<TState> action)
	{
		TaskState<TState> taskState = new(AndroidJniHost.GetAndroidHost(), this, action, state);
		TaskCompletionSource source = new();
		PostState postState = new(taskState, source);
		context.Post(PostState.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	public Task<TResult> Post<TResult>(SynchronizationContext context, AndroidJniFunc<TResult> func)
	{
		TaskState taskState = new(AndroidJniHost.GetAndroidHost(), this, func);
		TaskCompletionSource<TResult> source = new();
		PostState<TResult> postState = new(taskState, source);
		context.Post(PostState<TResult>.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	public Task<TResult> Post<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		SynchronizationContext context, AndroidJniFunc<JReferenceObject?> func) where TResult : class, IJavaPeerable
	{
		TaskState taskState = new(AndroidJniHost.GetAndroidHost(), this, func);
		TaskCompletionSource<TResult> source = new();
		PostJniState<TResult> postState = new(taskState, source);
		context.Post(PostJniState<TResult>.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	public Task<TResult> Post<TState, TResult>(SynchronizationContext context, TState state,
		AndroidJniFunc<TState, TResult> func)
	{
		TaskState<TState> taskState = new(AndroidJniHost.GetAndroidHost(), this, func, state);
		TaskCompletionSource<TResult> source = new();
		PostState<TResult> postState = new(taskState, source);
		context.Post(PostState<TResult>.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	public Task<TResult> Post<TState, [DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		TState state, SynchronizationContext context, AndroidJniFunc<TState, JReferenceObject?> func)
		where TResult : class, IJavaPeerable
	{
		TaskState<TState> taskState = new(AndroidJniHost.GetAndroidHost(), this, func, state);
		TaskCompletionSource<TResult> source = new();
		PostJniState<TResult> postState = new(taskState, source);
		context.Post(PostJniState<TResult>.Post, postState);
		return source.Task;
	}
}