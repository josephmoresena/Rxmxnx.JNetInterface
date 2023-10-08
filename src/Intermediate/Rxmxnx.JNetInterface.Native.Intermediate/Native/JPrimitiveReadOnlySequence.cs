namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a primitive values read-only sequence.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
public record JPrimitiveReadOnlySequence<TPrimitive> : JPrimitiveSequenceBase, IReadOnlyFixedContext<TPrimitive>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <inheritdoc cref="JPrimitiveReadOnlySequence{TValue}.Count"/>
	private readonly Int32 _count;
	/// <inheritdoc cref="JPrimitiveSequenceBase.Copy"/>
	private readonly Boolean _isCopy;
	/// <inheritdoc cref="JPrimitiveSequenceBase.Pointer"/>
	private readonly IntPtr _pointer;
	/// <inheritdoc cref="JPrimitiveSequenceBase.Release"/>
	private readonly Action<JPrimitiveSequenceBase> _release;
	/// <inheritdoc cref="JPrimitiveSequenceBase.Critical"/>
	private readonly Boolean _critical;

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
	internal override Action<JPrimitiveSequenceBase> Release => this._release;

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
			JPrimitiveSequenceBase.ReleaseArray<TPrimitive> :
			JPrimitiveSequenceBase.ReleaseCriticalArray<TPrimitive>;
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
		Boolean isCritical, Action<JPrimitiveSequenceBase> release) : base(source)
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
		Boolean isCopy, Boolean isCritical, Action<JPrimitiveSequenceBase> release) : base(source, disposeSource)
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