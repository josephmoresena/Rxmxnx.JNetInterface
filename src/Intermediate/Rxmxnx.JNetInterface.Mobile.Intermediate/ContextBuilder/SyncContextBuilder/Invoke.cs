// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	public void Invoke(AndroidJniAction action)
	{
		IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
		INativeThread env = host.GetNativeThread();
		Int32 objectCount = this._objects.Length;
		JLocalObject?[] objects = objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(objectCount) : [];
		JavaInteropCache? interopCache = objectCount > 0 ? new(host, env) : default;
		JGlobalBase? throwable = default;
		try
		{
			AndroidJniContext context = new(env)
			{
				Booleans = this._booleans,
				Bytes = this._bytes,
				Chars = this._chars,
				Doubles = this._doubles,
				Floats = this._floats,
				Ints = this._ints,
				Longs = this._longs,
				Shorts = this._shorts,
				Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
			};
			using InlineCache _ = new(env);
			action(context);
			return;
		}
		catch (ThrowableException e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
			throwable = e.GlobalThrowable;
		}
		catch (Exception e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
		}
		finally
		{
			ArrayPool<JLocalObject?>.Shared.Return(objects, true);
			SyncContextBuilder.FinalizeJniInvocation(throwable, interopCache);
		}
		JniObjectReference throwableRef = JniEnvironment.Exceptions.ExceptionOccurred();
		throw new JavaException(ref throwableRef, JniObjectReferenceOptions.CopyAndDispose);
	}
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	public void Invoke<TState>(TState state, AndroidJniAction<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
		INativeThread env = host.GetNativeThread();
		Int32 objectCount = this._objects.Length;
		JLocalObject?[] objects = objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(objectCount) : [];
		JavaInteropCache? interopCache = objectCount > 0 ? new(host, env) : default;
		JGlobalBase? throwable = default;
		try
		{
			AndroidJniContext context = new(env)
			{
				Booleans = this._booleans,
				Bytes = this._bytes,
				Chars = this._chars,
				Doubles = this._doubles,
				Floats = this._floats,
				Ints = this._ints,
				Longs = this._longs,
				Shorts = this._shorts,
				Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
			};
			using InlineCache _ = new(env);
			action(context, state);
			return;
		}
		catch (ThrowableException e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
			throwable = e.GlobalThrowable;
		}
		catch (Exception e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
		}
		finally
		{
			ArrayPool<JLocalObject?>.Shared.Return(objects, true);
			SyncContextBuilder.FinalizeJniInvocation(throwable, interopCache);
		}
		JniObjectReference throwableRef = JniEnvironment.Exceptions.ExceptionOccurred();
		throw new JavaException(ref throwableRef, JniObjectReferenceOptions.CopyAndDispose);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	public TResult Invoke<TResult>(AndroidJniFunc<TResult> func)
	{
		IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
		INativeThread env = host.GetNativeThread();
		Int32 objectCount = this._objects.Length;
		JLocalObject?[] objects = objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(objectCount) : [];
		JavaInteropCache? interopCache = objectCount > 0 ? new(host, env) : default;
		JGlobalBase? throwable = default;
		try
		{
			AndroidJniContext context = new(env)
			{
				Booleans = this._booleans,
				Bytes = this._bytes,
				Chars = this._chars,
				Doubles = this._doubles,
				Floats = this._floats,
				Ints = this._ints,
				Longs = this._longs,
				Shorts = this._shorts,
				Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
			};
			using InlineCache _ = new(env);
			return func(context);
		}
		catch (ThrowableException e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
			throwable = e.GlobalThrowable;
		}
		catch (Exception e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
		}
		finally
		{
			ArrayPool<JLocalObject?>.Shared.Return(objects, true);
			SyncContextBuilder.FinalizeJniInvocation(throwable, interopCache);
		}
		JniObjectReference throwableRef = JniEnvironment.Exceptions.ExceptionOccurred();
		throw new JavaException(ref throwableRef, JniObjectReferenceOptions.CopyAndDispose);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	public TResult Invoke<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		AndroidJniFunc<JReferenceObject?> func) where TResult : class, IJavaPeerable
	{
		IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
		INativeThread env = host.GetNativeThread();
		Int32 objectCount = this._objects.Length;
		JLocalObject?[] objects = objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(objectCount) : [];
		JavaInteropCache? interopCache = objectCount > 0 ? new(host, env) : default;
		JGlobalBase? throwable = default;
		try
		{
			AndroidJniContext context = new(env)
			{
				Booleans = this._booleans,
				Bytes = this._bytes,
				Chars = this._chars,
				Doubles = this._doubles,
				Floats = this._floats,
				Ints = this._ints,
				Longs = this._longs,
				Shorts = this._shorts,
				Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
			};
			using InlineCache _ = new(env);
			JReferenceObject? jObject = func(context);
			return jObject.ToJniObject<TResult>()!;
		}
		catch (ThrowableException e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
			throwable = e.GlobalThrowable;
		}
		catch (Exception e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
		}
		finally
		{
			ArrayPool<JLocalObject?>.Shared.Return(objects, true);
			SyncContextBuilder.FinalizeJniInvocation(throwable, interopCache);
		}
		JniObjectReference throwableRef = JniEnvironment.Exceptions.ExceptionOccurred();
		throw new JavaException(ref throwableRef, JniObjectReferenceOptions.CopyAndDispose);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	public TResult Invoke<TState, TResult>(TState state, AndroidJniFunc<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
		INativeThread env = host.GetNativeThread();
		Int32 objectCount = this._objects.Length;
		JLocalObject?[] objects = objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(objectCount) : [];
		JavaInteropCache? interopCache = objectCount > 0 ? new(host, env) : default;
		JGlobalBase? throwable = default;
		try
		{
			AndroidJniContext context = new(env)
			{
				Booleans = this._booleans,
				Bytes = this._bytes,
				Chars = this._chars,
				Doubles = this._doubles,
				Floats = this._floats,
				Ints = this._ints,
				Longs = this._longs,
				Shorts = this._shorts,
				Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
			};
			using InlineCache _ = new(env);
			return func(context, state);
		}
		catch (ThrowableException e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
			throwable = e.GlobalThrowable;
		}
		catch (Exception e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
		}
		finally
		{
			ArrayPool<JLocalObject?>.Shared.Return(objects, true);
			SyncContextBuilder.FinalizeJniInvocation(throwable, interopCache);
		}
		JniObjectReference throwableRef = JniEnvironment.Exceptions.ExceptionOccurred();
		throw new JavaException(ref throwableRef, JniObjectReferenceOptions.CopyAndDispose);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	public TResult Invoke<TState, [DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		TState state, AndroidJniFunc<TState, JReferenceObject?> func) where TResult : class, IJavaPeerable
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
		INativeThread env = host.GetNativeThread();
		Int32 objectCount = this._objects.Length;
		JLocalObject?[] objects = objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(objectCount) : [];
		JavaInteropCache? interopCache = objectCount > 0 ? new(host, env) : default;
		JGlobalBase? throwable = default;
		try
		{
			AndroidJniContext context = new(env)
			{
				Booleans = this._booleans,
				Bytes = this._bytes,
				Chars = this._chars,
				Doubles = this._doubles,
				Floats = this._floats,
				Ints = this._ints,
				Longs = this._longs,
				Shorts = this._shorts,
				Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
			};
			using InlineCache _ = new(env);
			JReferenceObject? jObject = func(context, state);
			return jObject.ToJniObject<TResult>()!;
		}
		catch (ThrowableException e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
			throwable = e.GlobalThrowable;
		}
		catch (Exception e)
		{
			JTrace.UnhandledException(e, false);
			if (!JniEnvironment.Exceptions.ExceptionCheck()) throw; // JNI exception is not captured by Java.Interop.
		}
		finally
		{
			ArrayPool<JLocalObject?>.Shared.Return(objects, true);
			SyncContextBuilder.FinalizeJniInvocation(throwable, interopCache);
		}
		JniObjectReference throwableRef = JniEnvironment.Exceptions.ExceptionOccurred();
		throw new JavaException(ref throwableRef, JniObjectReferenceOptions.CopyAndDispose);
	}
}