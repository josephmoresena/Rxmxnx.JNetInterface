namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
	/// <summary>
	/// Executes the specified JNI action in a controlled context.
	/// </summary>
	/// <typeparam name="TAction">The type of the action to execute.</typeparam>
	/// <param name="action">The JNI action instance to be executed by reference.</param>
	/// <exception cref="JavaException">Thrown when a JNI exception occurs during execution.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void ActionInvoke<TAction>(ref TAction action)
#if !NET9_0_OR_GREATER
		where TAction : IAndroidNativeAction
#else
		where TAction : IAndroidNativeAction, allows ref struct
#endif
	{
		Debug.Assert(typeof(TAction).IsValueType);
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
			action.Accept(context);
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
	/// Invokes a given function within the current JNI context and returns the result.
	/// </summary>
	/// <typeparam name="TResult">The type of the result returned by the function.</typeparam>
	/// <typeparam name="TFunction">The type of the function to be invoked.</typeparam>
	/// <param name="func">A reference to the function to be invoked.</param>
	/// <returns>The result of the invoked function.</returns>
	/// <exception cref="JavaException">Thrown if a JNI exception occurs during the function invocation.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private TResult FunctionInvoke<TResult, TFunction>(ref TFunction func)
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<TResult>
#else
		where TFunction : IAndroidNativeFunction<TResult>, allows ref struct
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
			return func.Apply(context);
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