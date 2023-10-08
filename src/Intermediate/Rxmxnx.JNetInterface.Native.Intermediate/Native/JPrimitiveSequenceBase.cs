namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a primitive values sequence base.
/// </summary>
public abstract partial record JPrimitiveSequenceBase : IReadOnlyFixedContext<Byte>, IDisposable
{
	/// <summary>
	/// Indicates whether the source object should be disposed.
	/// </summary>
	private readonly Boolean _disposeSource;
	/// <inheritdoc cref="JPrimitiveSequenceBase.Source"/>
	private readonly JReferenceObject _source;
	/// <inheritdoc cref="JPrimitiveSequenceBase.VirtualMachine"/>
	private readonly IVirtualMachine _vm;

	/// <summary>
	/// Indicates current instance is disposed.
	/// </summary>
	private readonly Boolean _disposed = true;

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
	internal abstract Action<JPrimitiveSequenceBase> Release { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="source">Source object.</param>
	internal JPrimitiveSequenceBase(JLocalObject source)
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
	internal JPrimitiveSequenceBase(JGlobalBase source, Boolean disposeSource)
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
	internal static void ReleaseArray<TPrimitive>(JPrimitiveSequenceBase sequence)
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
	internal static void ReleaseCriticalArray<TPrimitive>(JPrimitiveSequenceBase sequence)
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
	internal static void ReleaseString(JPrimitiveSequenceBase sequence)
	{
		using IThread thread = sequence.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		JStringObject jString = sequence.Source as JStringObject ?? new(thread, (JGlobalBase)sequence.Source);
		thread.StringProvider.ReleaseSequence(jString, sequence.Pointer);
	}
	/// <summary>
	/// Release string utf chars pointer.
	/// </summary>
	/// <param name="sequence">The <see cref="JPrimitiveReadOnlySequence{TPrimitive}"/> instance.</param>
	internal static void ReleaseUtf8String(JPrimitiveSequenceBase sequence)
	{
		using IThread thread = sequence.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		JStringObject jString = sequence.Source as JStringObject ?? new(thread, (JGlobalBase)sequence.Source);
		thread.StringProvider.ReleaseUtf8Sequence(jString, sequence.Pointer);
	}
	/// <summary>
	/// Release string critical chars pointer.
	/// </summary>
	/// <param name="sequence">The <see cref="JPrimitiveReadOnlySequence{TPrimitive}"/> instance.</param>
	internal static void ReleaseCriticalString(JPrimitiveSequenceBase sequence)
	{
		using IThread thread = sequence.VirtualMachine.CreateThread(ThreadPurpose.RemoveGlobalReference);
		JStringObject jString = sequence.Source as JStringObject ?? new(thread, (JGlobalBase)sequence.Source);
		thread.StringProvider.ReleaseCriticalSequence(jString, sequence.Pointer);
	}
}