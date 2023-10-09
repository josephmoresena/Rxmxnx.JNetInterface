namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Context of a read-only fixed primitive sequence.
/// </summary>
/// <typeparam name="TValue">Type of objects in the read-only fixed memory block.</typeparam>
internal record PrimitiveReadOnlyContext<TValue> : IReadOnlyFixedContext<TValue>
	where TValue : unmanaged
{
	/// <summary>
	/// Internal handler.
	/// </summary>
	public PrimitiveSequenceHandler Handler { get; private init; }
	/// <summary>
	/// Indicates whether current instance is valid.
	/// </summary>
	public IWrapper<Boolean> Valid { get; private init; }
	/// <summary>
	/// Internal offset.
	/// </summary>
	public Int32 Offset { get; private init; }
	
	/// <inheritdoc/>
	public IntPtr Pointer => this.Handler.Pointer + this.Offset;
	/// <summary>
	/// Binary memory size.
	/// </summary>
	public Int32 BinarySize => this.Handler.BinarySize - this.Offset;
	/// <summary>
	/// Number of elements in fixed memory sequence.
	/// </summary>
	public Int32 Count => this.BinarySize / NativeUtilities.SizeOf<TValue>();
	/// <summary>
	/// Binary representation of fixed memory.
	/// </summary>
	public ReadOnlySpan<Byte> Bytes 
	{
		get
		{
			ValidationUtilities.ThrowIfInvalidSequence(this.Valid);
			return this.Pointer.GetUnsafeReadOnlySpan<Byte>(this.BinarySize);
		}
	}
	/// <summary>
	/// Indicates whether current instance is read-only.
	/// </summary>
	public virtual Boolean ReadOnly => true;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="handler">A <see cref="PrimitiveSequenceHandler"/> instance.</param>
	/// <param name="valid">Indicates whether current instance is valid.</param>
	/// <param name="offset">Offset.</param>
	public PrimitiveReadOnlyContext(PrimitiveSequenceHandler handler, IWrapper<Boolean> valid, Int32 offset = 0)
	{
		this.Handler = handler;
		this.Valid = valid;
		this.Offset = offset;
	}
	
	ReadOnlySpan<TValue> IReadOnlyFixedMemory<TValue>.Values
	{
		get
		{
			ValidationUtilities.ThrowIfInvalidSequence(this.Valid);
			return this.Pointer.GetUnsafeReadOnlySpan<TValue>(this.BinarySize);
		}
	}
	
	IReadOnlyFixedContext<Byte> IReadOnlyFixedMemory.AsBinaryContext()
		=> this as IReadOnlyFixedContext<Byte> ?? (this.ReadOnly ?
			new PrimitiveReadOnlyContext<Byte>(this.Handler, this.Valid, this.Offset):
			new PrimitiveContext<Byte>(this.Handler, this.Valid, this.Offset));
	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<TValue>.Transformation<TDestination>(
		out IReadOnlyFixedMemory residual)
	{
		IReadOnlyFixedContext<TDestination> result = this as IReadOnlyFixedContext<TDestination> ?? (this.ReadOnly ?
			new PrimitiveReadOnlyContext<TDestination>(this.Handler, this.Valid, this.Offset) :
			new PrimitiveContext<TDestination>(this.Handler, this.Valid, this.Offset));
		Int32 offset = this.Offset + result.Values.Length * NativeUtilities.SizeOf<TDestination>();
		residual = (this.ReadOnly ?
			new PrimitiveReadOnlyContext<Byte>(this.Handler, this.Valid, offset) :
			new PrimitiveContext<Byte>(this.Handler, this.Valid, offset));
		return result;
	}
}