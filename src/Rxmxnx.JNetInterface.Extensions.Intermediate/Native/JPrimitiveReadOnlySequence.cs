namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a primitive values sequence base.
/// </summary>
public abstract partial record JPrimitiveReadOnlySequence : IReadOnlyFixedContext<Byte>, IDisposable
{
	/// <summary>
	/// Indicates current instance is disposed.
	/// </summary>
	private readonly Boolean _disposed = true;
	/// <summary>
	/// Indicates whether the source object should be disposed.
	/// </summary>
	private readonly Boolean _disposeSource;
	/// <inheritdoc cref="JPrimitiveReadOnlySequence.Source"/>
	private readonly JReferenceObject _source;
	/// <inheritdoc cref="JPrimitiveReadOnlySequence.VirtualMachine"/>
	private readonly IVirtualMachine _vm;

	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	public IVirtualMachine VirtualMachine => this._vm;

	/// <summary>
	/// Release mode.
	/// </summary>
	internal JReleaseMode ReleaseMode { get; set; }
	/// <summary>
	/// Indicates whether current sequence is a copy.
	/// </summary>
	public abstract Boolean Copy { get; }
	/// <summary>
	/// Indicates whether current sequence is critical.
	/// </summary>
	public abstract Boolean Critical { get; }

	/// <summary>
	/// Indicates whether current sequence is read-only.
	/// </summary>
	internal virtual Boolean ReadOnly => true;
	/// <summary>
	/// Source object.
	/// </summary>
	internal JReferenceObject Source => this._source;

	/// <summary>
	/// Binary sequence length.
	/// </summary>
	internal abstract Int32 BinarySize { get; }
	/// <summary>
	/// Release delegate.
	/// </summary>
	internal abstract Action<JPrimitiveReadOnlySequence> Release { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="source">Source object.</param>
	internal JPrimitiveReadOnlySequence(JLocalObject source)
	{
		this._vm = source.Environment.VirtualMachine;
		this._disposeSource = false;
		this._source = source;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="source">Source object.</param>
	/// <param name="disposeSource">Indicates whether the source object should be disposed.</param>
	internal JPrimitiveReadOnlySequence(JGlobalBase source, Boolean disposeSource)
	{
		this._vm = source.VirtualMachine;
		this._source = source;
		this._disposeSource = disposeSource;
	}
	/// <inheritdoc/>
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}
	/// <inheritdoc/>
	public ReadOnlySpan<Byte> Bytes => this.Pointer.GetUnsafeSpan<Byte>(this.BinarySize);

	/// <inheritdoc/>
	public abstract IntPtr Pointer { get; }

	ReadOnlySpan<Byte> IReadOnlyFixedMemory<Byte>.Values => this.Bytes;
	IReadOnlyFixedContext<Byte> IReadOnlyFixedMemory.AsBinaryContext() => this;

	/// <inheritdoc/>
	public IReadOnlyFixedContext<TDestination> Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		where TDestination : unmanaged
	{
		IReadOnlyFixedContext<TDestination> result = this as IReadOnlyFixedContext<TDestination> ?? (this.ReadOnly ?
			new ReadOnlyFixedContext<TDestination>(this) :
			new FixedContext<TDestination>(this));
		residual = this.ReadOnly ?
			new ReadOnlyFixedContext<Byte>(this, result.Bytes.Length) :
			new FixedContext<TDestination>(this, result.Bytes.Length);
		return result;
	}

	/// <inheritdoc cref="IDisposable.Dispose"/>
	/// <param name="disposing">Indicates whether current instance is called by <see cref="IDisposable.Dispose"/></param>
	protected virtual void Dispose(Boolean disposing)
	{
		if (this._disposed) return;
		if (this._disposeSource && this._source is IDisposable disposable)
			disposable.Dispose();
		this.Release(this);
	}

	/// <summary>
	/// Release array elements pointer.
	/// </summary>
	/// <param name="sequence">The <see cref="JPrimitiveReadOnlySequence{TPrimitive}"/> instance.</param>
	/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
	internal static void ReleaseArray<TPrimitive>(JPrimitiveReadOnlySequence sequence)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		using IThread thread = sequence.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		JArrayObject<TPrimitive> jArray =
			sequence.Source as JArrayObject<TPrimitive> ?? new(thread, (JGlobalBase)sequence.Source);
		thread.ArrayProvider.ReleaseSequence(jArray, sequence.Pointer, sequence.ReleaseMode);
	}

