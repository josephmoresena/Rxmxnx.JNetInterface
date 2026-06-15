namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	private partial class TaskState
	{
		/// <summary>
		/// Action invocation.
		/// </summary>
		/// <param name="env">A <see cref="INativeThread"/> instance.</param>
		public virtual void Invoke(INativeThread env)
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
					Objects = interopCache is not null ?
						interopCache.RegisterContext(objects, this._objects.AsSpan()[..this._objectCount]) :
						[],
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
				ArrayPool<JLocalObject?>.Shared.Return(objects, true);
				interopCache?.Dispose();
			}
		}
		/// <summary>
		/// Function invocation.
		/// </summary>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <param name="env">A <see cref="INativeThread"/> instance.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		public virtual TResult Invoke<TResult>(INativeThread env)
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
					Objects = interopCache is not null ?
						interopCache.RegisterContext(objects, this._objects.AsSpan()[..this._objectCount]) :
						[],
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
				ArrayPool<JLocalObject?>.Shared.Return(objects, true);
				interopCache?.Dispose();
			}
		}
		/// <summary>
		/// JNI function invocation.
		/// </summary>
		/// <typeparam name="TResult">Type of result object</typeparam>
		/// <param name="env">A <see cref="INativeThread"/> instance.</param>
		/// <returns>A <typeparamref name="TResult"/> function result.</returns>
		public virtual TResult
			InvokeJni<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(INativeThread env)
			where TResult : class, IJavaPeerable
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
					Objects = interopCache is not null ?
						interopCache.RegisterContext(objects, this._objects.AsSpan()[..this._objectCount]) :
						[],
				};
				using InlineCache _ = new(env);
				JReferenceObject? jObject = ((AndroidJniFunc<JReferenceObject?>)this._call)(context);
				return jObject.ToJniObject<TResult>()!;
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				ArrayPool<JLocalObject?>.Shared.Return(objects, true);
				interopCache?.Dispose();
			}
		}
	}

	private sealed partial class TaskState<TState>
	{
		/// <inheritdoc cref="TaskState.Invoke(INativeThread)"/>
		public override void Invoke(INativeThread env) => base.Invoke(env, this._state);
		/// <inheritdoc cref="TaskState.Invoke{TResult}(INativeThread)"/>
		public override TResult Invoke<TResult>(INativeThread env) => base.Invoke<TState, TResult>(env, this._state);
		/// <inheritdoc cref="TaskState.InvokeJni{TResult}(INativeThread)"/>
		public override TResult
			InvokeJni<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(INativeThread env)
			=> base.InvokeJni<TState, TResult>(env, this._state);
	}
}