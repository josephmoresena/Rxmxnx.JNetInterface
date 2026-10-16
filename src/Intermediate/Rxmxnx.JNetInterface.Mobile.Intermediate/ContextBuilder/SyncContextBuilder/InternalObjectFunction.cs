namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
	/// <summary>
	/// Invokes a Java object function using the specified native function and returns its result.
	/// </summary>
	/// <typeparam name="TResult">The expected result type of the invoked Java object function.</typeparam>
	/// <typeparam name="TFunction">The type of native function being invoked.</typeparam>
	/// <param name="func">The native function to be invoked.</param>
	/// <returns>The result of the invoked Java object function.</returns>
	/// <exception cref="JavaException">Thrown when an error occurs during the invocation of the function.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal TResult? ObjectFunctionInvoke<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult,
		TFunction>(ref TFunction func) where TResult : class, IJavaPeerable
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<JReferenceObject?>
#else
		where TFunction : IAndroidNativeFunction<JReferenceObject?>, allows ref struct
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
			JReferenceObject? jObject = func.Apply(context);
			return jObject.ToJniObject<TResult>();
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
#pragma warning disable CS8500
	/// <summary>
	/// Executes a specified function within a given <paramref name="context"/> and retrieves the resulting value.
	/// </summary>
	/// <typeparam name="TResult">The type of the result, which must implement <see cref="IJavaPeerable"/>.</typeparam>
	/// <typeparam name="TFunction">The type of the function to be executed, which must implement <see cref="IAndroidNativeFunction{TOutput}"/>.</typeparam>
	/// <param name="builder">Current builder instance.</param>
	/// <param name="context">An instance of <see cref="SynchronizationContext"/> used to execute the function.</param>
	/// <param name="func">A reference to the function to be executed.</param>
	/// <param name="result">The output object of type <typeparamref name="TResult"/> produced by the execution of <paramref name="func"/>.</param>
	/// <exception cref="Exception">Thrown if an error occurs during the execution of the function.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	internal static unsafe void
		ObjectFunctionSend<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult, TFunction>(
			in SyncContextBuilder builder, SynchronizationContext context, ref TFunction func, out TResult? result)
		where TResult : class, IJavaPeerable
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<JReferenceObject?>
#else
		where TFunction : IAndroidNativeFunction<JReferenceObject?>, allows ref struct
#endif
	{
		Exception? ex;
		fixed (TFunction* funcPtr = &func)
		fixed (SyncContextBuilder* builderPtr = &builder)
		{
			SendObjectFunctionState<TResult, TFunction> state = new(builderPtr) { FunctionPointer = funcPtr, };
			context.Send(SendState.Send, state);
			result = state.Result;
			ex = state.Exception;
		}
		if (ex is not null) throw ex;
	}
#pragma warning restore CS8500
}