	/// <summary>
	/// Release array elements pointer.
	/// </summary>
	/// <param name="sequence">The <see cref="JPrimitiveReadOnlySequence{TPrimitive}"/> instance.</param>
	/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
	internal static void ReleaseCriticalArray<TPrimitive>(JPrimitiveReadOnlySequence sequence)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		using IThread thread = sequence.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		JArrayObject<TPrimitive> jArray =
			sequence.Source as JArrayObject<TPrimitive> ?? new(thread, (JGlobalBase)sequence.Source);
		thread.ArrayProvider.ReleaseCriticalSequence(jArray, sequence.Pointer);
	}
	/// <summary>
	/// Release string chars pointer.
	/// </summary>
	/// <param name="sequence">The <see cref="JPrimitiveReadOnlySequence{TPrimitive}"/> instance.</param>
	internal static void ReleaseString(JPrimitiveReadOnlySequence sequence)
	{
		using IThread thread = sequence.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		JStringObject jString = sequence.Source as JStringObject ?? new(thread, (JGlobalBase)sequence.Source);
		thread.StringProvider.ReleaseSequence(jString, sequence.Pointer);
	}
	/// <summary>
	/// Release string utf chars pointer.
	/// </summary>
	/// <param name="sequence">The <see cref="JPrimitiveReadOnlySequence{TPrimitive}"/> instance.</param>
	internal static void ReleaseUtf8String(JPrimitiveReadOnlySequence sequence)
	{
		using IThread thread = sequence.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		JStringObject jString = sequence.Source as JStringObject ?? new(thread, (JGlobalBase)sequence.Source);
		thread.StringProvider.ReleaseUtf8Sequence(jString, sequence.Pointer);
	}
	/// <summary>
	/// Release string critical chars pointer.
	/// </summary>
	/// <param name="sequence">The <see cref="JPrimitiveReadOnlySequence{TPrimitive}"/> instance.</param>
	internal static void ReleaseCriticalString(JPrimitiveReadOnlySequence sequence)
	{
		using IThread thread = sequence.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		JStringObject jString = sequence.Source as JStringObject ?? new(thread, (JGlobalBase)sequence.Source);
		thread.StringProvider.ReleaseCriticalSequence(jString, sequence.Pointer);
	}
}

/// <summary>
/// This class represents a primitive values read-only sequence.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
public record JPrimitiveReadOnlySequence<TPrimitive> : JPrimitiveReadOnlySequence, IReadOnlyFixedContext<TPrimitive>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <inheritdoc cref="JPrimitiveReadOnlySequence{TPrimitive}.Count"/>
	private readonly Int32 _count;
	/// <inheritdoc cref="JPrimitiveReadOnlySequence.Critical"/>
	private readonly Boolean _critical;
	/// <inheritdoc cref="JPrimitiveReadOnlySequence.Copy"/>
	private readonly Boolean _isCopy;
	/// <inheritdoc cref="JPrimitiveReadOnlySequence.Pointer"/>
	private readonly IntPtr _pointer;
	/// <inheritdoc cref="JPrimitiveReadOnlySequence.Release"/>
	private readonly Action<JPrimitiveReadOnlySequence> _release;

	/// <summary>
	/// Number of <typeparamref name="TPrimitive"/> elements in current sequence.
	/// </summary>
	public Int32 Count => this._count;
	/// <inheritdoc/>
	public override Boolean Copy => this._isCopy;
	/// <inheritdoc/>
	public override Boolean Critical => this._critical;
	/// <inheritdoc/>
	internal override Int32 BinarySize => this._count * IPrimitiveType.GetMetadata<TPrimitive>().SizeOf;
	/// <inheritdoc/>
	internal override Action<JPrimitiveReadOnlySequence> Release => this._release;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="source">Array source.</param>
	/// <param name="isCritical">Indicates whether current sequence is critical.</param>
	protected JPrimitiveReadOnlySequence(JArrayObject<TPrimitive> source, Boolean isCritical) : base(source)
	{
		IEnvironment environment = source.Environment;
		this._count = source.Length;
		this._critical = isCritical;
		this._pointer = !isCritical ?
			environment.ArrayProvider.GetSequence(source, out this._isCopy) :
			environment.ArrayProvider.GetCriticalSequence(source);
		this._release = !isCritical ?
			JPrimitiveReadOnlySequence.ReleaseArray<TPrimitive> :
			JPrimitiveReadOnlySequence.ReleaseCriticalArray<TPrimitive>;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="source">String source.</param>
	/// <param name="pointer">Sequence pointer.</param>
	/// <param name="count">Sequence size.</param>
	/// <param name="isCopy">Indicates whether current sequence is a copy.</param>
	/// <param name="isCritical">Indicates whether current sequence is critical.</param>
	/// <param name="release">Release delegate.</param>
	internal JPrimitiveReadOnlySequence(JStringObject source, IntPtr pointer, Int32 count, Boolean isCopy,
		Boolean isCritical, Action<JPrimitiveReadOnlySequence> release) : base(source)
	{
		this._pointer = pointer;
		this._count = count;
		this._isCopy = isCopy;
		this._release = release;
		this._critical = isCritical;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="source">Global sequence</param>
	/// <param name="pointer">Sequence pointer.</param>
	/// <param name="count">Sequence size.</param>
	/// <param name="disposeSource">Indicates whether the source object should be disposed.</param>
	/// <param name="isCopy">Indicates whether current sequence is a copy.</param>
	/// <param name="isCritical">Indicates whether current sequence is critical.</param>
	/// <param name="release">Release delegate.</param>
	internal JPrimitiveReadOnlySequence(JGlobalBase source, Boolean disposeSource, IntPtr pointer, Int32 count,
		Boolean isCopy, Boolean isCritical, Action<JPrimitiveReadOnlySequence> release) : base(source, disposeSource)
	{
		this._release = release;
		this._pointer = pointer;
		this._isCopy = isCopy;
		this._critical = isCritical;
		this._count = count;
	}

	/// <inheritdoc/>
	public override IntPtr Pointer => this._pointer;
	/// <inheritdoc/>
	public ReadOnlySpan<TPrimitive> Values => this._pointer.GetUnsafeReadOnlySpan<TPrimitive>(this._count);
}