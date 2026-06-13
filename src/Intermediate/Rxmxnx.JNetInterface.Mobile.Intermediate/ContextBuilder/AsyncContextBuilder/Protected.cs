namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	private partial class TaskState
	{
		/// <summary>
		/// Action invocation.
		/// </summary>
		/// <typeparam name="TState">Type of state object</typeparam>
		/// <param name="state">A state instance object.</param>
		/// <param name="env">A <see cref="INativeThread"/> instance.</param>
		protected void Invoke<TState>(INativeThread env, in TState state)
		{
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
		/// <typeparam name="TState">Type of state object</typeparam>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <param name="env">A <see cref="INativeThread"/> instance.</param>
		/// <param name="state">A state instance object.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		protected TResult Invoke<TState, TResult>(INativeThread env, in TState state)
		{
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
		/// JNI function invocation.
		/// </summary>
		/// <typeparam name="TState">Type of state object</typeparam>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <param name="env">A <see cref="INativeThread"/> instance.</param>
		/// <param name="state">A state instance object.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		protected TResult InvokeJni<TState,
			[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(INativeThread env,
			in TState state) where TResult : class, IJavaPeerable
		{
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
	}
}