namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	private partial class TaskState
	{
		/// <summary>
		/// Action invocation.
		/// </summary>
		protected void Invoke<TState>(in TState state)
		{
			using IThread __ = this.CreateThread(out INativeThread env);
			JLocalObject?[] objects =
				this._objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(this._objectCount) : [];
			JavaInteropCache? interopCache = this._objectCount > 0 ? new(this._host, env) : default;
			try
			{
				AndroidJniContext context = new(env)
				{
					Booleans = TaskState.GetPrimitiveSpan<JBoolean>(this._booleans),
					Bytes = TaskState.GetPrimitiveSpan<JByte>(this._bytes),
					Chars = TaskState.GetPrimitiveSpan<JChar>(this._chars),
					Doubles = TaskState.GetPrimitiveSpan<JDouble>(this._doubles),
					Floats = TaskState.GetPrimitiveSpan<JFloat>(this._floats),
					Ints = TaskState.GetPrimitiveSpan<JInt>(this._ints),
					Longs = TaskState.GetPrimitiveSpan<JLong>(this._longs),
					Shorts = TaskState.GetPrimitiveSpan<JShort>(this._shorts),
					Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
				};
				using InlineCache _ = new(env);
				((AndroidJniAction<TState>)this._call)(context, state);
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				interopCache?.Dispose();
			}
		}
		/// <summary>
		/// Function invocation.
		/// </summary>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		protected TResult Invoke<TState, TResult>(in TState state)
		{
			using IThread __ = this.CreateThread(out INativeThread env);
			JLocalObject?[] objects =
				this._objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(this._objectCount) : [];
			JavaInteropCache? interopCache = this._objectCount > 0 ? new(this._host, env) : default;
			try
			{
				AndroidJniContext context = new(env)
				{
					Booleans = TaskState.GetPrimitiveSpan<JBoolean>(this._booleans),
					Bytes = TaskState.GetPrimitiveSpan<JByte>(this._bytes),
					Chars = TaskState.GetPrimitiveSpan<JChar>(this._chars),
					Doubles = TaskState.GetPrimitiveSpan<JDouble>(this._doubles),
					Floats = TaskState.GetPrimitiveSpan<JFloat>(this._floats),
					Ints = TaskState.GetPrimitiveSpan<JInt>(this._ints),
					Longs = TaskState.GetPrimitiveSpan<JLong>(this._longs),
					Shorts = TaskState.GetPrimitiveSpan<JShort>(this._shorts),
					Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
				};
				using InlineCache _ = new(env);
				return ((AndroidJniFunc<TState, TResult>)this._call)(context, state);
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				interopCache?.Dispose();
			}
		}
		/// <summary>
		/// JNI funcion invocation.
		/// </summary>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		protected TResult InvokeJni<TState,
			[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(in TState state)
			where TResult : class, IJavaPeerable
		{
			using IThread __ = this.CreateThread(out INativeThread env);
			JLocalObject?[] objects =
				this._objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(this._objectCount) : [];
			JavaInteropCache? interopCache = this._objectCount > 0 ? new(this._host, env) : default;
			try
			{
				AndroidJniContext context = new(env)
				{
					Booleans = TaskState.GetPrimitiveSpan<JBoolean>(this._booleans),
					Bytes = TaskState.GetPrimitiveSpan<JByte>(this._bytes),
					Chars = TaskState.GetPrimitiveSpan<JChar>(this._chars),
					Doubles = TaskState.GetPrimitiveSpan<JDouble>(this._doubles),
					Floats = TaskState.GetPrimitiveSpan<JFloat>(this._floats),
					Ints = TaskState.GetPrimitiveSpan<JInt>(this._ints),
					Longs = TaskState.GetPrimitiveSpan<JLong>(this._longs),
					Shorts = TaskState.GetPrimitiveSpan<JShort>(this._shorts),
					Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
				};
				using InlineCache _ = new(env);
				JReferenceObject? jObject = ((AndroidJniFunc<TState, JReferenceObject?>)this._call)(context, state);
				return jObject is JGlobal jGlobal ? jGlobal.ToJniObject<TResult>()! : jObject.ToJniObject<TResult>()!;
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				interopCache?.Dispose();
			}
		}

		/// <summary>
		/// Action invocation.
		/// </summary>
		private void Invoke()
		{
			using IThread __ = this.CreateThread(out INativeThread env);
			JLocalObject?[] objects =
				this._objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(this._objectCount) : [];
			JavaInteropCache? interopCache = this._objectCount > 0 ? new(this._host, env) : default;
			try
			{
				AndroidJniContext context = new(env)
				{
					Booleans = TaskState.GetPrimitiveSpan<JBoolean>(this._booleans),
					Bytes = TaskState.GetPrimitiveSpan<JByte>(this._bytes),
					Chars = TaskState.GetPrimitiveSpan<JChar>(this._chars),
					Doubles = TaskState.GetPrimitiveSpan<JDouble>(this._doubles),
					Floats = TaskState.GetPrimitiveSpan<JFloat>(this._floats),
					Ints = TaskState.GetPrimitiveSpan<JInt>(this._ints),
					Longs = TaskState.GetPrimitiveSpan<JLong>(this._longs),
					Shorts = TaskState.GetPrimitiveSpan<JShort>(this._shorts),
					Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
				};
				using InlineCache _ = new(env);
				((AndroidJniAction)this._call)(context);
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				interopCache?.Dispose();
			}
		}
		/// <summary>
		/// Function invocation.
		/// </summary>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		private TResult Invoke<TResult>()
		{
			using IThread __ = this.CreateThread(out INativeThread env);
			JLocalObject?[] objects =
				this._objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(this._objectCount) : [];
			JavaInteropCache? interopCache = this._objectCount > 0 ? new(this._host, env) : default;
			try
			{
				AndroidJniContext context = new(env)
				{
					Booleans = TaskState.GetPrimitiveSpan<JBoolean>(this._booleans),
					Bytes = TaskState.GetPrimitiveSpan<JByte>(this._bytes),
					Chars = TaskState.GetPrimitiveSpan<JChar>(this._chars),
					Doubles = TaskState.GetPrimitiveSpan<JDouble>(this._doubles),
					Floats = TaskState.GetPrimitiveSpan<JFloat>(this._floats),
					Ints = TaskState.GetPrimitiveSpan<JInt>(this._ints),
					Longs = TaskState.GetPrimitiveSpan<JLong>(this._longs),
					Shorts = TaskState.GetPrimitiveSpan<JShort>(this._shorts),
					Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
				};
				using InlineCache _ = new(env);
				return ((AndroidJniFunc<TResult>)this._call)(context);
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				interopCache?.Dispose();
			}
		}
		/// <summary>
		/// JNI funcion invocation.
		/// </summary>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		private TResult InvokeJni<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>()
			where TResult : class, IJavaPeerable
		{
			using IThread __ = this.CreateThread(out INativeThread env);
			JLocalObject?[] objects =
				this._objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(this._objectCount) : [];
			JavaInteropCache? interopCache = this._objectCount > 0 ? new(this._host, env) : default;
			try
			{
				AndroidJniContext context = new(env)
				{
					Booleans = TaskState.GetPrimitiveSpan<JBoolean>(this._booleans),
					Bytes = TaskState.GetPrimitiveSpan<JByte>(this._bytes),
					Chars = TaskState.GetPrimitiveSpan<JChar>(this._chars),
					Doubles = TaskState.GetPrimitiveSpan<JDouble>(this._doubles),
					Floats = TaskState.GetPrimitiveSpan<JFloat>(this._floats),
					Ints = TaskState.GetPrimitiveSpan<JInt>(this._ints),
					Longs = TaskState.GetPrimitiveSpan<JLong>(this._longs),
					Shorts = TaskState.GetPrimitiveSpan<JShort>(this._shorts),
					Objects = interopCache is not null ? interopCache.RegisterContext(objects, this._objects) : [],
				};
				using InlineCache _ = new(env);
				JReferenceObject? jObject = ((AndroidJniFunc<JReferenceObject?>)this._call)(context);
				return jObject is JGlobal jGlobal ? jGlobal.ToJniObject<TResult>()! : jObject.ToJniObject<TResult>()!;
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				interopCache?.Dispose();
			}
		}

		/// <summary>
		/// Action delegate for <see cref="TaskFactory.StartNew(Action{Object}, Object)"/>.
		/// </summary>
		/// <param name="state">Action state object.</param>
		public static void Invoke(Object? state)
		{
			if (state is not TaskState taskState) return;
			taskState.Invoke();
		}
		/// <summary>
		/// Function delegate for <see cref="TaskFactory.StartNew{TResult}(Func{Object, TResult}, Object)"/>.
		/// </summary>
		/// <param name="state">Function state object.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		public static TResult Invoke<TResult>(Object? state)
			=> state is not TaskState taskState ? default! : taskState.Invoke<TResult>();
		/// <summary>
		/// Function delegate for <see cref="TaskFactory.StartNew{TResult}(Func{Object, TResult}, Object)"/>.
		/// </summary>
		/// <param name="state">Function state object.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		public static TResult
			InvokeJni<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(Object? state)
			where TResult : class, IJavaPeerable
			=> state is not TaskState taskState ? default! : taskState.InvokeJni<TResult>();
	}

	private partial class TaskState<TState>
	{
		/// <summary>
		/// Action delegate for <see cref="TaskFactory.StartNew(Action{Object}, Object)"/>.
		/// </summary>
		/// <param name="state">Action state object.</param>
		public new static void Invoke(Object? state)
		{
			if (state is not TaskState<TState> taskState) return;
			taskState.Invoke(in taskState._state);
		}
		/// <summary>
		/// Function delegate for <see cref="TaskFactory.StartNew{TResult}(Func{Object, TResult}, Object)"/>.
		/// </summary>
		/// <param name="state">Function state object.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		public new static TResult Invoke<TResult>(Object? state)
			=> state is not TaskState<TState> taskState ?
				default! :
				taskState.Invoke<TState, TResult>(in taskState._state);
		/// <summary>
		/// Function delegate for <see cref="TaskFactory.StartNew{TResult}(Func{Object, TResult}, Object)"/>.
		/// </summary>
		/// <param name="state">Function state object.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		public new static TResult
			InvokeJni<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(Object? state)
			where TResult : class, IJavaPeerable
			=> state is not TaskState<TState> taskState ?
				default! :
				taskState.InvokeJni<TState, TResult>(in taskState._state);
	}
}