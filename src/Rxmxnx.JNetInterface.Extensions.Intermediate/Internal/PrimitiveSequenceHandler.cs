namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This struct contains a handler for a primitive sequence.
/// </summary>
internal readonly struct PrimitiveSequenceHandler
{
	/// <summary>
	/// Pointer to beginning element in current sequence.
	/// </summary>
	public IntPtr Pointer { get; private init; }
	/// <summary>
	/// Number of elements in current sequence.
	/// </summary>
	public Int32 Count { get; private init; }
	/// <summary>
	/// Indicates whether current sequence is a copy.
	/// </summary>
	public Boolean Copy { get; private init; }
	/// <summary>
	/// Indicates whether current sequence source should be disposed.
	/// </summary>
	public Boolean Disposable { get; private init; }
	/// <summary>
	/// Indicates whether current sequence is critical.
	/// </summary>
	public Boolean Critical { get; private init; }
	/// <summary>
	/// Binary sequence length.
	/// </summary>
	internal Int32 BinarySize { get; private init; }

	/// <summary>
	/// Source object.
	/// </summary>
	private JReferenceObject? Source { get; init; }
	/// <summary>
	/// Release delegate.
	/// </summary>
	private Action<IVirtualMachine, PrimitiveSequenceHandler, JReleaseMode>? ReleaseAction { get; init; }

	/// <summary>
	/// Releases the current handler.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="mode">Release handler mode.</param>
	public void Release(IVirtualMachine vm, JReleaseMode mode = default) { this.ReleaseAction?.Invoke(vm, this, mode); }

	/// <summary>
	/// Creates a sequence handler for array source.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
	/// <param name="source"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler CreateHandler<TPrimitive>(JArrayObject<TPrimitive> source, Boolean critical)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment environment = source.Environment;
		Boolean isCopy = false;
		IntPtr pointer = !critical ?
			environment.ArrayProvider.GetSequence(source, out isCopy) :
			environment.ArrayProvider.GetCriticalSequence(source);
		return new()
		{
			Source = source,
			Count = source.Length,
			BinarySize = source.Length * IPrimitiveType.GetMetadata<TPrimitive>().SizeOf,
			Disposable = false,
			Critical = critical,
			Pointer = pointer,
			Copy = isCopy,
			ReleaseAction =
				!critical ?
					PrimitiveSequenceHandler.ReleaseArray<TPrimitive> :
					PrimitiveSequenceHandler.ReleaseCriticalArray<TPrimitive>,
		};
	}
	/// <summary>
	/// Creates a sequence handler for string source.
	/// </summary>
	/// <param name="source"><see cref="JStringObject"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler CreateHandler(JStringObject source, Boolean critical)
	{
		IEnvironment environment = source.Environment;
		Boolean isCopy = false;
		IntPtr pointer = !critical ?
			environment.StringProvider.GetSequence(source, out isCopy) :
			environment.StringProvider.GetCriticalSequence(source);
		return new()
		{
			Source = source,
			Count = source.Length,
			BinarySize = source.Length * IPrimitiveType.GetMetadata<JChar>().SizeOf,
			Disposable = false,
			Critical = critical,
			Pointer = pointer,
			Copy = isCopy,
			ReleaseAction =
				!critical ? PrimitiveSequenceHandler.ReleaseString : PrimitiveSequenceHandler.ReleaseCriticalString,
		};
	}
	/// <summary>
	/// Creates a sequence handler for string source.
	/// </summary>
	/// <param name="source"><see cref="JStringObject"/> instance.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler CreateUtf8Handler(JStringObject source)
	{
		IEnvironment environment = source.Environment;
		IntPtr pointer = environment.StringProvider.GetUtf8Sequence(source, out Boolean isCopy);
		return new()
		{
			Source = source,
			Count = source.Utf8Length,
			BinarySize = source.Utf8Length,
			Disposable = false,
			Critical = false,
			Pointer = pointer,
			Copy = isCopy,
			ReleaseAction = PrimitiveSequenceHandler.ReleaseUtf8String,
		};
	}
	/// <summary>
	/// Creates a global sequence handler for string source.
	/// </summary>
	/// <param name="source"><see cref="JStringObject"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler CreateGlobalHandler(JStringObject source, Boolean critical)
		=> PrimitiveSequenceHandler.CreateHandler<JGlobal>(source, critical);
	/// <summary>
	/// Creates a global sequence handler for array source.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
	/// <param name="source"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler
		CreateGlobalHandler<TPrimitive>(JArrayObject<TPrimitive> source, Boolean critical)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveSequenceHandler.CreateHandler<JGlobal, TPrimitive>(source, critical);
	/// <summary>
	/// Creates a weak sequence handler for string source.
	/// </summary>
	/// <param name="source"><see cref="JStringObject"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler CreateWeakHandler(JStringObject source, Boolean critical)
		=> PrimitiveSequenceHandler.CreateHandler<JWeak>(source, critical);
	/// <summary>
	/// Creates a weak sequence handler for array source.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
	/// <param name="source"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler
		CreateWeakHandler<TPrimitive>(JArrayObject<TPrimitive> source, Boolean critical)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveSequenceHandler.CreateHandler<JWeak, TPrimitive>(source, critical);
	/// <summary>
	/// Creates a UTF-8 global sequence handler for string source.
	/// </summary>
	/// <param name="source"><see cref="JStringObject"/> instance.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler CreateUtf8GlobalHandler(JStringObject source)
		=> PrimitiveSequenceHandler.CreateUtf8Handler<JGlobal>(source);
	/// <summary>
	/// Creates a UTF-8 weak sequence handler for string source.
	/// </summary>
	/// <param name="source"><see cref="JStringObject"/> instance.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	public static PrimitiveSequenceHandler CreateUtf8WeakHandler(JStringObject source)
		=> PrimitiveSequenceHandler.CreateUtf8Handler<JWeak>(source);

	/// <summary>
	/// Creates a global sequence handler for string source.
	/// </summary>
	/// <typeparam name="TGlobal">The type of global object.</typeparam>
	/// <param name="source"><see cref="JStringObject"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	private static PrimitiveSequenceHandler CreateHandler<TGlobal>(JStringObject source, Boolean critical)
		where TGlobal : JGlobalBase
	{
		IEnvironment environment = source.Environment;
		JGlobalBase globalSource = environment.ReferenceProvider.Create<TGlobal>(source);
		JStringObject tempSource = new(environment, globalSource);
		Boolean isCopy = false;
		IntPtr pointer = !critical ?
			environment.StringProvider.GetSequence(tempSource, out isCopy) :
			environment.StringProvider.GetCriticalSequence(tempSource);
		return new()
		{
			Source = globalSource,
			Count = tempSource.Length,
			BinarySize = source.Length * IPrimitiveType.GetMetadata<JChar>().SizeOf,
			Disposable = true,
			Critical = critical,
			Pointer = pointer,
			Copy = isCopy,
			ReleaseAction =
				!critical ? PrimitiveSequenceHandler.ReleaseString : PrimitiveSequenceHandler.ReleaseCriticalString,
		};
	}
	/// <summary>
	/// Creates a global sequence handler for array source.
	/// </summary>
	/// <typeparam name="TGlobal">The type of global object.</typeparam>
	/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
	/// <param name="source"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="critical">Indicates the handler is for a critical sequence.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	private static PrimitiveSequenceHandler CreateHandler<TGlobal, TPrimitive>(JArrayObject<TPrimitive> source,
		Boolean critical) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive> where TGlobal : JGlobalBase
	{
		IEnvironment environment = source.Environment;
		JGlobalBase globalSource = environment.ReferenceProvider.Create<TGlobal>(source);
		JArrayObject<TPrimitive> tempSource = new(environment, globalSource);
		Boolean isCopy = false;
		IntPtr pointer = !critical ?
			environment.ArrayProvider.GetSequence(tempSource, out isCopy) :
			environment.ArrayProvider.GetCriticalSequence(tempSource);
		return new()
		{
			Source = globalSource,
			Count = tempSource.Length,
			BinarySize = tempSource.Length * IPrimitiveType.GetMetadata<TPrimitive>().SizeOf,
			Disposable = true,
			Critical = critical,
			Pointer = pointer,
			Copy = isCopy,
			ReleaseAction =
				!critical ?
					PrimitiveSequenceHandler.ReleaseArray<TPrimitive> :
					PrimitiveSequenceHandler.ReleaseCriticalArray<TPrimitive>,
		};
	}
	/// <summary>
	/// Creates a UTF-8 global sequence handler for string source.
	/// </summary>
	/// <typeparam name="TGlobal">The type of global object.</typeparam>
	/// <param name="source"><see cref="JStringObject"/> instance.</param>
	/// <returns><see cref="PrimitiveSequenceHandler"/> instance.</returns>
	private static PrimitiveSequenceHandler CreateUtf8Handler<TGlobal>(JStringObject source) where TGlobal : JGlobalBase
	{
		IEnvironment environment = source.Environment;
		JGlobalBase globalSource = environment.ReferenceProvider.Create<TGlobal>(source);
		JStringObject tempSource = new(environment, globalSource);
		IntPtr pointer = environment.StringProvider.GetUtf8Sequence(tempSource, out Boolean isCopy);
		return new()
		{
			Source = globalSource,
			Count = tempSource.Utf8Length,
			BinarySize = tempSource.Utf8Length,
			Disposable = true,
			Critical = false,
			Pointer = pointer,
			Copy = isCopy,
			ReleaseAction = PrimitiveSequenceHandler.ReleaseUtf8String,
		};
	}
	/// <summary>
	/// Release array elements pointer.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
	/// <param name="virtualMachine">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler">A <see cref="PrimitiveSequenceHandler"/> instance.</param>
	/// <param name="mode">Release mode.</param>
	private static void ReleaseArray<TPrimitive>(IVirtualMachine virtualMachine, PrimitiveSequenceHandler handler,
		JReleaseMode mode) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		if (handler.Source is null) return;
		using IThread thread = virtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
		JArrayObject<TPrimitive> jArray =
			handler.Source as JArrayObject<TPrimitive> ?? new(thread, (JGlobalBase)handler.Source);
		thread.ArrayProvider.ReleaseSequence(jArray, handler.Pointer, mode);
	}
	/// <summary>
	/// Release array elements pointer.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
	/// <param name="virtualMachine">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler">A <see cref="PrimitiveSequenceHandler"/> instance.</param>
	/// <param name="_">Release mode.</param>
	private static void ReleaseCriticalArray<TPrimitive>(IVirtualMachine virtualMachine,
		PrimitiveSequenceHandler handler, JReleaseMode _) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		if (handler.Source is null) return;
		using IThread thread = virtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
		JArrayObject<TPrimitive> jArray =
			handler.Source as JArrayObject<TPrimitive> ?? new(thread, (JGlobalBase)handler.Source);
		thread.ArrayProvider.ReleaseCriticalSequence(jArray, handler.Pointer);
	}
	/// <summary>
	/// Release string chars pointer.
	/// </summary>
	/// <param name="virtualMachine">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler">A <see cref="PrimitiveSequenceHandler"/> instance.</param>
	/// <param name="_">Release mode.</param>
	private static void ReleaseString(IVirtualMachine virtualMachine, PrimitiveSequenceHandler handler, JReleaseMode _)
	{
		if (handler.Source is null) return;
		using IThread thread = virtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
		JStringObject jString = handler.Source as JStringObject ?? new(thread, (JGlobalBase)handler.Source);
		thread.StringProvider.ReleaseSequence(jString, handler.Pointer);
	}
	/// <summary>
	/// Release string utf chars pointer.
	/// </summary>
	/// <param name="virtualMachine">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler">A <see cref="PrimitiveSequenceHandler"/> instance.</param>
	/// <param name="_">Release mode.</param>
	private static void ReleaseUtf8String(IVirtualMachine virtualMachine, PrimitiveSequenceHandler handler,
		JReleaseMode _)
	{
		if (handler.Source is null) return;
		using IThread thread = virtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
		JStringObject jString = handler.Source as JStringObject ?? new(thread, (JGlobalBase)handler.Source);
		thread.StringProvider.ReleaseUtf8Sequence(jString, handler.Pointer);
	}
	/// <summary>
	/// Release string critical chars pointer.
	/// </summary>
	/// <param name="virtualMachine">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler">A <see cref="PrimitiveSequenceHandler"/> instance.</param>
	/// <param name="_">Release mode.</param>
	private static void ReleaseCriticalString(IVirtualMachine virtualMachine, PrimitiveSequenceHandler handler,
		JReleaseMode _)
	{
		if (handler.Source is null) return;
		using IThread thread = virtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
		JStringObject jString = handler.Source as JStringObject ?? new(thread, (JGlobalBase)handler.Source);
		thread.StringProvider.ReleaseCriticalSequence(jString, handler.Pointer);
	}
}