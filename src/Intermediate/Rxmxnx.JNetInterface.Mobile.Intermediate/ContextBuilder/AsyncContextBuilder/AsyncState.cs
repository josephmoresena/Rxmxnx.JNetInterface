namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Represents an abstract base class for defining asynchronous states used in the
	/// <see cref="AsyncContextBuilder"/> process.
	/// </summary>
	private abstract class AsyncState : IDisposable
	{
		/// <summary>
		/// Thread creation args.
		/// </summary>
		private readonly ThreadCreationArgs _args;
		/// <inheritdoc cref="AndroidJniContext.Booleans"/>
		private readonly Array? _booleans;
		/// <inheritdoc cref="AndroidJniContext.Bytes"/>
		private readonly Array? _bytes;
		/// <inheritdoc cref="AndroidJniContext.Chars"/>
		private readonly Array? _chars;
		/// <inheritdoc cref="AndroidJniContext.Doubles"/>
		private readonly Array? _doubles;
		/// <inheritdoc cref="AndroidJniContext.Floats"/>
		private readonly Array? _floats;
		/// <summary>
		/// A <see cref="IVirtualMachineHost"/> instance.
		/// </summary>
		private readonly IVirtualMachineHost _host;
		/// <inheritdoc cref="AndroidJniContext.Ints"/>
		private readonly Array? _ints;
		/// <inheritdoc cref="AndroidJniContext.Longs"/>
		private readonly Array? _longs;
		/// <summary>
		/// Number of objects.
		/// </summary>
		private readonly Int32 _objectCount;
		/// <inheritdoc cref="AndroidJniContext.Objects"/>
		private readonly JniObjectInfo[] _objects;
		/// <inheritdoc cref="AndroidJniContext.Shorts"/>
		private readonly Array? _shorts;

		/// <summary>
		/// Indicates whether the objects should be stored as weak references.
		/// </summary>
		protected Boolean UseWeakGlobal { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="host">Current <see cref="IVirtualMachineHost"/> instance.</param>
		/// <param name="builder">Current <see cref="AsyncContextBuilder"/> instance.</param>
		/// <param name="useWeakGlobal">Indicates whether the objects should be stored as weak references.</param>
		/// <param name="isDaemon">Indicates whether the thread should be a daemon thread.</param>
		protected AsyncState(IVirtualMachineHost host, AsyncContextBuilder builder, Boolean useWeakGlobal,
			Boolean isDaemon)
		{
			this._args = new() { Name = builder._threadName, ThreadGroup = builder._threadGroup, IsDaemon = isDaemon, };
			this._booleans = builder._booleans;
			this._bytes = builder._bytes;
			this._chars = builder._chars;
			this._doubles = builder._doubles;
			this._floats = builder._floats;
			this._host = host;
			this._ints = builder._ints;
			this._longs = builder._longs;
			this._objectCount = builder._objects.Length;
			this._objects = AsyncState.CreateObject(builder._objects, useWeakGlobal);
			this._shorts = builder._shorts;
			this.UseWeakGlobal = useWeakGlobal;
		}

		/// <inheritdoc/>
		/// <remarks>This method may attach the current thread using <see cref="JniRuntime"/>.</remarks>
		public void Dispose()
		{
			for (Int32 index = 0; index < this._objectCount; index++)
			{
				if (this._objects[index].Reference == default) continue;
				JniObjectReference.Dispose(ref this._objects[index].Reference);
			}
			ArrayPool<JniObjectInfo>.Shared.Return(this._objects);
		}

		/// <summary>
		/// Executes user-defined asynchronous operations.
		/// </summary>
		/// <param name="env">The <see cref="INativeThread"/> used for context creation.</param>
		public abstract void Invoke(INativeThread env);

		/// <summary>
		/// Initialize a <see cref="IThread"/> instance.
		/// </summary>
		/// <param name="env">Output. A <see cref="INativeThread"/> instance</param>
		/// <returns>Created <see cref="IThread"/> instance.</returns>
		protected IThread CreateThread(out INativeThread env)
		{
			IThread result = !this._args.IsDaemon ?
				this._host.Value.InitializeThread(this._args.Name, this._args.ThreadGroup) :
				this._host.Value.InitializeDaemon(this._args.Name, this._args.ThreadGroup);
			env = this._host.GetNativeThread();
			return result;
		}

		/// <summary>
		/// Creates a new instance of <see cref="AndroidJniContext"/> using the provided
		/// <see cref="INativeThread"/> environment and initializes the Java interop cache.
		/// </summary>
		/// <param name="env">The native thread environment used for context creation.</param>
		/// <param name="interopCache">Output. The required <see cref="JavaInteropCache"/> instance by the context.</param>
		/// <param name="objects">The objects to be managed by the context.</param>
		/// <returns>A newly created <see cref="AndroidJniContext"/> configured.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected AndroidJniContext CreateContext(INativeThread env, out JavaInteropCache? interopCache,
			out JLocalObject?[] objects)
		{
			objects = this._objectCount > 0 ? ArrayPool<JLocalObject?>.Shared.Rent(this._objectCount) : [];
			interopCache = this._objectCount > 0 ? new(this._host, env) : default;
			return new(env)
			{
				Booleans = AsyncState.GetPrimitiveSpan<JBoolean>(this._booleans),
				Bytes = AsyncState.GetPrimitiveSpan<JByte>(this._bytes),
				Chars = AsyncState.GetPrimitiveSpan<JChar>(this._chars),
				Doubles = AsyncState.GetPrimitiveSpan<JDouble>(this._doubles),
				Floats = AsyncState.GetPrimitiveSpan<JFloat>(this._floats),
				Ints = AsyncState.GetPrimitiveSpan<JInt>(this._ints),
				Longs = AsyncState.GetPrimitiveSpan<JLong>(this._longs),
				Shorts = AsyncState.GetPrimitiveSpan<JShort>(this._shorts),
				Objects = interopCache is not null ?
					interopCache.RegisterContext(objects, this._objects.AsSpan()[..this._objectCount]) :
					[],
			};
		}

		/// <summary>
		/// Executes the asynchronous state represented by the specified object and disposes of its resources after
		/// execution.
		/// </summary>
		/// <param name="obj">An object representing the asynchronous state.</param>
		public static void ExecuteAndDispose(Object? obj)
		{
			if (obj is not AsyncState state) return;
			try
			{
				using IThread __ = state.CreateThread(out INativeThread env);
				state.Invoke(env);
			}
			finally
			{
				state.Dispose();
			}
		}

		/// <summary>
		/// Finalizes the provided Java interop cache and returns an array of objects to the shared pool.
		/// </summary>
		/// <param name="interopCache">The <see cref="JavaInteropCache"/> instance to dispose of, if applicable.</param>
		/// <param name="objects">The array of <see cref="JLocalObject"/> instances to be returned to the shared pool.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static void Finalize(JavaInteropCache? interopCache, JLocalObject?[] objects)
		{
			if (objects.Length > 0)
				ArrayPool<JLocalObject?>.Shared.Return(objects, true);
			interopCache?.Dispose();
		}

		/// <summary>
		/// Creates an array of <see cref="JniObjectReference"/> values.
		/// </summary>
		/// <param name="objects">A read-only span of <see cref="IJavaPeerable"/> objects.</param>
		/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
		/// <returns>An array of <see cref="JniObjectReference"/> values.</returns>
		private static JniObjectInfo[] CreateObject(ReadOnlySpan<IJavaPeerable?> objects, Boolean useWeakGlobal)
		{
			if (objects.Length == 0) return [];
			JniObjectInfo[] result = ArrayPool<JniObjectInfo>.Shared.Rent(objects.Length);
			for (Int32 index = 0; index < objects.Length; index++)
			{
				IJavaPeerable? obj = objects[index];
				if (obj is null || !obj.PeerReference.IsValid)
				{
					result[index] = default;
					continue;
				}
				result[index] =
					new(useWeakGlobal ? obj.PeerReference.NewWeakGlobalRef() : obj.PeerReference.NewGlobalRef(),
					    obj.GetJniTypeName());
			}
			return result;
		}
		/// <summary>
		/// Retrieves the primitive read-only array from <paramref name="array"/>.
		/// </summary>
		/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
		/// <param name="array">A <see cref="Array"/> instance.</param>
		/// <returns>A primitive read-only span.</returns>
		private static ReadOnlySpan<TPrimitive> GetPrimitiveSpan<TPrimitive>(Array? array)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			if (array is null || array.Length == 0) return [];
			ref Byte d0 = ref MemoryMarshal.GetArrayDataReference(array);
			ref TPrimitive dt0 = ref Unsafe.As<Byte, TPrimitive>(ref d0);
			return MemoryMarshal.CreateReadOnlySpan(ref dt0, array.Length);
		}
	}

	/// <summary>
	/// Represents an abstract base class for defining asynchronous function states used in the
	/// <see cref="AsyncContextBuilder"/> process.
	/// </summary>
	/// <typeparam name="TResult">The type of the result returned by the asynchronous operation.</typeparam>
	private abstract class AsyncState<TResult> : AsyncState
	{
		/// <summary>
		/// Represents the result of an asynchronous operation.
		/// </summary>
		public TResult? Result { get; protected set; }

		/// <inheritdoc/>
		protected AsyncState(IVirtualMachineHost host, AsyncContextBuilder builder, Boolean useWeakGlobal,
			Boolean isDaemon) : base(host, builder, useWeakGlobal, isDaemon) { }

		/// <inheritdoc cref="AsyncState.ExecuteAndDispose(Object)"/>
		/// <returns>The result of the asynchronous operation.</returns>
		public new static TResult? ExecuteAndDispose(Object? obj)
		{
			if (obj is not AsyncState<TResult> state) return default;
			try
			{
				using IThread __ = state.CreateThread(out INativeThread env);
				state.Invoke(env);
				return state.Result;
			}
			finally
			{
				state.Dispose();
			}
		}
	}
}