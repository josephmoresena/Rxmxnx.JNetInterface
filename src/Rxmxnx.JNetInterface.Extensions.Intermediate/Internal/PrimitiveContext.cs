namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Context of a fixed primitive sequence.
/// </summary>
/// <typeparam name="TValue">Type of objects in the read-only fixed memory block.</typeparam>
internal sealed record PrimitiveContext<TValue> : PrimitiveReadOnlyContext<TValue>, IFixedContext<TValue>
	where TValue : unmanaged
{
	/// <inheritdoc/>
	public override Boolean ReadOnly => false;

	/// <inheritdoc/>
	public PrimitiveContext(PrimitiveSequenceHandler handler, IWrapper<Boolean> valid, Int32 offset = 0) : base(
		handler, valid, offset) { }
	IFixedContext<Byte> IFixedMemory.AsBinaryContext() => throw new NotImplementedException();
	/// <summary>
	/// Binary representation of fixed memory.
	/// </summary>
	public new Span<Byte> Bytes
	{
		get
		{
			ValidationUtilities.ThrowIfInvalidSequence(this.Valid);
			return this.Pointer.GetUnsafeSpan<Byte>(this.BinarySize);
		}
	}

	Span<TValue> IFixedMemory<TValue>.Values
	{
		get
		{
			ValidationUtilities.ThrowIfInvalidSequence(this.Valid);
			return this.Pointer.GetUnsafeSpan<TValue>(this.BinarySize);
		}
	}

	IFixedContext<TDestination> IFixedContext<TValue>.Transformation<TDestination>(out IFixedMemory residual)
	{
		IFixedContext<TDestination> result = this as IFixedContext<TDestination> ??
			new PrimitiveContext<TDestination>(this.Handler, this.Valid, this.Offset);
		Int32 offset = this.Offset + result.Values.Length * NativeUtilities.SizeOf<TDestination>();
		residual = new PrimitiveContext<Byte>(this.Handler, this.Valid, offset);
		return result;
	}
	IFixedContext<TDestination> IFixedContext<TValue>.Transformation<TDestination>(out IReadOnlyFixedMemory residual)
	{
		IFixedContext<TDestination> result = this as IFixedContext<TDestination> ??
			new PrimitiveContext<TDestination>(this.Handler, this.Valid, this.Offset);
		Int32 offset = this.Offset + result.Values.Length * NativeUtilities.SizeOf<TDestination>();
		residual = new PrimitiveContext<Byte>(this.Handler, this.Valid, offset);
		return result;
	}
}