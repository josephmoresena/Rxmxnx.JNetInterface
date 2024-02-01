namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This struct contains a handler for a primitive sequence.
/// </summary>
internal readonly struct JNativeMemoryHandler : INativeMemoryHandle
{
	/// <summary>
	/// Indicates whether current sequence is a copy.
	/// </summary>
	public Boolean Copy { get; private init; }
	/// <summary>
	/// Indicates whether current sequence is critical.
	/// </summary>
	public Boolean Critical { get; private init; }

	/// <summary>
	/// Pointer to beginning element in current sequence.
	/// </summary>
	public IntPtr Pointer { get; init; }
	/// <summary>
	/// Source object.
	/// </summary>
	private JReferenceObject? Source { get; init; }
	/// <summary>
	/// Release delegate.
	/// </summary>
	private Action<IVirtualMachine, JNativeMemoryHandler, JReleaseMode>? ReleaseAction { get; init; }
	/// <summary>
	/// Binary sequence length.
	/// </summary>
	public Int32 BinarySize { get; init; }
	/// <summary>
	/// Indicates whether current sequence source should be disposed.
	/// </summary>
	private Boolean Disposable { get; init; }

	/// <summary>
	/// Retrieves a <see cref="IReadOnlyFixedContext{Byte}.IDisposable"/> instance from current handle
	/// using <paramref name="jMemory"/>.
	/// </summary>
	/// <param name="jMemory">A <see cref="JNativeMemory"/> instance.</param>
	/// <returns>A <see cref="IReadOnlyFixedContext{Byte}.IDisposable"/> instance.</returns>
	public IReadOnlyFixedContext<Byte>.IDisposable GetReadOnlyContext(JNativeMemory jMemory)
	{
		ReadOnlyValPtr<Byte> ptr = (ReadOnlyValPtr<Byte>)this.Pointer;
		return ptr.GetUnsafeFixedContext(this.BinarySize, jMemory);
	}
	/// <summary>
	/// Retrieves a <see cref="IFixedContext{Byte}.IDisposable"/> instance from current handle
	/// using <paramref name="jMemory"/>.
	/// </summary>
	/// <param name="jMemory">A <see cref="JNativeMemory"/> instance.</param>
	/// <returns>A <see cref="IFixedContext{Byte}.IDisposable"/> instance.</returns>
	public IFixedContext<Byte>.IDisposable GetContext(JNativeMemory jMemory)
	{
		ValPtr<Byte> ptr = (ValPtr<Byte>)this.Pointer;
		return ptr.GetUnsafeFixedContext(this.BinarySize, jMemory);
	}

	/// <summary>
	/// Releases the current handler.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="mode">Release handler mode.</param>
	public void Release(IVirtualMachine vm, JReleaseMode mode = default)
	{
		this.ReleaseAction?.Invoke(vm, this, mode);
		if (this.Disposable) (this.Source as IDisposable)?.Dispose();
	}

	/// <summary>
	/// Creates a memory handler for array source.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> element.</typeparam>
	/// <param name="source"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="JNativeMemoryHandler"/> instance.</returns>
	public static JNativeMemoryHandler CreateHandler<TPrimitive>(JArrayObject<TPrimitive> source, Boolean critical)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment environment = source.Environment;
		Boolean isCopy = false;
		IntPtr pointer = !critical ?
			environment.ArrayFeature.GetSequence(source, out isCopy) :
			environment.ArrayFeature.GetCriticalSequence(source);
		return new()
		{
			Source = source,
			BinarySize = source.Length * IPrimitiveType.GetMetadata<TPrimitive>().SizeOf,
			Disposable = false,
			Critical = critical,
			Pointer = pointer,
			Copy = isCopy,
			ReleaseAction =
				!critical ?
					JNativeMemoryHandler.ReleaseArray<TPrimitive> :
					JNativeMemoryHandler.ReleaseCriticalArray<TPrimitive>,
		};
	}

	/// <summary>
	/// Creates a global memory handler for array source.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> element.</typeparam>
	/// <param name="source"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="JNativeMemoryHandler"/> instance.</returns>
	public static JNativeMemoryHandler
		CreateGlobalHandler<TPrimitive>(JArrayObject<TPrimitive> source, Boolean critical)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> JNativeMemoryHandler.CreateHandler<JGlobal, TPrimitive>(source, critical);

	/// <summary>
	/// Creates a weak memory handler for array source.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> element.</typeparam>
	/// <param name="source"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="JNativeMemoryHandler"/> instance.</returns>
	public static JNativeMemoryHandler CreateWeakHandler<TPrimitive>(JArrayObject<TPrimitive> source, Boolean critical)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> JNativeMemoryHandler.CreateHandler<JWeak, TPrimitive>(source, critical);

	/// <summary>
	/// Creates a global memory handler for array source.
	/// </summary>
	/// <typeparam name="TGlobal">The type of global object.</typeparam>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> element.</typeparam>
	/// <param name="source"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="JNativeMemoryHandler"/> instance.</returns>
	private static JNativeMemoryHandler CreateHandler<TGlobal, TPrimitive>(JArrayObject<TPrimitive> source,
		Boolean critical) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive> where TGlobal : JGlobalBase
	{
		IEnvironment environment = source.Environment;
		JGlobalBase globalSource = environment.ReferenceFeature.Create<TGlobal>(source);
		JArrayObject<TPrimitive> tempSource = new(environment, globalSource);
		Boolean isCopy = false;
		IntPtr pointer = !critical ?
			environment.ArrayFeature.GetSequence(tempSource, out isCopy) :
			environment.ArrayFeature.GetCriticalSequence(tempSource);
		return new()
		{
			Source = globalSource,
			BinarySize = tempSource.Length * IPrimitiveType.GetMetadata<TPrimitive>().SizeOf,
			Disposable = true,
			Critical = critical,
			Pointer = pointer,
			Copy = isCopy,
			ReleaseAction =
				!critical ?
					JNativeMemoryHandler.ReleaseArray<TPrimitive> :
					JNativeMemoryHandler.ReleaseCriticalArray<TPrimitive>,
		};
	}

	/// <summary>
	/// Release array elements pointer.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> element.</typeparam>
	/// <param name="virtualMachine">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler">A <see cref="JNativeMemoryHandler"/> instance.</param>
	/// <param name="mode">Release mode.</param>
	private static void ReleaseArray<TPrimitive>(IVirtualMachine virtualMachine, JNativeMemoryHandler handler,
		JReleaseMode mode) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		if (handler.Source is null) return;
		using IThread thread = virtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
		JArrayObject<TPrimitive> jArray =
			handler.Source as JArrayObject<TPrimitive> ?? new(thread, (JGlobalBase)handler.Source);
		thread.ArrayFeature.ReleaseSequence(jArray, handler.Pointer, mode);
	}
	/// <summary>
	/// Release array elements pointer.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> element.</typeparam>
	/// <param name="virtualMachine">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler">A <see cref="JNativeMemoryHandler"/> instance.</param>
	/// <param name="_">Release mode.</param>
	private static void ReleaseCriticalArray<TPrimitive>(IVirtualMachine virtualMachine, JNativeMemoryHandler handler,
		JReleaseMode _) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		if (handler.Source is null) return;
		using IThread thread = virtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
		JArrayObject<TPrimitive> jArray =
			handler.Source as JArrayObject<TPrimitive> ?? new(thread, (JGlobalBase)handler.Source);
		thread.ArrayFeature.ReleaseCriticalSequence(jArray, handler.Pointer);
	}
}