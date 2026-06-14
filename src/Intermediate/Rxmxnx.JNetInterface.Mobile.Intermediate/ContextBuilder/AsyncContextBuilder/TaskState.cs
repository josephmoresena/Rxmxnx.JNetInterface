namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// State class for async JNI context.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <param name="builder">A <see cref="AsyncContextBuilder"/> instance.</param>
	/// <param name="call">A <see cref="Delegate"/> instance.</param>
	/// <param name="isDaemon">Indicates whether the current thread is a daemon.</param>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
	                 Justification = CommonConstants.InternalInheritanceJustification)]
#endif
	private partial class TaskState(
		IVirtualMachineHost host,
		AsyncContextBuilder builder,
		Delegate call,
		Boolean isDaemon = false) : IDisposable
	{
		/// <summary>
		/// Thread creation args.
		/// </summary>
		private readonly ThreadCreationArgs _args = new()
		{
			Name = builder._threadName, ThreadGroup = builder._threadGroup, IsDaemon = isDaemon,
		};
		/// <inheritdoc cref="AndroidJniContext.Booleans"/>
		private readonly Array? _booleans = builder._booleans;
		/// <inheritdoc cref="AndroidJniContext.Bytes"/>
		private readonly Array? _bytes = builder._bytes;
		/// <summary>
		/// Delegate to execute.
		/// </summary>
		private readonly Delegate _call = call;
		/// <inheritdoc cref="AndroidJniContext.Chars"/>
		private readonly Array? _chars = builder._chars;
		/// <inheritdoc cref="AndroidJniContext.Doubles"/>
		private readonly Array? _doubles = builder._doubles;
		/// <inheritdoc cref="AndroidJniContext.Floats"/>
		private readonly Array? _floats = builder._floats;
		/// <summary>
		/// A <see cref="IVirtualMachineHost"/> instance.
		/// </summary>
		private readonly IVirtualMachineHost _host = host;
		/// <inheritdoc cref="AndroidJniContext.Ints"/>
		private readonly Array? _ints = builder._ints;
		/// <inheritdoc cref="AndroidJniContext.Longs"/>
		private readonly Array? _longs = builder._longs;
		/// <summary>
		/// Number of objects.
		/// </summary>
		private readonly Int32 _objectCount = builder._objects.Length;
		/// <inheritdoc cref="AndroidJniContext.Objects"/>
		private readonly JniObjectInfo[] _objects = TaskState.CreateObject(builder._objects);
		/// <inheritdoc cref="AndroidJniContext.Shorts"/>
		private readonly Array? _shorts = builder._shorts;

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
		/// Initialize a <see cref="IThread"/> instance.
		/// </summary>
		/// <param name="env">Output. A <see cref="INativeThread"/> instance</param>
		/// <returns>Created <see cref="IThread"/> instance.</returns>
		private IThread CreateThread(out INativeThread env)
		{
			IThread result = !this._args.IsDaemon ?
				this._host.Value.InitializeThread(this._args.Name, this._args.ThreadGroup) :
				this._host.Value.InitializeDaemon(this._args.Name, this._args.ThreadGroup);
			env = this._host.GetNativeThread();
			return result;
		}

		/// <summary>
		/// Creates an array of <see cref="JniObjectReference"/> values.
		/// </summary>
		/// <param name="objects">A read-only span of <see cref="IJavaPeerable"/> objects.</param>
		/// <returns>An array of <see cref="JniObjectReference"/> values.</returns>
		private static JniObjectInfo[] CreateObject(ReadOnlySpan<IJavaPeerable?> objects)
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
				result[index] = new(obj.PeerReference.NewWeakGlobalRef(), obj.GetJniTypeName());
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
	/// State class for async JNI context.
	/// </summary>
	/// <typeparam name="TState">Type of the state object.</typeparam>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <param name="builder">A <see cref="AsyncContextBuilder"/> instance.</param>
	/// <param name="call">A <see cref="Delegate"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="isDaemon">Indicates whether the current thread is a daemon.</param>
	private sealed partial class TaskState<TState>(
		IVirtualMachineHost host,
		AsyncContextBuilder builder,
		Delegate call,
		TState state,
		Boolean isDaemon = false) : TaskState(host, builder, call, isDaemon)
	{
		/// <summary>
		/// Object state.
		/// </summary>
		private readonly TState _state = state;
	}
}