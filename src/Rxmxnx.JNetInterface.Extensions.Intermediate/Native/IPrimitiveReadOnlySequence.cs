// namespace Rxmxnx.JNetInterface.Native;
//
// /// <summary>
// /// This class represents a primitive values sequence.
// /// </summary>
// /// <typeparam name="TPrimitive">Type of <see cref="TPrimitive"/> element.</typeparam>
// public sealed record PrimitiveReadOnlySequence<TPrimitive> : JPrimitiveSequenceBase, IFixedContext<TPrimitive>,
// 	IFixedContext<Byte> where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
// {
// 	/// <inheritdoc cref="JPrimitiveReadOnlySequenceBase.ReleaseMode"/>
// 	public new JReleaseMode ReleaseMode
// 	{
// 		get => base.ReleaseMode;
// 		set => base.ReleaseMode = value;
// 	}
//
// 	/// <inheritdoc/>
// 	internal override Boolean ReadOnly => false;
//
// 	/// <inheritdoc/>
// 	internal PrimitiveReadOnlySequence(JArrayObject<TPrimitive> source, Boolean isCritical) : base(source, isCritical) { }
// 	/// <inheritdoc/>
// 	internal PrimitiveReadOnlySequence(JGlobalBase source, Boolean disposeSource, IntPtr pointer, Int32 count, Boolean isCopy,
// 		Boolean isCritical, Action<JPrimitiveReadOnlySequenceBase> release) : base(
// 		source, disposeSource, pointer, count, isCopy, isCritical, release) { }
// 	Span<Byte> IFixedMemory<Byte>.Values => this.Bytes;
// 	/// <inheritdoc/>
// 	public new Span<Byte> Bytes => this.Pointer.GetUnsafeSpan<Byte>(this.BinarySize);
// 	/// <inheritdoc/>
// 	public new Span<TPrimitive> Values => this.Pointer.GetUnsafeSpan<TPrimitive>(this.Count);
//
// 	IFixedContext<Byte> IFixedMemory.AsBinaryContext() => this;
//
// 	/// <inheritdoc cref="IFixedContext{T}.Transformation{TDetination}(out IFixedMemory)"/>
// 	public IFixedContext<TDestination> Transformation<TDestination>(out IFixedMemory residual)
// 		where TDestination : unmanaged
// 	{
// 		IFixedContext<TDestination>
// 			result = this as IFixedContext<TDestination> ?? new FixedContext<TDestination>(this);
// 		residual = new FixedContext<TDestination>(this, result.Bytes.Length);
// 		return result;
// 	}
// 	/// <inheritdoc cref="IFixedContext{T}.Transformation{TDetination}(out IReadOnlyFixedMemory)"/>
// 	public new IFixedContext<TDestination> Transformation<TDestination>(out IReadOnlyFixedMemory residual)
// 		where TDestination : unmanaged
// 	{
// 		IFixedContext<TDestination>
// 			result = this as IFixedContext<TDestination> ?? new FixedContext<TDestination>(this);
// 		residual = new FixedContext<TDestination>(this, result.Bytes.Length);
// 		return result;
// 	}
// }